using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    //16:10     - 5.5
    //16:9      - 5.5
    //18:9      - 5
    //19:9      - 5
    //19.5 : 9  - 5
    //20 :9     - 4.5
    //21:18     - 8
    //23:9      - 5


    public float defaultCameraSize = 7f;

    void Start()
    {
        StartCoroutine(AdjustCameraSize());
    }

    IEnumerator AdjustCameraSize()
    {
        yield return new WaitForEndOfFrame();

        float aspectRatio = (float)Screen.width / Screen.height;

        float newSize = defaultCameraSize;

        if (Mathf.Approximately(aspectRatio, 16f / 10f) || Mathf.Approximately(aspectRatio, 16f / 9f))
        {
            newSize = 7f;
        }
        else if (Mathf.Approximately(aspectRatio, 18f / 9f) || Mathf.Approximately(aspectRatio, 19f / 9f) || Mathf.Approximately(aspectRatio, 19.5f / 9f))
        {
            newSize = 6.5f;
        }
        else if (Mathf.Approximately(aspectRatio, 20f / 9f))
        {
            newSize = 6f;
        }
        else if (Mathf.Approximately(aspectRatio, 21f / 18f))
        {
            newSize = 9.5f;
        }
        else if (Mathf.Approximately(aspectRatio, 23f / 9f))
        {
            newSize = 6.5f;
        }
        else
        {
            // Find the nearest aspect ratio and set the camera size accordingly
            float[] validAspectRatios = { 16f / 10f, 16f / 9f, 18f / 9f, 19f / 9f, 19.5f / 9f, 20f / 9f, 21f / 18f, 23f / 9f };
            float closestAspectRatio = FindClosestAspectRatio(aspectRatio, validAspectRatios);
            newSize = GetCameraSizeForAspectRatio(closestAspectRatio);
        }

        Camera.main.orthographicSize = newSize;
    }

    float FindClosestAspectRatio(float targetAspectRatio, float[] validAspectRatios)
    {
        float closestRatio = validAspectRatios[0];
        float minDifference = Mathf.Abs(targetAspectRatio - closestRatio);

        foreach (float ratio in validAspectRatios)
        {
            float difference = Mathf.Abs(targetAspectRatio - ratio);
            if (difference < minDifference)
            {
                closestRatio = ratio;
                minDifference = difference;
            }
        }

        return closestRatio;
    }

    float GetCameraSizeForAspectRatio(float aspectRatio)
    {
        switch (aspectRatio)
        {
            case 16f / 10f:
            case 16f / 9f:
                return 7f;
            case 18f / 9f:
            case 19f / 9f:
            case 19.5f / 9f:
                return 6.5f;
            case 20f / 9f:
                return 6f;
            case 21f / 18f:
                return 9.5f;
            case 23f / 9f:
                return 6.5f;
            default:
                return defaultCameraSize;
        }
    }
}
