using System;
using System.Collections;
using UnityEngine;

public class DotFadeIn : MonoBehaviour
{

	#region Variables
	Color imgColor;
	SpriteRenderer renderer;
	#endregion

	#region Unity Methods

	void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
		imgColor = renderer.color;
		imgColor.a = 0;
		GetComponent<SpriteRenderer>().color = imgColor;

		StartCoroutine(FadeInRoutine());
	}

	IEnumerator FadeInRoutine()
	{
		imgColor = renderer.color;
		for (int i = 0; i < 11; i++)
		{
			imgColor.a = 0.1f * i;
			renderer.color = imgColor;
			yield return new WaitForSeconds(0.05f);
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

    #endregion
}
