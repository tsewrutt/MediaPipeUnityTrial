using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerLineMapping : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Line Points")]
    public GameObject point1;
    public GameObject point2;

    private LineRenderer lr;
    private Vector3 startpt;
    private Vector3 endpt;
    // Update is called once per frame
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        startpt = point1.transform.position;
        endpt = point2.transform.position;

        lr.SetPosition(0,startpt);
        lr.SetPosition(1,endpt);

    }
}
