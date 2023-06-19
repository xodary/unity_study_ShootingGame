using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class particle : MonoBehaviour
{
    private float speed = 1;
    UnityEngine.Color[] colors = null;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        UnityEngine.Color[] temp = { new UnityEngine.Color(255 / 255f, 108 / 255f, 83 / 255f),
            new UnityEngine.Color(77 / 255f, 77 / 255f, 77 / 255f)};
        colors = temp;
        UnityEngine.Color randomColor = colors[Random.Range(0, colors.Length)];
        GetComponent<Renderer>().material.color = randomColor;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
