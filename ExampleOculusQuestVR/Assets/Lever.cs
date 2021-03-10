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
    public override void Start()
    {
        leverPulled += generator.drop_ball;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{

        if(follow != null){
            
            
            Vector3 force = 1000*(follow.position - handleTop.position);
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
