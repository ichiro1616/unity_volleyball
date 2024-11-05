using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;



public class experiment : MonoBehaviour
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
 
    private float speedInMetersPerSecond = 100.0f /3.6f;
 
    private float launchAngleTheta = 6.0f;  // X軸とY軸の間の角度（度単位）
    private float rotationAnglePhi = 0.0f;  // X軸とZ軸の間の角度（度単位）
 
     private string filePath; // ファイルパス
 
    private bool judge; //コートに入ったかどうか
   
    private float start_x_posi;
    private float start_y_posi;
    private float start_z_posi;
    private float start_x_velocity;
    private float start_y_velocity;
    private float start_z_velocity;
    private bool serveOverNet = false;
    private bool magnificationFlag = false;
    private float fixedDeltaTime;
    private int angularVelocity = 0;
    private TextAsset csvFile; // CSVファイル
    private List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト

    private int csvDatas_counter = 0;
    public void Start () {
        fixedDeltaTime = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 1000.0f; //AngularVelocityの初期値を変更。変更前は7
        // 各カメラオブジェクトを取得
        OVRCameraRig_front = GameObject.Find("OVRCameraRig_front");
        LoadCsv();
        SetSpeed();
        
        originalPosition = this.transform.position;
        setvelocityText();
        ThrowBall();
    }
    void FixedUpdate (){ //Updateだとフレームが飛んだ際の処理が飛ばされてしまう
        posi = this.transform.position;
 
        Debug.Log("経過時間(秒)" + Time.time + "Velocity: " + rb.velocity+ "Position: " + posi.y);
        if(posi.y >= 3.2 && posi.y <= 3.4 && rb.velocity.y < 0  && hasLanded == false){
            if (audioSource != null)
            {
                audioSource.volume = (1/100f) * speedInMetersPerSecond * 3.6f - (0.2f);
                Debug.Log(audioSource.volume);
                audioSource.Play();
            }
        }
        if(posi.y >= 2.6 && posi.y <= 2.8 && rb.velocity.y < 0  && hasLanded == false){
            hasLanded = true;
            magnificationFlag = true;
            // if (audioSource != null)
            // {
            //     audioSource.volume = (1/100f) * speedInMetersPerSecond * 3.6f - (0.2f);
            //     audioSource.volume = -(1/100f) * speedInMetersPerSecond * 3.6f + (5.2f);

            //     Debug.Log(audioSource.volume);
            //     audioSource.Play();
            // }
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
    }

    void LoadCsv(){
        Debug.Log("aaaaaaaaa");
        // TextAsset csvFile = Resources.Load<TextAsset>("random_rotation_varied_add_11/position_data_rotation_varied_add_11_5"); // CSVの拡張子を含めない 
        TextAsset csvFile = Resources.Load<TextAsset>("random_rotation_varied/position_data_rotation_varied1"); // CSVの拡張子を含めない 



        if (csvFile == null)
        {
            Debug.LogError("CSV file not found in Resources folder.");
            return;
        }

        StringReader reader = new StringReader(csvFile.text);
        reader.ReadLine();
        
        // int lineNumber = 1; // 行番号のカウンタを初期化
        // int startLine = 102; // 読み込みを開始する行番号
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            // lineNumber++; // 行番号をインクリメント
            // if (lineNumber < startLine)
            // {
            //     continue; // 101行目以前の行は無視
            // }
            string[] values = line.Split(','); // , 区切りで分割
            csvDatas.Add(values); // 条件を満たす行を追加
        }
        Debug.Log("Loaded CSV Data: " + csvDatas.Count + " rows.");
        foreach (var csvData in csvDatas)
        {
            Debug.Log(string.Join(", ", csvData)); // 各配列の要素をカンマ区切りで表示
        }


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
        rb.angularVelocity = new Vector3(0, 0, angularVelocity);  //毎秒x[rad/s]、9.549x[rpm]で回転 63[rad/s]だと約600[rpm]　

        Debug.Log(rb.angularVelocity);
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
            hasLanded = false;
            serveOverNet = false;
            magnificationFlag = false;
            setlandText();
            SetSpeed();
            ResetBall();
            setvelocityText();
            Thread.Sleep(3000);
            

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
            land.text = "着地\nPosition: " + (Mathf.Round(posi.z * 100) / 100f + 4.50f).ToString("F2") + ", " + (Mathf.Round(posi.y * 100) / 100f).ToString("F2") + ", " + Mathf.Abs((Mathf.Round(posi.x * 100) / 100f - 9.00f)).ToString("F2") + "\nVelocity: " + rb.velocity.ToString("F2");
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
        speedInMetersPerSecond = float.Parse(csvDatas[csvDatas_counter][0]) /3.6f;
        launchAngleTheta = float.Parse(csvDatas[csvDatas_counter][1]);
        rotationAnglePhi = float.Parse(csvDatas[csvDatas_counter][2]);
        angularVelocity = int.Parse(csvDatas[csvDatas_counter][16]);
        if(csvDatas_counter == 100){
            Application.Quit();
        }
        csvDatas_counter += 1;

 
    }
 
    void ThrowBall(){
        Vector3 force = new Vector3(0.0f, 2.5f, 0.0f);
        rb.AddForce(force, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(0, 0, angularVelocity); 
    }
    void textReset(){
        land.text = "";
        takeoff.text = "";
        velocity.text = "";

    }

}