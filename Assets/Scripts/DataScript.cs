using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class DataScript : MonoBehaviour {
   
  

    private const int listenPort = 50000;
    // Use this for initialization
    void Start () {
        Thread t1 = new Thread(loadData);
        t1.IsBackground = true;
        t1.Start();
      
        
    }
	
	// Update is called once per frame
	void Update () {


        
    }

    private void loadData()
    {
        
        Debug.Log("Started");
        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

        try
        {
            while (true)
            {
                // Console.WriteLine("Waiting for broadcast");
                byte[] bytes = listener.Receive(ref groupEP);

                //Console.WriteLine($"Received broadcast from {groupEP} :");
                Debug.Log(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            }
        }
        catch (SocketException e)
        {
            Debug.Log(e);
        }
        finally
        {
            listener.Close();
        }


    }
}
