using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
[RequireComponent(typeof(Rigidbody))]
public class Hand : WorldMouse
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
    public float snapRotateAmountDegrees = 25;
    bool canActivateSnap = true;
    public GameObject teleporterLinePrefab;
    public GameObject teleporterTarget;
    List<GameObject> teleporterPoints = new List<GameObject>();
    public int maxTeleporterPoints = 50;
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
    protected override void Update()
    {
        worldRayDistance = Mathf.Infinity;
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit))
        {
            worldRayDistance = hit.distance;
            worldHitPoint = hit.point;
            textureHitPoint = hit.textureCoord;
            hitCollider = hit.collider;
            if (hit.collider.attachedRigidbody != null)
            {
                hitGo = hit.collider.attachedRigidbody.gameObject;
            }
            else
            {
                hitGo = hit.transform.gameObject;
            }
        }
        else
        {
            hitGo = null;
        }
        
        float laserDistance = Mathf.Min(worldRayDistance, rayDistance);
        if(laserDistance < Mathf.Infinity)
		{
            laser.SetActive(true);
		}
        laser.transform.localScale = new Vector3(1, 1, laserDistance);

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
        teleporterTarget.SetActive(false);
        foreach(GameObject g in teleporterPoints){
                GameObject.Destroy(g);
            }
            
        teleporterPoints.Clear();
        if(thumbstick.y > .5f)
		{
            //draw teleporter line
            //laser.SetActive(true);

            

            //simulate the teleporter forward, starting at the laser position, 
            //and ending when the teleporter line hits something

            Vector3 currentPosition = laser.transform.position;
            Vector3 currentVelocity = laser.transform.forward*6.0f;
            float dt = .03f;
            for(int i=0;i<maxTeleporterPoints;i++){
                Vector3 nextPoint = currentPosition + currentVelocity*dt;
                int layerMask =~(1 << LayerMask.NameToLayer ("Player"));
                RaycastHit[] hits = Physics.RaycastAll(currentPosition, currentVelocity.normalized, (nextPoint-currentPosition).magnitude,layerMask);
                bool teleporterHit = false;
                if(hits.Length > 0)
                {
                    teleporterTarget.SetActive(true);
                    teleportLocation = hits[0].point;
                    teleporterTarget.transform.position = teleportLocation;
                    teleporterTarget.transform.up = teleportNormal;
                    teleportNormal = hits[0].normal;
                    teleporterHit = true;
                    nextPoint = teleportLocation;
                }
                GameObject line = GameObject.Instantiate<GameObject>(teleporterLinePrefab);
                line.layer = LayerMask.NameToLayer("Player");
                line.transform.position = currentPosition;
                line.transform.forward = currentVelocity.normalized;
                line.transform.localScale = new Vector3(1,1,
                    (nextPoint-currentPosition).magnitude
                );
                currentVelocity = currentVelocity + Physics.gravity*dt;
                currentPosition = nextPoint;
                teleporterPoints.Add(line);
                if(teleporterHit){
                    break;
                }
            }


            // RaycastHit[] hits = Physics.RaycastAll(laser.transform.position, laser.transform.forward, Mathf.Infinity);
            // if(hits.Length > 0)
			// {
            //     laser.transform.localScale = new Vector3(1,1,
            //     (hits[0].point - laser.transform.position).magnitude);
            //     teleportLocation = hits[0].point;
            //     teleportNormal = hits[0].normal;
			// }
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

        //handle snap rotation
        if(canActivateSnap && Mathf.Abs(thumbstick.x) > .5f){
            player.rotate(snapRotateAmountDegrees * Mathf.Sign(thumbstick.x));
            canActivateSnap = false;
        }else if(!canActivateSnap && Mathf.Abs(thumbstick.x) <= .5f){
            canActivateSnap = true;
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

    public override bool pressDown()
    {
        return OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, myHand);

    }
    public override bool pressUp()
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, myHand);
    }


}
