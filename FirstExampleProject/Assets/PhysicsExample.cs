using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int i = 0;
    // Update is called once per frame
    void Update()
    {
        print("In update at time" + Time.time);
    }
    //Fixed update is going to be called exactly N = 1/Time.fixedDeltaTime per second
    void FixedUpdate(){
        print("In fixed update: " + Time.time);
    }
}
