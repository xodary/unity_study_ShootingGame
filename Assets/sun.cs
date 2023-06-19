using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    public Transform sunset, sunrise;

    // Start is called before the first frame update
    void Start()
    {
        sunrise.position = new Vector3(0, 0, 0);
        sunset.position = new Vector3(0, 0, 10);
        transform.position = new Vector3(0, 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = (sunrise.position + sunset.position) * 0.5f;
        center -= new Vector3(0, 1, 0);
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, Time.time / 10);
        transform.position += center;
    }
}
