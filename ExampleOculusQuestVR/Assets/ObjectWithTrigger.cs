using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithTrigger : Grabbable
{
    public override void handleTrigger(float v)
	{
		v = Mathf.Max(v, .001f);
		this.transform.localScale = new Vector3(v, v, v);
	}
}
