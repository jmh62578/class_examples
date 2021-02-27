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
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        

        //
    }

    
}
