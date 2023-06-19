using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletparticle : MonoBehaviour
{
    private float speed = 0.25f;
    UnityEngine.Color[] colors = null;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        UnityEngine.Color[] temp = { new UnityEngine.Color(0, 0, 0),
            new UnityEngine.Color(132 / 255f, 107 / 255f, 78 / 255f)};
        colors = temp;
        UnityEngine.Color randomColor = colors[Random.Range(0, colors.Length)];
        GetComponent<Renderer>().material.color = randomColor;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
