using UnityEngine;
using System.Collections;
using System;

public class Fadeout : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(setAlpha());
    }

    IEnumerator setAlpha()
    {
        for (int i = 0; i < 11; i++)
        {
            Color objcolor = gameObject.GetComponent<SpriteRenderer>().color;
            objcolor.a = 1-(0.1f * i);
            gameObject.GetComponent<SpriteRenderer>().color = objcolor;
            yield return new WaitForSeconds(0.02f);
        }

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
