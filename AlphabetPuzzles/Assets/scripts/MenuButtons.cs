using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject menuButtons;
    public GameObject quitCanvas;
    public AudioSource audioSource;
    public AudioClip audioclip;

    public Button btnTypeCase;
    public Button btnOrderType;
    public Sprite spriteUpperCase;
    public Sprite spriteLowerCase;
    public Sprite spriteSequence;
    public Sprite spriteShuffle;

    string sceneName = "";

    void Awake()
    {        
        sceneName = SceneManager.GetActiveScene().name;
        //Debug.Log(sceneName);
        if (!sceneName.Equals("MenuScene"))
        {
            //Debug.Log("Inside IF!!!");
            menuButtons.SetActive(false);
            float timeToDisplay = UnityEngine.Random.Range(1.5f, 2f);
            Invoke("EnableMenuButtons", timeToDisplay);
        }
        else
        {
            menuButtons.SetActive(true);
            quitCanvas.SetActive(false);
        }        
    }

    void Start()
    {

        StaticArrays.typecase = PlayerPrefs.GetString(StaticArrays.TYPECASE, StaticArrays.UPPERCASE);
        StaticArrays.orderType = PlayerPrefs.GetString(StaticArrays.ORDERTYPE, StaticArrays.SHUFFLE);

        if (sceneName.Equals("MapShadows"))
        {
            if (StaticArrays.orderType.Equals(StaticArrays.SHUFFLE))
            {
                btnOrderType.image.overrideSprite = spriteSequence;
            }
            else
            {
                btnOrderType.image.overrideSprite = spriteShuffle;
            }
            SetTypeButtonSprite();
        }

        
    }

    void EnableMenuButtons()
    {
        menuButtons.SetActive(true);
    }

    public void LoadMapShadowsScene()
    {
        PlaySound();
        SceneManager.LoadScene("MapShadows");
    }

    public void LoadTracingScene()
    {
        PlaySound();
        SceneManager.LoadScene("AlphabetTracingScene_Uppercase");
    }

    public void LoadMoreGames()
    {
        PlaySound();
        Application.OpenURL("https://play.google.com/store/apps/dev?id=6487105028651572662");
    }

    public void OpenPlaystore(string url)
    {
        PlaySound();
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + url);
    }

    void PlaySound()
    {
        audioSource.Play();
    }

    public void ToggleOrderType()
    {
        PlaySound();
        if (StaticArrays.orderType.Equals(StaticArrays.SHUFFLE))
        {
            btnOrderType.image.overrideSprite = spriteShuffle;
            StaticArrays.orderType = StaticArrays.SEQUENCE;
            PlayerPrefs.SetString(StaticArrays.ORDERTYPE, StaticArrays.SEQUENCE);
        }
        else
        {
            btnOrderType.image.overrideSprite = spriteSequence;
            StaticArrays.orderType = StaticArrays.SHUFFLE;
            PlayerPrefs.SetString(StaticArrays.ORDERTYPE, StaticArrays.SHUFFLE);
        }
        //StaticArrays.lowercaseSpriteList = null;
        //StaticArrays.uppercaseSpriteList = null;
        StaticArrays.alphabetSpriteList.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    public void LoadScene(string sceneName)
    {
        PlaySound();
        menuButtons.SetActive(false);
        float timeToDisplay = UnityEngine.Random.Range(1.5f, 3f);
        Invoke("EnableMenuButtons", timeToDisplay);
        SceneManager.LoadScene(sceneName);
    }

    public void ShowQuitDialog()
    {
        PlaySound();
        quitCanvas.SetActive(true);
    }

    public void HideQuitDialog()
    {
        PlaySound();
        quitCanvas.SetActive(false);
    }

    public void Quit()
    {
        PlaySound();
        Application.Quit();
    }

    public void ToggleTypeCase()
    {
        PlaySound();
        if (StaticArrays.UPPERCASE.Equals(StaticArrays.typecase))
        {
            StaticArrays.typecase = StaticArrays.LOWERCASE;
            PlayerPrefs.SetString(StaticArrays.TYPECASE, StaticArrays.LOWERCASE);
        }
        else if (StaticArrays.LOWERCASE.Equals(StaticArrays.typecase))
        {
            StaticArrays.typecase = StaticArrays.UPPERCASE;
            PlayerPrefs.SetString(StaticArrays.TYPECASE, StaticArrays.UPPERCASE);
        }
        SetTypeButtonSprite();
        //StaticArrays.alphabetSpriteList.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetTypeButtonSprite()
    {
        menuButtons.SetActive(false);
        float timeToDisplay = UnityEngine.Random.Range(1.5f, 3f);
        Invoke("EnableMenuButtons", timeToDisplay);
        if (sceneName.Equals("MapShadows"))
        {
            if (StaticArrays.UPPERCASE.Equals(StaticArrays.typecase))
            {
                btnTypeCase.image.overrideSprite = spriteLowerCase;
            }
            else if (StaticArrays.LOWERCASE.Equals(StaticArrays.typecase))
            {
                btnTypeCase.image.overrideSprite = spriteUpperCase;
            }
        }
    }

}
