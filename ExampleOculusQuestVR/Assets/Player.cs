using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public Hand leftHand;
    public Hand rightHand;
    public Transform head;
    public Transform playSpace;
    public enum LOCOMODE { FLYING, DRIVING, TELEPORTING }
    public LOCOMODE movementTechnique;

    public GameObject desktopCamera;
    
    // Start is called before the first frame update
    bool rightTeleporterActive = false;
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android){
            desktopCamera.SetActive(false);
           

        }else{
            
        }
    }

    

    Vector3 getWorldFootPosition()
	{
        Vector3 headPosWorld = head.position;
        Vector3 headPosPlay = playSpace.worldToLocalMatrix.MultiplyPoint(headPosWorld);
        //Vector3 headPosPlay = playSpace.InverseTransformPoint(headPosWorld);
        Vector3 footPosPlay = headPosPlay;
        footPosPlay.y = 0;
        Vector3 footPosWorld = playSpace.localToWorldMatrix.MultiplyPoint(footPosPlay);

        return footPosWorld;
    }
    public void teleport(Vector3 targetFootPos)
	{
        Vector3 footPosWorld = getWorldFootPosition();
        Vector3 offsetFootWorld = targetFootPos - footPosWorld;
        playSpace.Translate(offsetFootWorld, Space.World);
	}

    public void rotate(float angleDegrees)
	{
        Vector3 saveFootPos = getWorldFootPosition();
        playSpace.Rotate(0, angleDegrees, 0, Space.World);
        teleport(saveFootPos);
	}

    // Update is called once per frame
    void Update()
    {
        float ls = leftHand.getSpeedControl();
        float rs = rightHand.getSpeedControl();

        

        float s = Mathf.Clamp(ls + rs,-1,1);

		//flying
		switch (movementTechnique)
		{
            case LOCOMODE.FLYING:
                {
                    playSpace.Translate(s * head.forward * Time.deltaTime,Space.World);
                    break;
                }
            case LOCOMODE.DRIVING:
				{
                    Vector3 intendedMotion = s * head.forward * Time.deltaTime;
                    intendedMotion.y = 0;
                    playSpace.Translate(intendedMotion, Space.World);
                    break;
				}
            case LOCOMODE.TELEPORTING:
				{

                    break;
				}
        }
        

        //
    }

    
}
