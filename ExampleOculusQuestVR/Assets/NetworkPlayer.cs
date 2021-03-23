using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    public Vector3 networkPosition;
    public Quaternion networkRotation;
    Player myController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(photonView.IsMine){
            
            networkPosition = this.transform.position;
            networkRotation = this.transform.rotation;
        }else{
            this.transform.position = Vector3.Lerp(this.transform.position,networkPosition,.05f);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,networkRotation,.05f);
        }
        
    }

    public void assignController(Player p)
	{
        this.myController = p;
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        Debug.Log("serializing " + this.name + " " + stream.IsWriting);
        stream.Serialize(ref networkPosition);
        stream.Serialize(ref networkRotation);
    }
}
