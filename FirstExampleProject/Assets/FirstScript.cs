using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FirstScript : MonoBehaviour
{
    
    private void Start() //called once at the beginning (when you hit play, OR the object is first instantiated)
    {
        
    }
    void Update() //called once per frame
    {

        transform.Rotate(100*Time.deltaTime, 0, 0,Space.World);
       
    }
    private void FixedUpdate()
    {
    }

}
