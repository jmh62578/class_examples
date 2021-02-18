using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    Transform follow;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void grab(Transform by)
	{
        follow = by;
	}
    public void release()
	{
        follow = null;
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
}
