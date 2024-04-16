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

    // Update is called once per frame
    void Update()
    {
        ReceiveData();
    }

    private void ReceiveData()
    {
        try
        {
            if (client.Available > 0)
            {
                //byte[] receivedBytes = client.Receive(ref endPoint);
                //string receivedData = System.Text.Encoding.ASCII.GetString(receivedBytes);
                //Debug.Log("Received data from server: " + receivedData);

                //// Parse received JSON data here and use it as needed
                //ParseAndUseData(receivedData);

                byte[] receivedBytes = client.Receive(ref endPoint);

                Debug.Log(receivedBytes.Length);
                // Convert byte array to float arrays
                float[] normalizedLandmarks = new float[receivedBytes.Length / sizeof(float) / 6]; // 3 floats per landmark (x, y, z) for normalized and world landmarks
                float[] worldLandmarks = new float[receivedBytes.Length / sizeof(float) / 6];

                Buffer.BlockCopy(receivedBytes, 0, normalizedLandmarks, 0, receivedBytes.Length / 2);
                Buffer.BlockCopy(receivedBytes, receivedBytes.Length/2 , worldLandmarks, 0, receivedBytes.Length / 2);



                // Use received data as needed
                UseData(normalizedLandmarks, worldLandmarks);

            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving data from server: " + e.Message);
        }
    }

    private void UseData(float[] normalizedLandmarks, float[] worldLandmarks)
    {
        // Use received data as needed
        Debug.Log("Normalized Landmarks: " + string.Join(", ", normalizedLandmarks));
        Debug.Log("World Landmarks: " + string.Join(", ", worldLandmarks));

        // Replace this example code with your actual processing logic
    }

    //private void ParseAndUseData(string jsonData)
    //{
    //    // Parse JSON data and use it as needed
    //    try
    //    {
    //        // Example parsing code
    //        JObject parsedData = JObject.Parse(jsonData);
    //        string normalizedLandmarks = parsedData["normalized_landmarks"].ToString();
    //        string worldLandmarks = parsedData["world_landmarks"].ToString();
    //        Debug.Log("Normalized Landmarks: " + normalizedLandmarks);
    //        Debug.Log("World Landmarks: " + worldLandmarks);

    //        // Replace this example code with your actual processing logic
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Error parsing JSON data: " + e.Message);
    //    }
    //}

    private void OnDestroy()
    {
        if (client != null)
            client.Close();
    }
}
