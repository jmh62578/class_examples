using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip standardBounce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnCollisionEnter(Collision collision)
	{
        
            float volume = this.GetComponent<Rigidbody>().velocity.magnitude / 10.0f;
            AudioSource.PlayClipAtPoint(
                standardBounce,
                this.transform.position,
                volume);
            //audio.volume = 
            //audio.Play();
        
	}
}
