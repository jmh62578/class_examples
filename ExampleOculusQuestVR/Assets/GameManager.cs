using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Transform startLocation;
    // Start is called before the first frame update
    void Start()
    {
        player.teleport(startLocation.position);
        //StartCoroutine(RotateOften(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RotateOften(float time)
	{
		while (true)
		{
            player.rotate(10);
            yield return new WaitForSeconds(time);
		}
	}

    public void handleGogoSliderChange(float v){
        player.setGogoScaling(v);
    }
}
