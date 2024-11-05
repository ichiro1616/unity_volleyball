using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatgpt : MonoBehaviour
{
    public float mass = 0.27f; // 質量
    public float targetHeight = 6.296287f; // 目標高さ

    private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();

        // 力の計算
        float g = 9.81f;
        float initialVelocity = Mathf.Sqrt(2 * g * targetHeight);

        // インパルスの計算
        Vector3 force = new Vector3(0.0f, initialVelocity * mass / 0.01f, 0.0f);
        rb.AddForce(force, ForceMode.Impulse);
    }

    void Update (){
        Vector3 posi = this.transform.position;
        Debug.Log("経過時間(秒)" + Time.time + " Velocity: " + rb.velocity + " x = " + posi.y);
    }
}
