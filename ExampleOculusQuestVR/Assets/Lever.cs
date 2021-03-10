using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Lever : Grabbable
{
    public float activateDegree;

    public bool isActive;

    public Transform handleTop;

    public BallGenerator generator;
    public Action leverPulled = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        leverPulled += generator.drop_ball;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{

        if(follow != null){
            Logger.log("following lever");
            Vector3 force = (follow.position - handleTop.position);
            rb.AddForceAtPosition(force,handleTop.position);
        }


        float currentValue = this.GetComponent<HingeJoint>().angle;
        
        if(currentValue < activateDegree && !isActive)
		{
            Logger.log("dropping ball");
            //activate
            leverPulled();
            isActive = true;
		}

        if(isActive && currentValue >= activateDegree)
		{
            
            isActive = false;
		}

	}
}
