using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentalControlScript : MonoBehaviour
{
	public GameObject parentalControlDialog;
	[Space]
	public Text questionText;
	public GameObject[] optionObjects = new GameObject[3];
	//public GameObject tongueObject;
	public GameObject PurchaseBtnObject;
	[Space]
	public AudioClip wrongAnswerClip;
	[Space]
	public GameObject successMsgObject;

	public static bool answered = false;

	int firstNo = 0;
	int secondNo = 0;
	int answer = 0;
	int answerObjIndex = 0;
	List<string> optionsList = new List<string>();
	System.Random random = new System.Random();

	//private Vector3 tongueInitialPos;

	private void Awake()
	{
		parentalControlDialog.SetActive(false);
	}


	private void Start()
	{
		//tongueInitialPos = tongueObject.transform.position;
	}


	public void DisplayParentalControlDialog()
	{
		PurchaseBtnObject.SetActive(false);
		GenerateQuestion();
		parentalControlDialog.SetActive(true);
	}


	public void HideParentalControlDialog()
	{
		parentalControlDialog.SetActive(false);
		answered = false;
		//tongueObject.transform.position = tongueInitialPos;
	}


	void GenerateQuestion()
	{
		int maxNo = 5;
		firstNo = random.Next(1, maxNo);
		secondNo = 0;
		answer = 0;
		secondNo = random.Next(1, maxNo);
		answer = firstNo + secondNo;

		questionText.text = firstNo.ToString() + "  +  " + secondNo.ToString() + " =  ?";
		questionText.gameObject.transform.localScale = new Vector3(1, 1, 1);
		iTween.ScaleFrom(questionText.gameObject, new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
		PopulateScene();
	}


	void PopulateScene()
	{
		string[] answerNos = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
											"11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
		optionsList = new List<string>();

		List<string> dummyList = new List<string>();
		dummyList.AddRange(answerNos);

		if (answer < 10)
		{
			optionsList.AddRange(dummyList.GetRange(0, 10));
		}
		else
		{
			int maxNo = GetMax(firstNo, secondNo);
			optionsList.AddRange(dummyList.GetRange(maxNo, (20 - maxNo + 1)));
		}

		CreateAnswerObject();
		CreateOtherObjects();
	}


	private int GetMax(int first, int second)
	{
		return first > second ? first : second;
	}


	private int GetMin(int first, int second)
	{
		return first < second ? first : second;
	}


	void CreateAnswerObject()
	{
		answerObjIndex = random.Next(0, 3);

		GameObject optionObj = optionObjects[answerObjIndex];

		optionObj.GetComponentInChildren<Text>().text = answer.ToString();

		if (answer < 10)
		{
			optionsList.RemoveAt(answer);
		}
		else
		{
			optionsList.RemoveAt(GetMin(firstNo, secondNo));
		}
		optionObj.name = "answer";
	}


	void CreateOtherObjects()
	{
		int minOption = 0;
		if (answer < 10)
		{
			minOption = 1;
		}
		for (int i = 0; i < 3; i++)
		{
			if (i != answerObjIndex)
			{
				GameObject optionObj = optionObjects[i];
				int optionIndex = random.Next(minOption, optionsList.Count);
				optionObj.GetComponentInChildren<Text>().text = optionsList[optionIndex];
				optionsList.RemoveAt(optionIndex);
			}
			optionObjects[i].transform.localScale = new Vector3(1, 1, 1);
			iTween.ScaleFrom(optionObjects[i], new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
		}
	}


	public void OptionClicked(Text optionText)
	{
		if (!answered)
		{
			if (optionText.text.ToString().Equals(answer.ToString()))
			{
				answered = true;
				//iTween.MoveTo(tongueObject, tongueInitialPos + new Vector3(0f, -160f, 0f), 0.8f);
				PurchaseBtnObject.SetActive(true);
				iTween.ScaleFrom(PurchaseBtnObject, new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
				//SlideIn();
			}
			else
			{
				GetComponent<AudioSource>().PlayOneShot(wrongAnswerClip);
				answered = false;
				GenerateQuestion();
			}
		}
	}


	/*
	public RectTransform uGuiElement;
	Vector2 targetPosition;

	public void SlideIn()
	{
		targetPosition = uGuiElement.anchoredPosition + new Vector2(0f, -160f);

		iTween.ValueTo(uGuiElement.gameObject, iTween.Hash(
			"from", uGuiElement.anchoredPosition,
			"to", targetPosition,
			"time",0.5f,
			"onupdatetarget", this.gameObject,
			"easetype",iTween.EaseType.easeOutSine, //easeInBack
			"onupdate", "MoveGuiElement"));
	}


	public void MoveGuiElement(Vector2 position)
	{
		uGuiElement.anchoredPosition = position;
	}
	*/

	public void SuccessMessage()
	{
		//tongueObject.transform.position = tongueInitialPos;
		questionText.text = "";
		for (int i = 0; i < optionObjects.Length; i++)
		{
			optionObjects[i].SetActive(false);
		}
		answered = false;
		PurchaseBtnObject.SetActive(false);
		successMsgObject.SetActive(true);
		Invoke("DiminishParentalControlDialog", 2f);
	}

	void DiminishParentalControlDialog()
	{
		iTween.ScaleTo(parentalControlDialog, new Vector3(0, 0, 0), 0.5f);
		Invoke("HideParentalControlDialog", 0.5f);
	}
}
