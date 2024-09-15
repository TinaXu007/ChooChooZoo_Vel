using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Linq;

public class ArduinoConnector : MonoBehaviour
{
    public static ArduinoConnector Instance {get; set;}
    SerialPort data_stream = new SerialPort("/dev/cu.usbmodem11101", 9600);
    public string receivedString = "";

    void Awake(){
        Instance = this;
    }

    void Start()
    {
        data_stream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        receivedString = data_stream.ReadLine();
        // Debug.Log(receivedString);
    }
}
