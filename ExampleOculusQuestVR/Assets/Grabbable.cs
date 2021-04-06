﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using Photon.Pun;
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviourPun, IPunObservable
{
    protected Transform follow;
    protected Rigidbody rb;

    Vector3 networkPosition;
    Quaternion networkRotation;
    Vector3 networkVelocity;
    Vector3 networkAngularVelocity;
    bool networkUseGravity;
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = Mathf.Infinity;

    }

    // Update is called once per frame
    void Update()
    {
		if (photonView.IsMine)
		{
            networkPosition = rb.position;
            networkRotation = rb.rotation;
            networkVelocity = rb.velocity;
            networkAngularVelocity = rb.angularVelocity;
            networkUseGravity = rb.useGravity;
		}
		else
		{
            if(follow != null)
			{
                release();
			}
            rb.position = networkPosition;
            rb.velocity = networkVelocity;
            rb.rotation = networkRotation;
            rb.angularVelocity = networkAngularVelocity;
            rb.useGravity = networkUseGravity;
		}

    }

    public virtual void handleTrigger(float v)
    {

    }

    public virtual void grab(Transform by)
    {
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        rb.useGravity = false;
        follow = by;
    }
    public virtual void release()
    {
        if (this.photonView.IsMine)
        {
            rb.useGravity = true;
            follow = null;
        }

    }
    private void FixedUpdate()
    {
        if (follow != null)
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
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        stream.Serialize(ref networkPosition);
        stream.Serialize(ref networkRotation);
        stream.Serialize(ref networkVelocity);
        stream.Serialize(ref networkAngularVelocity);
        stream.Serialize(ref networkUseGravity);
    }
}
