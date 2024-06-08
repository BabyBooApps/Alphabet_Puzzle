using UnityEngine;

public class LetterScript : MonoBehaviour
{

	#region Variables
	public Color enterColor = Color.red;
	public Color exitColor = Color.black;
	#endregion

	#region Unity Methods

	//private static Transform selected = null;
	private bool isSelected = false;
	private bool outside = true;
	//void Update()
	//{
	//	if (isSelected && selected != transform)
	//	{
	//		selected = null;
	//		isSelected = false;
	//		gameObject.GetComponent<SpriteRenderer>().material.color = exitColor;
	//	}
	//}
	public void SelectMe()
	{
		if (!isSelected )
		{
			//selected = transform;
			isSelected = true;

			if (outside)
			{				
				gameObject.GetComponent<SpriteRenderer>().material.color = exitColor;
			}
			else
			{
				gameObject.GetComponent<SpriteRenderer>().material.color = enterColor;
			}
			//outside = false;
		}
	}

	public void UnSelectMe()
	{
		if (isSelected)
		{
			//selected = null;
			isSelected = false;
			//outside = true;
			gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
		}
	}

	public void OutsideMe()
	{
		if (!outside)
		{
			//selected = null;
			//isSelected = false;
			outside = true;			
			gameObject.GetComponent<SpriteRenderer>().material.color = exitColor;
		}
	}

	void OnMouseDown()
	{
		outside = false;
	}

	void OnMouseUp()
	{
		outside = true;
	}

	#endregion
}
