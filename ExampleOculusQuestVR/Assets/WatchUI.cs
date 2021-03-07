using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WatchUI : MonoBehaviour
{
    public Text watchText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setWatchText(Time.time + "");
    }

    public void setWatchText(string text)
	{
        watchText.text = text;
	}
}
