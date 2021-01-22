using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class ViewProjectionMatrix : MonoBehaviour
{
    Camera c;
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        t.text = c.projectionMatrix.ToString();
    }
}
