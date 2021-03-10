using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainedGrabbable : Grabbable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        if (follow != null)
        {
            Vector3 between = follow.position - rb.position;

            rb.AddForce(between * 100);
            
        }
    }
}
