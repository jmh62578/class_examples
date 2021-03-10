using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Lever : MonoBehaviour
{
    public float activateDegree;

    public bool isActive;

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
        float currentValue = this.transform.localRotation.eulerAngles.x;
        if(currentValue < activateDegree && !isActive)
		{
            Debug.Log("dropping ball");
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
