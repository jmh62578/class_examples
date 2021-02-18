using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    public Transform follow;
    Rigidbody rb;
    public enum HAND_SIDE { LEFT, RIGHT }
    public HAND_SIDE side;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = Mathf.Infinity;
        if(side == HAND_SIDE.LEFT)
		{
            GetComponent<Renderer>().material.color = Color.red;
        }
		else
		{
            GetComponent<Renderer>().material.color = Color.blue;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
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

        RaycastHit[] hits = Physics.SphereCastAll(rb.position, 0.5f, Vector3.forward,0.0f);
        //for (int i = 0; i < hits.>)
    }   
}
