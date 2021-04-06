using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    public Vector3 headPosition;
    public Quaternion headRotation;

    public Vector3 leftHandPosition;
    public Quaternion leftHandRotation;
    public bool leftHandGrabbing;

     public Vector3 rightHandPosition;
    public Quaternion rightHandRotation;
    public bool rightHandGrabbing;

    Player myController;
    public Transform head;
    public Transform body;
    public Transform leftHand;
    public Transform rightHand;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine){
            head.GetChild(0).gameObject.SetActive(false);
            body.GetChild(0).gameObject.SetActive(false);
            leftHand.GetChild(0).gameObject.SetActive(false);
            rightHand.GetChild(0).gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void copyTransform(Transform t1, Transform t2){
        t1.position = t2.position;
        t1.rotation = t2.rotation;
        
    }
    void Update()
    {

        if(photonView.IsMine){
            


            if(myController != null){
                copyTransform(head.transform,myController.head);
                copyTransform(leftHand.transform,myController.leftHand.transform);
                copyTransform(rightHand.transform,myController.rightHand.transform);
            }

            headPosition = head.position;
            headRotation = head.rotation;
            leftHandPosition = leftHand.position;
            leftHandRotation = leftHand.rotation;
            rightHandPosition = rightHand.position;
            rightHandRotation = rightHand.rotation;
            leftHandGrabbing = myController.leftHand.handGraphics.gameObject.activeSelf;
            rightHandGrabbing = myController.rightHand.handGraphics.gameObject.activeSelf;

        }else{
            head.position = Vector3.Lerp(head.position,headPosition,.05f);
            head.rotation = Quaternion.Slerp(head.rotation,headRotation,.05f);

            leftHand.position = Vector3.Lerp(leftHand.position,leftHandPosition,.05f);
            leftHand.rotation = Quaternion.Slerp(leftHand.rotation,leftHandRotation,.05f);

            rightHand.position = Vector3.Lerp(rightHand.position,rightHandPosition,.05f);
            rightHand.rotation = Quaternion.Slerp(rightHand.rotation,rightHandRotation,.05f);

            leftHand.gameObject.SetActive(leftHandGrabbing);
            rightHand.gameObject.SetActive(rightHandGrabbing);
        }
        
    }

    public void assignController(Player p)
	{
        this.myController = p;
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        Debug.Log("serializing " + this.name + " " + stream.IsWriting);
        stream.Serialize(ref headPosition);
        stream.Serialize(ref headRotation);
        stream.Serialize(ref leftHandPosition);
        stream.Serialize(ref leftHandRotation);
        stream.Serialize(ref rightHandPosition);
        stream.Serialize(ref rightHandRotation);
        stream.Serialize(ref leftHandGrabbing);
        stream.Serialize(ref rightHandGrabbing);
    }
}
