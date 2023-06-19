using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermy : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject particle;
    public Rigidbody rigidbody;
    float speed = 0.5f;
    float power = 250f;
    int bulletLayer = 8;
    bool dead = false;

    public delegate void OnDestroyDelegate();
    public OnDestroyDelegate OnDestroyed;

    private void OnDestroy()
    {
        if (OnDestroyed != null)
            OnDestroyed();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        bulletLayer = LayerMask.NameToLayer("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTransform = GameObject.Find("Tank") == null ?
            GameObject.Find("Capsule").transform :
            GameObject.Find("Tank").transform;
        if (GameObject.Find("Tank") == null)
            power = 70f;
        else
            power = 250f;

        if (!dead)
        {
            transform.LookAt(PlayerTransform.position);
            if ((transform.position - PlayerTransform.position).magnitude > 3)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }

    private IEnumerator Bomb()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 30; i++)
            Instantiate<GameObject>(particle, transform.position, Quaternion.LookRotation(Random.onUnitSphere));
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == bulletLayer)
        {
            Debug.Log("사격 성공");
            dead = true;
            rigidbody.freezeRotation = false;
            Vector3 knockbackDirection = collision.GetContact(0).normal;
            rigidbody.AddForce(knockbackDirection * 10f, ForceMode.Impulse);
            rigidbody.AddForce(Vector3.up * power);
            StartCoroutine(Bomb());
        }
    }

    
    
}
