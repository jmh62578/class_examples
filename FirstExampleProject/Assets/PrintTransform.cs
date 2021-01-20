using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrintTransform : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Matrix4x4 toParent = this.transform.parent.worldToLocalMatrix * transform.localToWorldMatrix;
        text.text = toParent + "";
    }
}
