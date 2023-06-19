using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class Example : MonoBehaviour
{
    public GameObject redCube, blueCube;
    float xVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.MoveTowards(redCube.transform.position.x,
            20.0f, 5.0f * Time.deltaTime);
        redCube.transform.position = new Vector3(x, 0, 0);
        float angle = Mathf.MoveTowardsAngle(redCube.transform.eulerAngles.z,
            270.0f, 45.0f * Time.deltaTime);
        redCube.transform.eulerAngles = new Vector3(0, 0, angle);

        redCube.transform.position = new Vector3(Mathf.PingPong(Time.time, 3.0f), 0.0f, 0.0f);
        blueCube.transform.position = new Vector3(Mathf.Repeat(Time.time, 3.0f), 0.0f, 3.0f);

        blueCube.transform.position = new Vector3(Mathf.Lerp(1.0f, 5.0f, Time.time), 3.0f, 3.0f);

        float angle1 = Mathf.LerpAngle(0.0f, 360.0f, Time.time);
        blueCube.transform.eulerAngles = new Vector3(0, 0, angle1);

        float t = Time.time / 5.0f;
        blueCube.transform.position = new Vector3(Mathf.SmoothStep(0.0f, 5.0f, Time.time / 5.0f), 0, 0);

        float x1 = Mathf.SmoothDamp(blueCube.transform.position.x, 0.0f, ref xVelocity, 0.9f);
        blueCube.transform.position = new Vector3(x1, 0.0f, 3.0f);
    }
}
