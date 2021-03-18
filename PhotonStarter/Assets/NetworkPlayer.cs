using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    public Vector3 networkPosition;
    public Quaternion networkRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(photonView.IsMine){
            //set the network position (really all network variables should be set here)
            if(Input.GetKey(KeyCode.UpArrow)){
               this.transform.position += Vector3.up*Time.deltaTime; 
            }
            if(Input.GetKey(KeyCode.DownArrow)){
                this.transform.position -= Vector3.up*Time.deltaTime;
            }
            networkPosition = this.transform.position;
            networkRotation = this.transform.rotation;
        }else{
            this.transform.position = Vector3.Lerp(this.transform.position,networkPosition,.05f);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,networkRotation,.05f);
        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        Debug.Log("serializing " + this.name + " " + stream.IsWriting);
        stream.Serialize(ref networkPosition);
        stream.Serialize(ref networkRotation);
    }
}
