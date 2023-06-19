using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermyMaker : MonoBehaviour
{
    public GameObject child;
    public Vector3[] childObjectPosition;
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] temp = {
            transform.GetChild(0).position,
            transform.GetChild(1).position,
            transform.GetChild(2).position,
            transform.GetChild(3).position };
        childObjectPosition = temp;
    }

    private void OnChildDestroyed(GameObject childObject)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 4)
        {
             Instantiate(child,
                 childObjectPosition[Random.Range(0, childObjectPosition.Length)], 
                 Quaternion.identity, transform);
        }
    }
}
