using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scopeCamera : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 30.0f;
    [SerializeField] private float delay = 0.3f;
    float angle;
    public Transform Player;
    Vector3 offset;
    Camera c;
    // Start is called before the first frame update
    void Start()
    {
        if(!Player)
        {
            Player = GameObject.Find("Tank").transform;
        }
        c = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        Transform movingObject = Player.transform.GetChild(0);
        // 위아래
        if (-15f < (angle - vertical * turnSpeed * 0.02f) && (angle - vertical * turnSpeed * 0.02f) < 15f)
        {
            angle += -vertical * turnSpeed * 0.02f;
            transform.Rotate(-vertical * turnSpeed * 0.05f * Vector3.right);
        }
        // 오른쪽왼쪽
        transform.RotateAround(Player.transform.position, Vector3.up, horizontal * turnSpeed * 0.2f);
        if (c.enabled)
        {
            movingObject.rotation = transform.rotation;
        }
    }
}
