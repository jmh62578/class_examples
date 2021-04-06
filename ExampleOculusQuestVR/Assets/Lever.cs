using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

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

        leverPulled += doPullLever;
        base.Start();
    }

    public void doPullLever()
	{
        if (this.photonView.IsMine) {
            generator.drop_ball();
        };
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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

	//public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	//{
	//	base.OnPhotonSerializeView(stream, info);
	//	//if (photonView.IsMine)
	//	//{

 // //          stream.SendNext(rb.GetComponent<HingeJoint>().angle);
	//	//}
	//	//else
	//	//{
 // //          rb.GetComponent<HingeJoint>().angle = (float) stream.ReceiveNext();
	//	//}
	//}
}
