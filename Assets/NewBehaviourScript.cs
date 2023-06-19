using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{ 
    Coroutine do1, do2, do3;

    // Start is called before the first frame update
    void Start()
    {
        do1 = StartCoroutine("DoSomething", (1.0f));
    }

    IEnumerable DoSomething(float waitTime)
    {
        print(string.Format("Start Coroutine Dosomething({0})", waitTime));
        while(true)
        {
            print(string.Format("DoSomething()::Frames:{0}", Time.frameCount));
            yield return new WaitForSeconds(waitTime);
            print(string.Format("After yield return WaitForSeconds({0})", waitTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(string.Format("Update()::Frames:{0}", Time.frameCount));
        if (Input.GetKey(KeyCode.Alpha2)) do2 = StartCoroutine("DoSomething", 2.0f);
        if (Input.GetKey(KeyCode.Alpha3)) do3 = StartCoroutine("DoSomething", 3.0f);
    }
}
