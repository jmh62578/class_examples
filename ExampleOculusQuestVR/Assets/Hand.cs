using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
[RequireComponent(typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    public Transform follow;
    Rigidbody rb;
    public enum HAND_SIDE { LEFT, RIGHT }
    public HAND_SIDE side;
    Grabbable grabbed=null;
    OVRInput.Controller myHand;
    public float gripAtPercentage;
    public float releaseAtPercentage;
    public GameObject handGraphics;
    public bool teleporterActive;
    public Player player;
    public Vector3 teleportLocation;
    public Vector3 teleportNormal;
    public GameObject laser;
    // Start is called before the first frame update
    void Awake()
    {
        teleporterActive = false;
        laser.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = Mathf.Infinity;
        if(side == HAND_SIDE.LEFT)
		{
            handGraphics.GetComponent<Renderer>().material.color = Color.red;
            myHand = OVRInput.Controller.LTouch;
        }
		else
		{
            handGraphics.GetComponent<Renderer>().material.color = Color.blue;
            myHand = OVRInput.Controller.RTouch;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float handTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, myHand);
        if (handTrigger < releaseAtPercentage && grabbed != null)
		{
            grabbed.release();
            grabbed = null;
            StartCoroutine(setHandToSolid(1));
		}
        float indexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, myHand);
        if(grabbed != null)
		{
            grabbed.handleTrigger(indexTrigger);
		}

        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, myHand);

        if(thumbstick.y > .5f)
		{
            //draw teleporter line
            laser.SetActive(true);
            RaycastHit[] hits = Physics.RaycastAll(laser.transform.position, laser.transform.forward, Mathf.Infinity);
            if(hits.Length > 0)
			{
                
                teleportLocation = hits[0].point;
                teleportNormal = hits[0].normal;
			}
            teleporterActive = true;
		}
		else
		{
			if (teleporterActive)
			{
                //ask to do the teleportation
                if (teleportNormal.y > .5f)
                {
                    player.teleport(teleportLocation);
                }
                teleporterActive = false;
			}
		}
    }

    public float getSpeedControl()
	{
        return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, myHand).y;
	}

    IEnumerator setHandToSolid(float t)
	{
        yield return new WaitForSeconds(t);
        if (grabbed == null)
        {
            handGraphics.SetActive(true);
        }
	}
	private void FixedUpdate()
	{
        Vector3 between = follow.position - rb.position;
        rb.velocity = between / Time.deltaTime;

        Quaternion betweenRot = follow.rotation * Quaternion.Inverse(rb.rotation);
        float angle;
        Vector3 axis;
        betweenRot.ToAngleAxis(out angle, out axis);
        Vector3 av = axis * Mathf.Deg2Rad * angle;
        rb.angularVelocity = av / Time.deltaTime;

        
    }

	private void OnTriggerStay(Collider other)
	{
        float handTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger,myHand);
        Grabbable g = other.GetComponent<Grabbable>();
		if (g == null) { return; } //we know it's a grabbable
        if(handTrigger > gripAtPercentage && grabbed == null)
		{
            grabbed = g;
            handGraphics.SetActive(false);
            g.grab(this.follow);
		}
	}


}
