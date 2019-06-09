using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

public class GyroControl : MonoBehaviour
{
    public InputField portField;
    public InputField localIpField;
    public InputField gyroIpField;
    public static GyroControl Instance { get; private set; }
    private UdpClient udpClient;
    private ConcurrentQueue<string> inputQueue = new ConcurrentQueue<string>();
    private string localIp;
    private IPAddress gyroIp;
    private float difX;
    private float difY;
    private float difZ;
    private float rawX;
    private float rawY;
    private float rawZ;


    public float RotX
    {
        get
        {
            return rotX;
        }

        set
        {
            rotX = value;
        }
    }

    public float RotY
    {
        get
        {
            return rotY;
        }

        set
        {
            rotY = value;
        }
    }

    public float RotZ
    {
        get
        {
            return rotZ;
        }

        set
        {
            rotZ = value;
        }
    }


    private int port;




    private float rotX;
    private float rotY;
    private float rotZ;
    
    private Thread t1;
    // Use this for initialization
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        Instance.rotX = 0;
        Instance.rotY = 0;
        Instance.rotZ = 0;
        difX = 0;
        difY = 0;
        difZ = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToZero();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Calibrate();
        }
        if(t1 != null) { Debug.Log(t1.IsAlive); }
        string message = "";
        while (inputQueue.TryDequeue(out message))
        {

            try
            {
                message = Regex.Replace(message, "[,.]", ",");
                message = Regex.Replace(message, "[\\s]", ",");
                string[] rotVals = message.Split(';');


               

                RotX += (float.Parse(rotVals[0]) - difX) / 250;
                RotY += (float.Parse(rotVals[1]) - difY) / 250;
                RotZ += (float.Parse(rotVals[2]) - difZ) / 250;
                rawY = float.Parse(rotVals[1]) / 250;
                rawX = float.Parse(rotVals[0]) / 250;
                rawZ = float.Parse(rotVals[2]) / 250;
            }catch(Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }


    public void gyroConnect()
    {

        Instance.localIp = localIpField.text;
        Instance.gyroIp = IPAddress.Parse(gyroIpField.text);
        port = int.Parse(portField.text);
        if (Instance.udpClient != null)
        { udpClient.Close(); }
            
            Instance.udpClient = new UdpClient(port);
            Debug.Log("Started a new UDP client on port: "+udpClient );
        

        
        
        if (Instance.t1 == null)
        {
            Instance.t1 = new Thread(GyroStart);
        }
        else
        {
            Instance.t1.Abort();
            Instance.t1 = new Thread(GyroStart);
            
        }
        Instance.t1.IsBackground = true;
        Instance.t1.Start();


    }
    private void GyroStop()
    {
        Instance.t1.Abort();
        Instance.udpClient.Close();
        Instance.t1 = null;
        Instance.udpClient = null;
    }
    private void GyroStart()
    {
        Debug.Log("StartedThread");
        IPEndPoint iPEnd = new IPEndPoint(Instance.gyroIp,port);
        IPEndPoint all = new IPEndPoint(IPAddress.Any,port);
        byte[] sendIp = Encoding.ASCII.GetBytes(Instance.localIp);
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        s.SendTo(sendIp, iPEnd);
        
        s.Close();

        while (true)
        {
            byte[] receiveBytes = Instance.udpClient.Receive(ref all);
            Debug.Log("Passed this");
            string message = Encoding.ASCII.GetString(receiveBytes);

            inputQueue.Enqueue(message);

           // Thread.Sleep(1);
        }



        /*
                while (true)
                {
                    byte[] receiveBytes = Instance.udpClient.Receive(ref all);
                    Debug.Log("Passed this");
                    string message = Encoding.ASCII.GetString(receiveBytes);

                    string[] rotVals = message.Split(';');
                    lock (Instance)
                    {
                        Instance.RotX += (float.Parse(rotVals[0]) - Instance.difX) / 250;
                        Instance.RotY += (float.Parse(rotVals[1]) - Instance.difY) / 250;
                        Instance.RotZ += (float.Parse(rotVals[2]) - Instance.difZ) / 250;
                        Instance.rawY = float.Parse(rotVals[1]) / 250;
                        Instance.rawX = float.Parse(rotVals[0]) / 250;
                        Instance.rawZ = float.Parse(rotVals[2]) / 250;
                    }

                    Thread.Sleep(1);

                }
                */






    }



    private void OnApplicationQuit()
    {
        GyroStop();
    }

    public void ResetToZero()
    {
        Instance.RotX = 0;
        Instance.RotY = 0;
        Instance.RotZ = 0;
    }
    public void Calibrate()
    {
        Instance.difX = Instance.rawX;
        Instance.difY = Instance.rawY;
        Instance.difZ = Instance.rawZ;

    }
}
