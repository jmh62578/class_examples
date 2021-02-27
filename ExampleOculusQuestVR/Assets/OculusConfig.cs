using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OculusConfig : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        float[] freqs = OVRManager.display.displayFrequenciesAvailable;
        if (Array.Exists(freqs, element => element == 90.0f))
        {
            OVRPlugin.systemDisplayFrequency = 90.0f;
            Time.fixedDeltaTime = 1 / 90.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
