//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.Purchasing;
//using UnityEngine.UI;

//public class IAPStatusScript : MonoBehaviour
//{

//	//[Tooltip("[Optional] Displays the localized title from the app store")]
//	//public Text titleText;

//	[Tooltip("[Optional] Displays the status of Product")]
//	public Text statusText;


//	// Use this for initialization
//	void Start()
//	{
//		StartCoroutine(CheckRemoveAdsStatus());
//	}

//	private int attemptNo = 0;

//	IEnumerator CheckRemoveAdsStatus()
//	{
//		while (IAPController.removeAdsStatus == IAPController.IAPStatus.UNKNOWN)
//		{
//			attemptNo++;
//			if (statusText != null)
//			{
//				//statusText.text = "status unknown : " + attemptNo.ToString();
//				statusText.text = attemptNo + " : fetching data...";
//			}
//			yield return new WaitForSeconds(1f);
//		}

//		if (IAPController.removeAdsStatus == IAPController.IAPStatus.PURCHASED)
//		{
//			if (statusText != null)
//			{
//				statusText.text = "purchased";
//			}
//		}
//		else if (IAPController.removeAdsStatus == IAPController.IAPStatus.NOT_PURCHASED)
//		{
//			if (statusText != null)
//			{
//				statusText.text = "not purchased";
//			}
//		}
//	}

//    private void OnDestroy()
//    {
//        try
//        {
//            StopAllCoroutines();
//        }
//        catch (Exception)
//        { }
//    }
//}
