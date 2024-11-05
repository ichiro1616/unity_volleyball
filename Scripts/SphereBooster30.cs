using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class SphereBooster300 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    async void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            await Task.Delay(18000);

            // 100km/hをメートル/秒に変換（1 km/h ≈ 0.2778 m/s）
            float speedInMetersPerSecond = 120.0f * 0.2778f;

            // ボールの初速度を設定する方向をVector3型で定義
            Vector3 initialVelocity = new Vector3(1.0f, 0.1f, 0.0f); //1.0f,0.8f,0.0f

            // 初速度に速度を乗じてボールに加える速度を計算
            Vector3 velocity = speedInMetersPerSecond * initialVelocity;

            // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // 速度を設定
            rb.velocity = velocity;

            // ボールに回転を加える
            Vector3 torque = new Vector3(0f, 10f, 20f);

            // 力を加えるメソッド
            // ForceMode.Impulseは撃力
            rb.AddTorque(torque, ForceMode.Impulse);
        }

        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            await Task.Delay(18000);

            // 100km/hをメートル/秒に変換（1 km/h ≈ 0.2778 m/s）
            float speedInMetersPerSecond = 30.0f * 0.2778f;

            // ボールの初速度を設定する方向をVector3型で定義
            Vector3 initialVelocity = new Vector3(1.7f, 0.8f, 0.0f); //1.0f,0.8f,0.0f

            // 初速度に速度を乗じてボールに加える速度を計算
            Vector3 velocity = speedInMetersPerSecond * initialVelocity;

            // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // 速度を設定
            rb.velocity = velocity;

            // ボールに回転を加える
            Vector3 torque = new Vector3(0f, 10f, 20f);

            // 力を加えるメソッド
            // ForceMode.Impulseは撃力
            rb.AddTorque(torque, ForceMode.Impulse);
        }

    }
}

