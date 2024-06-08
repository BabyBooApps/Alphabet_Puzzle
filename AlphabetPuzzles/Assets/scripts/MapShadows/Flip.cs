using UnityEngine;
using System.Collections;
using System;

public class Flip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void OnMouseDown () {
        //transform.localEulerAngles = transform.eulerAngles + new Vector3(0, 180, 0);
        // Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;

        // iTween.RotateTo(gameObject, new Vector3(0, 90, 0), 1f);

        StartCoroutine(Flipcard());


    }

    IEnumerator Flipcard()
    {
        iTween.RotateTo(gameObject, new Vector3(0, 90, 0), 1f);
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("alphacards/1/DI");
        iTween.RotateTo(gameObject, new Vector3(0, 0, 0), 1f);

    }


    private void OnDestroy()
    {
        try
        {
            StopAllCoroutines();
        }
        catch (Exception)
        { }
    }

}
