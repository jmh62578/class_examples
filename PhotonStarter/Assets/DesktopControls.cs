using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DesktopControls: MonoBehaviour
{
    Transform grabbed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        if(Input.GetMouseButtonDown(0)){
            Camera cam = GetComponent<Camera>();
            
            Ray r = cam.ScreenPointToRay(mousePos);
            RaycastHit[] hits = Physics.RaycastAll(r,Mathf.Infinity);
            if(hits.Length > 0){
                RaycastHit hit = hits[0];
                PhotonView pv = hit.transform.GetComponent<PhotonView>();
                if(pv != null && !pv.IsMine){
                    pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                    
                }
                grabbed = hit.transform;
            }
        }else if(Input.GetMouseButtonDown(1)){
            Camera cam = GetComponent<Camera>();
            Ray r = cam.ScreenPointToRay(mousePos);
            RaycastHit[] hits = Physics.RaycastAll(r,Mathf.Infinity);
            if(hits.Length > 0){
                RaycastHit hit = hits[0];
                PhotonView pv = hit.transform.GetComponent<PhotonView>();
                if(pv != null){
                    pv.RPC("rpcTakeDamage",RpcTarget.All,10.0f);;
                    Debug.Log("Sent damage message");
                }

            }
        }
        if(grabbed != null){
            //make the object follow the mouse
            grabbed.position = mousePos/1000.0f;
        }
        if(!Input.GetMouseButton(0)){
            grabbed = null;
        }
        
    }
}
