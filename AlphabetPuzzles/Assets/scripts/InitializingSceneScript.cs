using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializingSceneScript : MonoBehaviour
{
    #region Variables    
    [SerializeField]
    Sprite[] UPPERCASE;
    [SerializeField]
    Sprite[] UPPERCASE_SHADOW;
    [Space]
    [SerializeField]
    Sprite[] LOWERCASE;
    [SerializeField]
    Sprite[] LOWERCASE_SHADOW;
    [Space]
    [SerializeField]
    Sprite[] CARD_1;
    [SerializeField]
    Sprite[] CARD_2;
    [Space]
    [SerializeField]
    AudioClip[] ALPHABET_CLIP_FEMALE;
    [SerializeField]
    AudioClip[] ALPHABET_CLIP_MALE;
    [Space]
    [SerializeField]
    AudioClip[] CARD_1_AUDIO;
    [SerializeField]
    AudioClip[] CARD_2_AUDIO;
    [Space]
    [SerializeField]
    Sprite[] tracingWhiteLetterSprites_Uppercase;
    [SerializeField]
    Sprite[] tracingColorLetterSprites_Uppercase;
    [Space]
    public GameObject GameSceneCanvas;
    #endregion

    void Start()
    {
        //DontDestroyOnLoad(GameSceneCanvas);

        DataFeed.DataGen(UPPERCASE, UPPERCASE_SHADOW,
                         LOWERCASE, LOWERCASE_SHADOW,
                         CARD_1, CARD_2,
                         ALPHABET_CLIP_FEMALE, ALPHABET_CLIP_MALE,
                         CARD_1_AUDIO, CARD_2_AUDIO,
                         tracingWhiteLetterSprites_Uppercase, tracingColorLetterSprites_Uppercase);

        //System.GC.Collect();

        //DataFeed.CheckForNullValues(0);
        //DataFeed.CheckForNullValues(1);
        //DataFeed.CheckForNullValues(2);
        //DataFeed.CheckForNullValues(3);
        //DataFeed.CheckForNullValues(4);
        //DataFeed.CheckForNullValues(5);
        //DataFeed.CheckForNullValues(6);
        //DataFeed.CheckForNullValues(7);
        //DataFeed.CheckForNullValues(8);
        //DataFeed.CheckForNullValues(9);

        //SceneManager.LoadScene("MapShadows");
        //SceneManager.LoadScene("AlphabetTracingScene_Uppercase");    
     
        SceneManager.LoadScene("MenuScene");
    }   
}
