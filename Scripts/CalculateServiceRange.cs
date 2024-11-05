using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
 
public class CalculateServiceRange : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasLanded = false;
    public Text takeoff;
    public Text land;
    public Text velocity;

    private GameObject OVRCameraRig_front;
    // private GameObject OVRCameraRig_side;
 
 
    private Vector3 posi;
    private Vector3 originalPosition;

    public AudioSource audioSource;
 
    private float speedInMetersPerSecond = 40.0f /3.6f;
 
    private float launchAngleTheta = 0.0f;  // X軸とY軸の間の角度（度単位）
    private float rotationAnglePhi = -10.0f;  // X軸とZ軸の間の角度（度単位）
 
     private string filePath; // ファイルパス
 
    private bool judge; //コートに入ったかどうか
   
    private float start_x_posi;
    private float start_y_posi;
    private float start_z_posi;
    private float start_x_velocity;
    private float start_y_velocity;
    private float start_z_velocity;
    private float previousFrameLanding_x_velocity;
    private float previousFrameLanding_y_velocity;
    private float previousFrameLanding_z_velocity;


    private bool serveOverNet = false;
    private bool magnificationFlag = false;
    private float fixedDeltaTime;
 
    private int angularVelocity = 11;
 
 
 
 
 
    public void Start () {
        fixedDeltaTime = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 1000.0f; //AngularVelocityの初期値を変更。変更前は7
 
 
        // 各カメラオブジェクトを取得
        OVRCameraRig_front = GameObject.Find("OVRCameraRig_front");
        // OVRCameraRig_side = GameObject.Find("OVRCameraRig_side");
       
        // サブカメラはデフォルトで無効にしておく
        // OVRCameraRig_side.SetActive(false);
 
        originalPosition = this.transform.position;
        filePath = Application.dataPath + "/position_data_rotation_11.csv"; // 出力先のファイルパスを設定する
        Debug.Log(filePath);
        string header = "Velocity,X-Y_Angle,X-Z_Angle,judge,start_x_posi,start_y_posi,start_z_posi,land_x_posi,land_y_posi,land_z_posi,start_x_velocity,start_y_velocity,start_z_velocity,land_x_velocity,land_y_velocity,land_z_velocity,angular_velocity\n";
        AppendToCSV(header);
        setvelocityText();

        ThrowBall();
    }
    void FixedUpdate (){ //Updateだとフレームが飛んだ際の処理が飛ばされてしまう
        posi = this.transform.position;
        previousFrameLanding_x_velocity = (Mathf.Round(rb.velocity.x * 100) / 100f);
        previousFrameLanding_y_velocity = (Mathf.Round(rb.velocity.y * 100) / 100f);
        previousFrameLanding_z_velocity = (Mathf.Round(rb.velocity.z * 100) / 100f);

 
        // Debug.Log("経過時間(秒)" + Time.time + "Velocity: " + rb.velocity+ "Position: " + posi.y);
        // if(posi.y >= 3.2 && posi.y <= 3.4 && rb.velocity.y < 0  && hasLanded == false){
        //     if (audioSource != null)
        //     {
        //         audioSource.volume = (1/100f) * speedInMetersPerSecond * 3.6f - (0.2f);
        //         Debug.Log(audioSource.volume);
        //         audioSource.Play();
        //     }
        // }
        if(posi.y >= 2.6 && posi.y <= 2.8 && rb.velocity.y < 0  && hasLanded == false){
            hasLanded = true;
            magnificationFlag = true;
            if (audioSource != null)
            {
                audioSource.volume = (1/100f) * speedInMetersPerSecond * 3.6f - (0.2f);
                audioSource.volume = -(1/100f) * speedInMetersPerSecond * 3.6f + (5.2f);

                Debug.Log(audioSource.volume);
                audioSource.Play();
            }
            // Debug.Log("サーブ！！！");
            Serve();
        }
 
        if(posi.x >= -0.2 && posi.x <= 0.2){
            if(posi.y >= 2.2 && posi.z >= -4.5 && posi.z <= 4.5){
                serveOverNet = true;
                Debug.Log("ネット判定：" + serveOverNet);
            }
        }
 
        if (magnificationFlag)
        {
            ApplyMagnusEffect();
        }
        // Debug.Log(previousFrameLanding_x_velocity.ToString("F2")+ ", " + previousFrameLanding_y_velocity.ToString("F2")+", " + previousFrameLanding_z_velocity.ToString("F2"));
 
 
        // // もしSpaceキーが押されたならば、
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // 各カメラオブジェクトの有効フラグを逆転(true→false,false→true)させる
        //     OVRCameraRig_front.SetActive(!OVRCameraRig_front.activeSelf);
        //     OVRCameraRig_side.SetActive(!OVRCameraRig_side.activeSelf);
        // }
    
    }
 
    void Serve()
    {
        // rb.velocity = Vector3.zero;
        // rb.angularVelocity = Vector3.zero;
        Debug.Log("速度:" + speedInMetersPerSecond * 3.6f + "  X-Y角度:" + launchAngleTheta + "  X-Z角度:" + rotationAnglePhi);
        // 角度をラジアンに変換
        float thetaRad = launchAngleTheta * Mathf.Deg2Rad;
        float phiRad = rotationAnglePhi * Mathf.Deg2Rad;
 
        // 初速度の成分を計算
        float v0y = Mathf.Sin(thetaRad);
        float v0x = Mathf.Cos(thetaRad) * Mathf.Cos(phiRad);
        float v0z = Mathf.Cos(thetaRad) * Mathf.Sin(phiRad);
 
        // 初速度ベクトルを設定
        Vector3 initialVelocity = new Vector3(-v0x, v0y, v0z);
        // 初速度に速度を乗じてボールに加える速度を計算
        Vector3 velocity = speedInMetersPerSecond * initialVelocity;
 
        // 速度を設定
        rb.velocity = velocity;
        // rb.angularVelocity = new Vector3(0, 0, angularVelocity);  //毎秒x[rad/s]、9.549x[rpm]で回転 63[rad/s]だと約600[rpm]

        // Debug.Log(rb.angularVelocity);
        settakeoffText();
 
    }
 
 
    void ApplyMagnusEffect() // マグヌス効果を付与
{
    float airDensity = 1.293f; // 空気の密度（kg/m³）
    SphereCollider sphereCollider = GetComponent<SphereCollider>();
   
    float radius = sphereCollider.radius; // ボールの半径
    float angularVelocityMagnitude = rb.angularVelocity.magnitude; // 角速度の大きさ
    float velocityMagnitude = rb.velocity.magnitude; // 速度の大きさ
 
    var direction = Vector3.Cross(rb.angularVelocity, rb.velocity).normalized; // 速度と角速度のベクトルの外積を正規化して求める
 
    // 画像に示されたLiftの式を用いる
    float liftForceMagnitude = (4f / 3f) * (4f * Mathf.Pow(Mathf.PI,2) * Mathf.Pow(radius, 3) * airDensity * velocityMagnitude * angularVelocityMagnitude) * fixedDeltaTime;
    // float liftForceMagnitude = (4f / 3f) * (Mathf.PI* Mathf.Pow(radius, 3) * airDensity * velocityMagnitude * angularVelocityMagnitude) *fixedDeltaTime;
    // Debug.Log("angularVelocity:" + rb.angularVelocity + " velocity:" + rb.velocity + " direction:" + direction + " liftForceMagnitude:" + liftForceMagnitude);
 
    rb.AddForce(direction * liftForceMagnitude);
}
 
 
 
    // 衝突イベントを処理
    void OnCollisionEnter(Collision collision)
    {
        // ボールが地面に着地したとき
        if (collision.gameObject.CompareTag("Ground") && hasLanded)
        {
            Debug.Log("衝突しました");
            Vector3 posi = this.transform.position;
            judge = (posi.x >= -9.0 && posi.x <= 0.0) && (posi.z >= -4.5 && posi.z <= 4.5) && serveOverNet;
            Debug.Log(serveOverNet);

            string data = $"{Mathf.Round(speedInMetersPerSecond * 3.6f * 100 / 100f)},{launchAngleTheta},{rotationAnglePhi},{judge},{start_x_posi},{start_y_posi},{start_z_posi},{Mathf.Abs((Mathf.Round(posi.x * 100) / 100f - 9.00f))},{(Mathf.Round(posi.y * 100) / 100f)},{(Mathf.Round(posi.z * 100) / 100f + 4.50f)},{(Mathf.Round(start_x_velocity * 100) / 100f)},{(Mathf.Round(start_y_velocity * 100) / 100f)},{(Mathf.Round(start_z_velocity * 100) / 100f)},{(Mathf.Round(previousFrameLanding_x_velocity * 100) / 100f)},{(Mathf.Round(previousFrameLanding_y_velocity * 100) / 100f)},{(Mathf.Round(previousFrameLanding_z_velocity * 100) / 100f)},{angularVelocity}\n";
            AppendToCSV(data);
            hasLanded = false;
            serveOverNet = false;
            magnificationFlag = false;
            setlandText();
            SetSpeed();

            ResetBall();
            setvelocityText();
            
            ThrowBall();
        }
    }
 
    void settakeoffText()
    {
            Vector3 posi = this.transform.position;
            takeoff.text = "開始\nPosition: " + (Mathf.Round(posi.z * 100) / 100f + 4.50f).ToString("F2") + ", " + (Mathf.Round(posi.y * 100) / 100f).ToString("F2") + ", " + (Mathf.Round(posi.x * 100) / 100f - 9.00f).ToString("F2") + "\nVelocity: " + rb.velocity.ToString("F2");
 
            start_x_posi = (Mathf.Round(posi.x * 100) / 100f - 9.00f);
            start_y_posi = (Mathf.Round(posi.y * 100) / 100f);
            start_z_posi = (Mathf.Round(posi.z * 100) / 100f + 4.50f);
            start_x_velocity = rb.velocity.x;
            start_y_velocity = rb.velocity.y;
            start_z_velocity = rb.velocity.z;
 
    }
 
    void setlandText()
    {
            Vector3 posi = this.transform.position;
            land.text = "着地\nPosition: " + (Mathf.Round(posi.z * 100) / 100f + 4.50f).ToString("F2") + ", " + (Mathf.Round(posi.y * 100) / 100f).ToString("F2") + ", " + Mathf.Abs((Mathf.Round(posi.x * 100) / 100f - 9.00f)).ToString("F2") + "\nVelocity: " + previousFrameLanding_x_velocity.ToString("F2") + ", " + previousFrameLanding_y_velocity.ToString("F2") + ", " + previousFrameLanding_z_velocity.ToString("F2");
 
    }

    void setvelocityText()
    {
            
            velocity.text = "速度: " + Mathf.Round(speedInMetersPerSecond * 3.6f * 100 / 100f) + "km/h";
            
    }
 
    void ResetBall(){
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = originalPosition;
    }
 
    void SetSpeed(){
        //無回転サーブ
            //１回目
            //speed:(30,120,10)
            // X軸とY軸の間の角度:(0,90,10)
            // X軸とZ軸の間の角度:(-30,30,5)
            if(speedInMetersPerSecond == 82.0){
                Debug.Log("終了");
                Application.Quit();
            }
            if(rotationAnglePhi == 10.0){
                launchAngleTheta += 2.0f;
                rotationAnglePhi = -10.0f;
            }else{
                rotationAnglePhi += 2.0f;
            }

            if(launchAngleTheta == 32.0){
                speedInMetersPerSecond+=2.0f /3.6f;
                launchAngleTheta = 0.0f;
            }
            
            //2回目 20*25*20*6[s] /3600 = 16[h]
            //speed:(30,110,2)
            // X軸とY軸の間の角度:(0,50,2)
            // X軸とZ軸の間の角度:(-20,20,2)
 
        //回転サーブ
            //１回目
            //speed:(30,130,2)
            // X軸とY軸の間の角度:(0,46,2)
            // X軸とZ軸の間の角度:(-10,10,2)
        
        
    
 
    }
 
    void ThrowBall(){
        Vector3 force = new Vector3(0.0f, 2.5f, 0.0f);
        rb.AddForce(force, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(0, 0, angularVelocity);  
    }
    void textReset(){
        land.text = "";
        takeoff.text = "";
    }
 
    void AppendToCSV(string data)
    {
        // ファイルにデータを追記する
        File.AppendAllText(filePath, data);
    }
 
}