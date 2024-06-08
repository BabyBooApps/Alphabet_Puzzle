using UnityEngine;

public class SparklesTopFlow : MonoBehaviour {

    public GameObject leftObject;
    public GameObject rightObject;

    void Start()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //  Debug.Log(stageDimensions);
        Vector3 rightPos = new Vector3(stageDimensions.x, stageDimensions.y, 0);
        Vector3 leftPos = new Vector3(-stageDimensions.x, stageDimensions.y, 0);

        //Debug.Log(leftPos);
        leftObject.transform.position = leftPos;
        rightObject.transform.position = rightPos;


    }
}
