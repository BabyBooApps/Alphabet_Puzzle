using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparklesbursts : MonoBehaviour {
    public GameObject leftObject;
    public GameObject rightObject;

	
	void Start () {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
      //  Debug.Log(stageDimensions);
        Vector3 rightPos = new Vector3(stageDimensions.x-0.5f, -stageDimensions.y+0.5f, 0);
        Vector3 leftPos = new Vector3(-stageDimensions.x+0.5f, -stageDimensions.y+0.5f, 0);

        //Debug.Log(leftPos);
        leftObject.transform.position = leftPos;
        rightObject.transform.position = rightPos;


    }
	
	
}
