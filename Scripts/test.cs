using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class input: MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // Update is called once per frame
    async void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
        //Aボタン
        Debug.Log("aaa");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
        //Aボタン
        Debug.Log("aaa");

        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
        //Xボタン
        Debug.Log("aaa");

        }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
        //Yボタン
        Debug.Log("aaa");

        }
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
        //左手メニュボタン
        Debug.Log("aaa");

        }

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
        //右手トリガー
        Debug.Log("aaa");

        }
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
        //右手中指グリップ
        Debug.Log("aaa");

        }
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
        //左手トリガー
        Debug.Log("aaa");

        }
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
        //左手中指グリップ
        Debug.Log("aaa");

        }
    }
}

