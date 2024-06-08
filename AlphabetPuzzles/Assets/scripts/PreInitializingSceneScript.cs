using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreInitializingSceneScript : MonoBehaviour {

    void Start()
    {
        StartCoroutine(LoadInitializingScene());
    }

    IEnumerator LoadInitializingScene()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("InitializingScene");
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
