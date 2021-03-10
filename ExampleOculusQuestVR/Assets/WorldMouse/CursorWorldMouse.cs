using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorWorldMouse : WorldMouse
{
    public Camera myCamera;
    public override bool pressDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public override bool pressUp()
    {
        return Input.GetMouseButtonUp(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        Ray r = myCamera.ScreenPointToRay(Input.mousePosition);
        
        this.transform.position = myCamera.transform.position;
        this.transform.forward = r.direction;
        base.Update();
        // Debug.Log(Input.mousePosition);
    }
}
