using UnityEngine;

public class VolleyballServe : MonoBehaviour
{
    public float targetSpeedKmh = 90f; // 目標速度 (km/h)
    public Vector3 serveDirection = new Vector3(-1.0f, 1.0f, 0.0f); // サーブの方向

    private Rigidbody rb;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得
        Vector3 force = new Vector3(0.0f, 3.0f, 0.0f);
        rb.AddForce(force, ForceMode.Impulse);
    }


    void Update()
    {
        // スペースキーを押したらサーブを実行
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Serve();
        }
        Debug.Log("Velocity: " + rb.velocity);
    }


    void Serve()
    {
        // // 速度の変換 (km/h から m/s)
        // float targetSpeedMs = targetSpeedKmh / 3.6f;

        // // サーブの方向を正規化し、速度を設定
        // Vector3 targetVelocity = serveDirection.normalized * targetSpeedMs;

        // // 速度を直接設定
        // rb.velocity = targetVelocity;

           // 100km/hをメートル/秒に変換（1 km/h ≈ 0.2778 m/s）
            // float targetSpeedMs = targetSpeedKmh / 3.6f;


            // // ボールの初速度を設定する方向をVector3型で定義
            // Vector3 initialVelocity = new Vector3(-1.0f, 0.1f, 0.0f); //1.0f,0.8f,0.0f

            // // 初速度に速度を乗じてボールに加える速度を計算
            // Vector3 velocity = speedInMetersPerSecond * initialVelocity;

            // // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
            // Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // // 速度を設定
            // rb.velocity = velocity;

            // // ボールに回転を加える
            // Vector3 torque = new Vector3(0f, 10f, 20f);

            // // 力を加えるメソッド
            // // ForceMode.Impulseは撃力
            // rb.AddTorque(torque, ForceMode.Impulse);
    }
}
