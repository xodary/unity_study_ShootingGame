using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TankPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float turnSpeed = 30.0f;
    private CharacterController controller;
    public GameObject MainCamera;
    public GameObject SubCamera;

    public GameObject bulletPrefab;
    public float delay = 0.3f;

    Vector3 offset = Vector3.zero;
    Vector3 wannaGo = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (!MainCamera)
            MainCamera = GameObject.Find("Main Camera");
        if (!SubCamera)
            SubCamera = GameObject.Find("ScopeCamera");

        MainCamera.GetComponent<Camera>().enabled = true;
        SubCamera.GetComponent<Camera>().enabled = false;
        SubCamera.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Vector3.zero;
        wannaGo = transform.forward;

        float horizontalInput = Input.GetAxis("Horizontal");
        Quaternion rotating = Quaternion.identity;
        int num = 1;
        if (Input.GetMouseButtonDown(1))
        {
            MainCamera.GetComponent<Camera>().enabled = false;
            SubCamera.GetComponent<Camera>().enabled = true;
            SubCamera.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            MainCamera.GetComponent<Camera>().enabled = true;
            SubCamera.GetComponent<Camera>().enabled = false;
            SubCamera.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
        if (Input.GetKey(KeyCode.W)) offset += transform.forward * Time.deltaTime * moveSpeed;
        if (Input.GetKey(KeyCode.S)) { offset -= transform.forward * Time.deltaTime * moveSpeed; num = -1; }
        transform.Rotate(0, num * offset.magnitude * horizontalInput * 70 * turnSpeed * Time.deltaTime, 0);
        transform.GetChild(1).GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(1).GetChild(0).rotation,
            transform.GetChild(1).rotation * Quaternion.Euler(0, horizontalInput * 10, 0),
            Mathf.Abs(horizontalInput));
        transform.GetChild(1).GetChild(1).rotation = transform.GetChild(1).GetChild(0).rotation;

        if (Input.GetKey(KeyCode.Q)) transform.Rotate(Vector3.down * turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R)) transform.eulerAngles += new Vector3(0, 45.0f, 0);
        Vector3 ex = transform.position;

        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
            Application.Quit();

        controller.Move(offset);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 now = transform.position;
        MainCamera.transform.Translate(now - ex, Space.World);
        SubCamera.transform.Translate(now - ex, Space.World);
        // ==================================== 카메라 조정 ====================================


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab,
                transform.GetChild(0).position + transform.GetChild(0).forward + Vector3.up * 0.5f,
                transform.GetChild(0).rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * 15f, ForceMode.Impulse);
            GameObject.Destroy(bullet, 5.0f);
        }
    }
    public GameObject FindInactiveObject(string name)
    {
        Transform[] trs = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform tr in trs)
        {
            if (tr.name == name && !tr.gameObject.activeSelf)
            {
                return tr.gameObject;
            }
        }
        return null;
    }
}

