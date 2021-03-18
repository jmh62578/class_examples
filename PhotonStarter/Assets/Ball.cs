using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Ball : MonoBehaviourPun, IPunObservable
{

    public float health;
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
            
            networkPosition = this.transform.position;
            networkRotation = this.transform.rotation;

            
        }else{
            this.transform.position = Vector3.Lerp(this.transform.position,networkPosition,.05f);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,networkRotation,.05f);
        }

        this.transform.localScale = Vector3.one * health/100;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){


        Debug.Log("serializing " + this.name + " " + stream.IsWriting);
        stream.Serialize(ref networkPosition);
        stream.Serialize(ref networkRotation);
        stream.Serialize(ref health);
    }
    [PunRPC]
    void rpcTakeDamage(float damage,PhotonMessageInfo info){
        if(photonView.IsMine){
            this.health-=damage;
            if(this.health <= 0){
                PhotonNetwork.Destroy(this.photonView);
            }
        }
    }
}
