
using System;
using System.Collections;
using UnityEngine;

public class FlareSelfDestroy : MonoBehaviour {

	#region Variables
	#endregion
	
	#region Unity Methods

	void Start () 
	{
		StartCoroutine(DestroyRoutine());
	}	
	
	IEnumerator DestroyRoutine()
	{
		yield return new WaitForSeconds(1f);
		//for (int i = 0; i < 10; i++)
		//{
		//	transform.localScale =(float) (1f - (i/10f)) * new Vector3(1f,1f,1f);			
		//	GetComponent<ParticleSystem>().main.startSizeMultiplier = transform.lossyScale.magnitude;
		//	yield return new WaitForSeconds(0.5f);
		//}
		Destroy(gameObject);
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
