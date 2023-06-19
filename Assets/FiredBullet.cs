using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredBullet : MonoBehaviour
{
    public float speed = 12.0f;
    int wallLayer = 7;
    int enermyLayer = 6;
    Rigidbody rigidbody;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        wallLayer = LayerMask.NameToLayer("Wall");
        enermyLayer = LayerMask.NameToLayer("Enermy");
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("´ê¾ÒÀ½");

        for (int i = 0; i < 30; i++)
        {
            GameObject go = Instantiate<GameObject>(particle, transform.position, Quaternion.LookRotation(Random.onUnitSphere));
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(-go.transform.forward * 0.5f);
        }
        GameObject.Destroy(gameObject);

        //Debug.Log(other.gameObject + " : " + other.gameObject.layer);
        if (other.gameObject.layer == wallLayer)
        {
            GameObject.Destroy(gameObject, 0.0f);
            //Debug.Log("Triggered");
        }
        if (other.gameObject.layer == enermyLayer)
        {
            GameObject.Destroy(gameObject, 0.0f);
            Debug.Log("ÃÑ¾Ë ¸Â¾ÒÀ½");
        }
    }
}
