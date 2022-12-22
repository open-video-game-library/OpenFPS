using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    //MySerial serial;
    float _pitch;
    float _roll;
    public string portNum;

    // Start is called before the first frame update
    void Start()
    {
        //serial = MySerial.Instance;
        //bool success = serial.Open(portNum, MySerial.Baudrate.B_115200);
        /*
        if (!success)
        {
            return;
        }
        */
        //serial.OnDataReceived += SerialCallBack;
    }

    private void OnDisable()
    {
        //serial.Close();
        //serial.OnDataReceived -= SerialCallBack;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SerialCallBack(string m)
    {
        objRotation(m);
    }

    void objRotation(string message)
    {
        string[] a;
        a = message.Split("="[0]);
        if (a.Length != 2)
        {
            return;
        }
        int v = int.Parse(a[1]);
        switch (a[0])
        {
            case "pitch":
                _pitch = v;
                break;
            case "roll":
                _roll = v;
                break;
        }
        Quaternion AddRot = Quaternion.identity;
        AddRot.eulerAngles = new Vector3(-_pitch, 0, -_roll);
        transform.rotation = AddRot;
    }
}