using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    // Start is called before the first frame update
    public PoseDataReceiver receiver;
    

    //if set to true = normalized landmarks, false = world landmarks
    //World Landmarks recreate what we want
    //public bool selectPoses = true;

    [Header("Joint Points")]
    public GameObject[] points;
    private Vector3[] normalized_landmark;
    private Vector3[] world_landmark;
    void Start()
    {
        //set the points to dedicated slot
        //generate points // we know it will be 33 points all the time
        //points = new GameObject[32];
        
        normalized_landmark = new Vector3[33];
        world_landmark = new Vector3[33];

    }

    // Update is called once per frame
    void Update()
    {
        //dance
        UpdatePose();

    }


    private void UpdatePose()
    {
       float [] normalized_landmark_flat = receiver.normalizedLandmarks;
       float [] world_landmark_flat = receiver.worldLandmarks;

        int vectorCount;
        
        if(normalized_landmark_flat.Length % 3 != 0 && world_landmark_flat.Length % 3 != 0)
        {
            Debug.Log("Invalid input array length, Must be divisible by 3, this is on-going");

        }
        else
        {
            vectorCount = normalized_landmark_flat.Length / 3;
            
            for (int i = 0; i < vectorCount; i++)
            {
                int xIndex = i * 3;
                int yIndex = i * 3 + 1;
                int zIndex = i * 3 + 2;

                // for now we store in new vector
                // but technically we dont have to we can move points right away
                normalized_landmark[i] = new Vector3(normalized_landmark_flat[xIndex] * -1, normalized_landmark_flat[yIndex] * -1, normalized_landmark_flat[zIndex] * -1);
                world_landmark[i] = new Vector3(world_landmark_flat[xIndex] * -1, world_landmark_flat[yIndex] * -1, world_landmark_flat[zIndex] * -1);

                //if(selectPoses)
                //{
                //    points[i].transform.position = normalized_landmark[i];
                //}
                //else
                //{
                    points[i].transform.position = world_landmark[i];
                
                //}

            }


        }

    }

}
