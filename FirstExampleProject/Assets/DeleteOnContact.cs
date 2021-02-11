using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DeleteOnContact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision){
        //Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider collider){
        Renderer[] renderers =  this.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers){
            r.material.color = Color.red;
            Debug.Log("here");
        }
    
        
    }
}
