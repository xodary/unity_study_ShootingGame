using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public class HumanPlayer : MonoBehaviour
{
    public float DirectionDampTime = .25f;
    protected Animator animator;
    public Transform Gun;
    public GameObject bulletObject;
    private Transform spine;
    public Transform Target;
    public GameObject[] cameraObjects = new GameObject[3];
    public Camera[] cameras = new Camera[3];
    float clickTime = 0.0f;
    public Quaternion currentRotation = Quaternion.identity;
    public float idleTime = 0.0f;
    public int CameraMode = 2;
    public float xSpeed = 1.0f;
    public float ySpeed = 1.0f;
    public float yRotation = 0.0f;
    public Quaternion originalRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        if (cameras[0] == null)
        {
            cameraObjects[0] = GameObject.Find("Scope Camera");
            cameras[0] = cameraObjects[0].GetComponent<Camera>();
        }
        if (cameras[1] == null)
        {
            cameraObjects[1] = GameObject.Find("2nd Camera");
            cameras[1] = cameraObjects[0].GetComponent<Camera>();
        }
        if (cameras[2] == null)
        {
            cameraObjects[2] = GameObject.Find("Main Camera");
            cameras[2] = cameraObjects[0].GetComponent<Camera>();
        }
        if(Target == null)
        {
            Target = GameObject.Find("Target").transform;
        }
        cameras[0].enabled = false;
        cameras[1].enabled = false;
        animator = GetComponent<Animator>();
        spine = animator.GetBoneTransform(HumanBodyBones.Spine);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1) && CameraMode == 2)
        {
            clickTime += Time.deltaTime;
            if (clickTime >= 0.2f)
            {
                CameraChange(1);
                transform.rotation = currentRotation; 
                spine.transform.rotation = transform.rotation;
                Target.rotation = transform.rotation;
                Target.position = transform.position;
                Target.Translate(new Vector3(0, 1.6f, 1.7f), Space.Self);
                cameraObjects[CameraMode].transform.LookAt(Target);
                animator.enabled = true;
            }
            yRotation = 0;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if(CameraMode == 2)
            {
                CameraChange(0);
                animator.enabled = false;
            }
            else
            {
                CameraChange(2);
                animator.enabled = true;
            }
            clickTime = 0;
            yRotation = 0;
            transform.rotation = currentRotation;
            spine.transform.rotation = transform.rotation;
        }
        if(Input.GetMouseButtonDown(0))
        {
            FireLaser();
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public float maxDistance = 100f; // 레이저 최대 거리
    private void FireLaser()
    {
        // 레이저를 발사하기 위한 라인 캐스트
        RaycastHit hit;
        if (Physics.Raycast(cameraObjects[CameraMode].transform.position, 
            cameraObjects[CameraMode].transform.forward, out hit, maxDistance))
        {
            Debug.Log("레이저가 " + hit.collider.gameObject.name + "에 충돌했습니다.");
            humanEnermy hitAnimation = hit.collider.gameObject.GetComponent<humanEnermy>();
            GameObject g = GameObject.Instantiate(bulletObject, Gun.position, Quaternion.identity);
            g.transform.LookAt(hit.point);
            if (hit.collider.tag == "Enermy")
            {
                if (hitAnimation != null)
                {
                    // HitAnimation 스크립트의 PlayHitAnimation 함수 호출
                    hitAnimation.PlayHitAnimation();
                }
            }
        }
        else
        {
            humanEnermy hitAnimation = hit.collider.gameObject.GetComponent<humanEnermy>();
            GameObject g = GameObject.Instantiate(bulletObject, Gun.position, Quaternion.identity);
            g.transform.LookAt(cameraObjects[CameraMode].transform.position + cameraObjects[CameraMode].transform.forward * 100);
        }
    }

    void LateUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        float speed = h * h + v * v;
        animator.SetFloat("Speed", speed);
        animator.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);

        switch (CameraMode)
        {
            case 0:
                {
                    transform.Rotate(x * xSpeed * Vector3.up, Space.World);
                    spine.transform.Rotate(-y * ySpeed * transform.right, Space.World);
                }
                break;
            case 1:
                {
                    transform.Rotate(x * xSpeed * Vector3.up, Space.World);
                    yRotation -= y * ySpeed;
                    spine.transform.Rotate(yRotation * transform.right, Space.World);
                    Target.transform.RotateAround(transform.position, transform.right, -y * ySpeed); 
                    cameraObjects[CameraMode].transform.LookAt(Target);
                }
                break;
            case 2:
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                        animator.SetBool("Run", true);
                    else if (Input.GetKeyUp(KeyCode.LeftShift))
                        animator.SetBool("Run", false);
                    if (Input.GetKeyDown(KeyCode.Space))
                        animator.SetBool("Dive", true);
                    else if (Input.GetKeyUp(KeyCode.Space))
                    {
                        animator.SetBool("Dive", false);
                        idleTime = 0;
                    }
                    animator.SetFloat("IdleTime", idleTime + Time.deltaTime);
                }
                break;
        }
        currentRotation = transform.rotation;
    }

    void CameraChange(int n)
    {
        idleTime = 0;
        CameraMode = n;
        for (int i = 0; i < 3; i++)
        {
            cameras[i].enabled = false;
        }
        cameras[CameraMode].enabled = true;
        clickTime = 0;
    }

    GUIStyle font = new GUIStyle();
    private void OnGUI()
    {
        font.fontSize = 20;
        font.normal.textColor = Color.white;
        GUI.Label(new Rect(
            Screen.width / 2 - 10,
            Screen.height / 2 - 10,
            Screen.width / 2 + 10,
            Screen.height / 2 + 10), "+", font);
    }
}

