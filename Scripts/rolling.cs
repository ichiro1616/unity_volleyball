using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rolling : MonoBehaviour
{
    public bool Flag = false;
    private Rigidbody rb;
    private bool hasLanded = false;
    public Text takeoff;
    public Text land;

    public GameObject OVRCameraRig_front;
    public GameObject OVRCameraRig_side;


    void Start () {
        GetComponent<Rigidbody>().maxAngularVelocity = 1000.0f; //AngularVelocityの初期値を変更。変更前は7

        // 各カメラオブジェクトを取得
        OVRCameraRig_front = GameObject.Find("OVRCameraRig_front");
        OVRCameraRig_side = GameObject.Find("OVRCameraRig_side");
        
        // サブカメラはデフォルトで無効にしておく
        OVRCameraRig_side.SetActive(false);


        rb = GetComponent<Rigidbody>();
        // Vector3 force = new Vector3(0.0f, 3.0f, 0.0f);
        // rb.AddForce(force, ForceMode.Impulse);
        // Vector3 torque = new Vector3(0f, 0f, 95.49f);
        // rb.AddTorque(torque, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(0, 0, 100);  //毎秒x[rad/s]、9.549x[rpm]で回転

    }
    void FixedUpdate (){ //Updateだとフレームが飛んだ際の処理が飛ばされてしまう
        Vector3 posi = this.transform.position;
        Debug.Log("経過時間(秒)" + Time.time + "Velocity: " + rb.velocity + "y = " + posi.y);
    
        // ApplyMagnusEffect();
        // もしSpaceキーが押されたならば、
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 各カメラオブジェクトの有効フラグを逆転(true→false,false→true)させる
            OVRCameraRig_front.SetActive(!OVRCameraRig_front.activeSelf);
            OVRCameraRig_side.SetActive(!OVRCameraRig_side.activeSelf);
        }
        
    }
        void ApplyMagnusEffect()//マグヌス効果を付与
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        var direction = Vector3.Cross(rb.angularVelocity, rb.velocity);
        Debug.Log("angularVelocity:" + rb.angularVelocity + "velocity:" + rb.velocity + "direction" + direction);
        var magnification = 4 / 3f * Mathf.PI * rb.angularDrag * Mathf.Pow(sphereCollider.radius, 3);
        rb.AddForce(direction * magnification);
        Debug.Log("AddForce" + direction * magnification);
    }

    

    // 衝突イベントを処理
    void OnCollisionEnter(Collision collision)
    {
        // ボールが地面に着地したとき
        if (collision.gameObject.CompareTag("Ground") && hasLanded)
        {
            hasLanded = false;
            setlandText();
        }
    }

    void settakeoffText() 
    {
            Vector3 posi = this.transform.position;
            takeoff.text = "開始\nPosition: " + (Mathf.Round(posi.z * 100) / 100f + 4.50f).ToString("F2") + ", " + (Mathf.Round(posi.y * 100) / 100f).ToString("F2") + ", " + (Mathf.Round(posi.x * 100) / 100f - 9.00f).ToString("F2") + "\nVelocity: " + rb.velocity.ToString("F2");
    }

        void setlandText() 
    {
            Vector3 posi = this.transform.position;
            land.text = "着地\nPosition: " + (Mathf.Round(posi.z * 100) / 100f + 4.50f).ToString("F2") + ", " + (Mathf.Round(posi.y * 100) / 100f).ToString("F2") + ", " + Mathf.Abs((Mathf.Round(posi.x * 100) / 100f - 9.00f)).ToString("F2") + "\nVelocity: " + rb.velocity.ToString("F2");

    }


}


