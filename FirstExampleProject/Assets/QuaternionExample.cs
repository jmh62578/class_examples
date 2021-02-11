using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionExample : MonoBehaviour
{
    public Transform A;
    public Transform B;
    // Start is called before the first frame update
    public float rotation_time;
    public bool useEuler;
    // Update is called once per frame
    void Update()
    {
        
        float t = Time.time;
        float pos = (Mathf.Sin(2*Mathf.PI*t/rotation_time)+1)/2.0f;
        if(!useEuler){
            transform.rotation = Quaternion.Slerp(A.rotation,B.rotation,pos);
        }else{
            Vector3 newEuler = Vector3.Lerp(A.rotation.eulerAngles,B.rotation.eulerAngles,pos);
            transform.rotation = Quaternion.Euler(newEuler);
        }
        transform.position = Vector3.Lerp(A.position,B.position,pos);
    }
}
