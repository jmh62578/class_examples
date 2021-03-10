using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class WorldMouse : MonoBehaviour {


	public float rayDistance;
	public float worldRayDistance;
	public Vector2 textureHitPoint;
	public Vector3 worldHitPoint;
	public GameObject hitGo;
	public Collider hitCollider;
	
	protected virtual void Update()
	{

		worldRayDistance = Mathf.Infinity;
		RaycastHit hit;
		if(Physics.Raycast(new Ray(transform.position, transform.forward),out hit))
		{
			worldRayDistance = hit.distance;
			worldHitPoint = hit.point;
			textureHitPoint = hit.textureCoord;
			hitCollider = hit.collider;
			if (hit.collider.attachedRigidbody != null) {
				hitGo = hit.collider.attachedRigidbody.gameObject;
			}
			else {
				hitGo = hit.transform.gameObject;
			}
		}
		else {
			hitGo = null;
		}
	}


	public abstract bool pressDown();

	public abstract bool pressUp();
}
