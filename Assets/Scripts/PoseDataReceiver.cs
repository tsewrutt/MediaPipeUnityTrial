using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class PoseDataReceiver : MonoBehaviour
{
    public int port = 65304;
    public string serverAddress = "127.0.0.1";

    private UdpClient client;
    private IPEndPoint endPoint;

    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();

        // Start listening for data asynchronously
        //client.BeginReceive(ReceiveCallback, null);
    }

    private void ConnectToServer()
    {
        try
        {
            client = new UdpClient(port); //IPAddress.Parse(serverAddress)
            endPoint = new IPEndPoint(IPAddress.Any, port);
            Debug.Log("Connected to server");
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    //// Update is called once per frame
    void Update()
    {
        ReceiveData();
    }

    //private void ReceiveCallback(IAsyncResult result)
    //{
    //    try
    //    {
    //        byte[] receivedBytes = client.EndReceive(result, ref endPoint);

    //        // Ensure received data is large enough to be processed
    //        if (receivedBytes.Length % sizeof(float) != 0)
    //        {
    //            Debug.LogError("Received data size is not a multiple of float size");
    //            return;
    //        }

    //        // Calculate number of floats
    //        int numFloats = (receivedBytes.Length / 2) / sizeof(float);

    //        // Create array to hold float data
    //        float[] normalizedLandmarks = new float[numFloats];
    //        float[] worldLandmarks = new float[numFloats];


    //        //float[] worldLandmarks = new float[receivedBytes.Length / sizeof(float) / 3];

    //        Buffer.BlockCopy(receivedBytes, 0, normalizedLandmarks, 0, receivedBytes.Length / 2);
    //        Buffer.BlockCopy(receivedBytes, receivedBytes.Length / 2, worldLandmarks, 0, receivedBytes.Length / 2);

    //        // Process received data here (e.g., pass it to another method)
    //        ProcessLandmarks(normalizedLandmarks, worldLandmarks);

    //        // Continue listening for more data
    //        client.BeginReceive(ReceiveCallback, null);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Error receiving data: " + e.Message);
    //    }
    //}


    private void ReceiveData()
    {
        try
        {
            if (client.Available > 0)
            {

                byte[] receivedBytes = client.Receive(ref endPoint);
                //byte[] receivedBytes = client.Receive(ref endPoint);

                // Calculate number of floats
                int numFloats = (receivedBytes.Length / 2) / sizeof(float);

                // Create array to hold float data
                float[] normalizedLandmarks = new float[numFloats];
                float[] worldLandmarks = new float[numFloats];


                //float[] worldLandmarks = new float[receivedBytes.Length / sizeof(float) / 3];

                Buffer.BlockCopy(receivedBytes, 0, normalizedLandmarks, 0, receivedBytes.Length / 2);
                Buffer.BlockCopy(receivedBytes, receivedBytes.Length / 2, worldLandmarks, 0, receivedBytes.Length / 2);



                // Use received data as needed
                ProcessLandmarks(normalizedLandmarks, worldLandmarks);

            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving data from server: " + e.Message);
        }
    }
    private void ProcessLandmarks(float[] normalizedLandmarks, float[] worldLandmarks)
    {
        // Process received landmarks here
        Debug.Log("Normalized landmarks: " + string.Join(", ", normalizedLandmarks));
        Debug.Log("\nWorld Landmarks:" + string.Join(", ", worldLandmarks));
    }
    

    private void OnDestroy()
    {
        if (client != null)
            client.Close();
    }
}
