using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using System;
using Photon.Pun;
public class BallGenerator : MonoBehaviour
{
    public GameObject ballPrefab;
    public Camera raycastCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void drop_ball()
	{
        PhotonNetwork.Instantiate("ball", this.transform.position,
                this.transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.RawButton.A))
		{
            drop_ball();


		}

		if (Input.GetMouseButtonDown(0))
		{
            //Ray r = new Ray(raycastCamera.transform.position,)
            Ray r = raycastCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(r);
            if(hits.Length > 0)
			{
                Instantiate<GameObject>(
                ballPrefab,
                hits[0].point + new Vector3(0, this.transform.position.y, 0),
                this.transform.rotation,
                this.transform); 
            }
		}
    }
};
