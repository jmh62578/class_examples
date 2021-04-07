using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Logger : MonoBehaviour
{
    static Logger logger;
    public Text debugText;
    // Start is called before the first frame update
    void Awake(){
        logger = this;
    }
    public static void log(string text){
        logger.debugText.text = text + "\n" + logger.debugText.text;
        if(logger.debugText.text.Length > 5000){
            logger.debugText.text = logger.debugText.text.Substring(0,2500);
        }
        Debug.Log(text);
    }
}
