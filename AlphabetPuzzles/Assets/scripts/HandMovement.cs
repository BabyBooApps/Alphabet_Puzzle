using System;
using System.Collections;
using UnityEngine;

public class HandMovement : MonoBehaviour
{

	#region Variables
	// array to hold waypoint locations
	[HideInInspector]
	public Vector3[] waypoints;
	[HideInInspector]
	public int startIndex = 1;
	// variable to control time taken to travel between points
	float duration = 0.3f;

	private Vector3 startPoint;
	private Vector3 endPoint;
	private float startTime;

	// the array index number of the current target waypoint
	private int targetwaypoint;

	private bool showPath = false;
	#endregion

	#region Unity Methods
	void Start()
	{
		GetComponent<SpriteRenderer>().enabled = false;
	}
	/*
	void Start()
	{
		startPoint = transform.position;
		startTime = Time.time;
		if (waypoints.Length <= 0)
		{
			Debug.Log("No waypoints found");
			enabled = false;
		}
		targetwaypoint = 0;
		endPoint = waypoints[targetwaypoint].position;

	}
	*/
	void Update()
	{
		if (showPath)
		{
			var i = (Time.time - startTime) / duration;
			transform.position = Vector3.Lerp(startPoint, endPoint, i);
			if (i >= 1)
			{
				startTime = Time.time;
				// increment and wrap the target waypoint index
				targetwaypoint++;
				targetwaypoint = targetwaypoint % waypoints.Length;
				if(targetwaypoint == 0)
				{
					targetwaypoint = startIndex;
					GetComponent<SpriteRenderer>().enabled = false;
				}else if(targetwaypoint == startIndex + 1)
				{
					GetComponent<SpriteRenderer>().enabled = true;
				}
				// assign the new lerp waypoints
				startPoint = endPoint;
				endPoint = waypoints[targetwaypoint];
			}
		}
	}

	public void InitiateShowingPath()
	{
		//startPoint = transform.position;
		if (waypoints.Length > 0)
		{
			startIndex = Camera.main.GetComponent<GameController>().currentDotIndex;
			//Debug.Log(startIndex + " : " + waypoints.Length);
			startPoint = waypoints[startIndex];
			transform.position = waypoints[startIndex];
			if (waypoints.Length - startIndex <= 0)
			{
				Debug.Log("No waypoints found");
				enabled = false;
			}
			targetwaypoint = startIndex;
			endPoint = waypoints[targetwaypoint];
			//Invoke("StartShowingPath", 3f);
			StartCoroutine(StartShowingRoutine());
		}
		//else
		//{
		//	Invoke("InitiateShowingPath", 2f);
		//}
	}

	IEnumerator StartShowingRoutine()
	{
		yield return new WaitForSeconds(3f);
		StartShowingPath();
	}

	void StartShowingPath()
	{
		startTime = Time.time;
		showPath = true;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	public void StopShowingPath()
	{
		StopAllCoroutines();
		GetComponent<SpriteRenderer>().enabled = false;
		showPath = false;
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
