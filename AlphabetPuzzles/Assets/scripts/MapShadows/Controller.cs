using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Data;
using System;

public class Controller : MonoBehaviour
{
    public GameObject[] imageObjects;
    public GameObject[] shadowObjects;
    public GameObject[] cardObjects;

    public GameObject trigger;
    public AudioSource audioSource;
    public List<AudioClip> instaClips;

    public static int objCount = 3;
    
    //private Sprite[] currentSprites;
    //private Sprite[] currentSpriteShadows;

    DataRow[] currentDataSet;

    int typeSel;
    List<int> positionsOrder;
    System.Random rand = new System.Random();

    AudioClip appreciateSound;
    //string spritesFolder;

    //Vector3[] initialPositions = new Vector3[3] { new Vector3(-8.25f, 3.8f, -0.5f), new Vector3(-8.25f, 0.6f, -0.5f), new Vector3(-8.25f, -2.65f, -0.5f) };
    Vector3[] initialPositions = new Vector3[3] { new Vector3(-4f, -3.6f, -0.5f), new Vector3(0f, -3.6f, -0.5f), new Vector3(4f, -3.6f, -0.5f) };

    Vector3[] targetPositions = new Vector3[5]{ new Vector3(-2.75f,3.5f,-0.5f),                             new Vector3(6.5f,2.25f,-0.5f),
                                                                                new Vector3(1.5f,0f,-0.5f),
                                                new Vector3(-2.75f,-2.5f,-0.5f),                                    new Vector3(6.5f,-2.5f,-0.5f)};

    Vector3[][] targetPositionSets = new Vector3[][]{
                                        new Vector3[]{new Vector3( 0F,3F,0F),new Vector3(-6.5F,3F,0F),new Vector3(7.5F,3F,0F)},
                                        new Vector3[]{new Vector3( -6.5F,3.6F,0F),new Vector3(0F,2.1F,0F),new Vector3(7.5F,-0.7F,0F)},
                                        new Vector3[]{new Vector3(0F,4F,0F),new Vector3(-8F,0F,0F),new Vector3( 7.5F,0F,0F)},
                                        new Vector3[]{new Vector3(0F, 4F, 0F),new Vector3(-8F, 0F, 0F),new Vector3( 7.5F,3.1F,0F)},
                                        new Vector3[]{new Vector3(-6.5F,3.6F,0F),new Vector3(0F,2F,0F),new Vector3( 7.5F,3.1F,0F)},
                                        new Vector3[]{new Vector3(-6.5F, 3.6F, 0F),new Vector3(0F, 2F, 0F),new Vector3(7.5F,0F,0F)}
                    };
    float leftX;
    int[] sprites = null;

    AudioClip shadowClip;
    AudioClip mainClip;

    void Awake()
    {
        objCount = 3;
        try
        {
            if (DisplayAds_IAP.instance != null)
                DisplayAds_IAP.DisplayInterstitial();
                //DisplayAds_IAP.instance.DisplayInterstitial();
        }
        catch (System.Exception)
        { }
    }

    void Start()
    {
        leftX = StaticArrays.leftX;

        targetPositions = targetPositionSets[UnityEngine.Random.Range(0, 6)];
        positionsOrder = GetRandPosOfArrayList(targetPositions.Length);

        int tempIndex = UnityEngine.Random.Range(0, instaClips.Count);
        shadowClip = instaClips[tempIndex];
        instaClips.RemoveAt(tempIndex);
        tempIndex = UnityEngine.Random.Range(0, instaClips.Count);
        mainClip = instaClips[tempIndex];

        StartCoroutine(InstantiateSpritesOverTime());        

        string appreciate = "appreciate_" + GenerateRand().ToString();
        appreciateSound = (AudioClip)Resources.Load("appreciate/" + appreciate);

        if (IAPController.removeAdsStatus != IAPController.IAPStatus.PURCHASED)
        {
            StartCoroutine(DisplayAds_IAP.instance. InitializeRoutine());
        }
    }

    int GenerateRand()
    {
        return UnityEngine.Random.Range(1, 23);
    }

    /*
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    */

    void FixedUpdate()
    {
        if (objCount == 0)
        {
            objCount = 3;
            Invoke("PlayAppreciateSound", 1F);
            Invoke("ActivateTrigger", 0.5F);
            for (int i = 0; i < 3; i++)
            {
                cardObjects[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    List<int> GetRandPosOfArrayList(int length)
    {
        List<int> a = new List<int>();
        for (int i = 0; i < length; i++)
        {
            a.Add(i);
        }
        int[] arr = a.ToArray();


        for (int i = 0; i < arr.Length; i++)
        {
            int randIndex = rand.Next(i, arr.Length);
            int tmp = arr[randIndex];
            arr[randIndex] = arr[i];
            arr[i] = tmp;
        }
        List<int> s = new List<int>();
        s.AddRange(arr);
        return s;
    }

    void ActivateTrigger()
    {
        trigger.SetActive(true);
    }

    public int[] GetRandomSpriteIndexes(int size)
    {
        int[] retSpriteIndexes = new int[size];

        if (StaticArrays.alphabetSpriteList == null || StaticArrays.alphabetSpriteList.Count < size)
        {
            StaticArrays.alphabetSpriteList = Enumerable.Range(0, 26).ToList();
        }

        if (StaticArrays.orderType.Equals(StaticArrays.SHUFFLE))
        {
            for (int i = 0; i < size; i++)
            {
                int index = rand.Next(0, StaticArrays.alphabetSpriteList.Count);
                retSpriteIndexes[i] = StaticArrays.alphabetSpriteList[index];
                StaticArrays.alphabetSpriteList.RemoveAt(index);
            }
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                retSpriteIndexes[i] = StaticArrays.alphabetSpriteList[0];
                StaticArrays.alphabetSpriteList.RemoveAt(0);
            }
        }
        return retSpriteIndexes;     
    }


    IEnumerator InstantiateSpritesOverTime()
    {
        int[] letterIndexes = new int[3];
        int alphaCount = 3;
        int imgColumnIndex = 1;
        int shadowColumnIndex = 2;

        if (StaticArrays.LOWERCASE.Equals(StaticArrays.typecase))
        {
            imgColumnIndex = 3;
            shadowColumnIndex = 4;
        }

        if (StaticArrays.alphabetSpriteList != null && StaticArrays.alphabetSpriteList.Count == 2)
        {
            letterIndexes = new int[2];
            letterIndexes = GetRandomSpriteIndexes(2);
            alphaCount = 2;
        }
        else
        {
            letterIndexes = GetRandomSpriteIndexes(3);
            alphaCount = 3;
        }

        currentDataSet = new DataRow[objCount];
        for (int i = 0; i < alphaCount; i++)
        {
            string whereClause = DataFeed.Attributes[0] + "='" + DataFeed.alphabets[letterIndexes[i]] + "'";
            currentDataSet[i] = DAL.getSingleDataRow(whereClause);
        }

        objCount = alphaCount;

        for (int i = 0; i < alphaCount; i++)
        {
            audioSource.PlayOneShot(shadowClip);
            yield return new WaitForSeconds(0.1f);
            int j = positionsOrder[0];
            positionsOrder.RemoveAt(0);
            GameObject go = shadowObjects[i];

            go.GetComponent<SpriteRenderer>().sprite = (Sprite)currentDataSet[i][DataFeed.Attributes[shadowColumnIndex]];
            go.name = currentDataSet[i][0].ToString() + "_shadow";
            go.GetComponent<AudioSource>().clip = (AudioClip)currentDataSet[i][DataFeed.Attributes[8]];

            go.transform.position = targetPositions[j];
            go.SetActive(true);
            //Instantiate(go, targetPositions[j], Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < alphaCount; i++)
        {
            audioSource.PlayOneShot(mainClip);
            yield return new WaitForSeconds(0.1f);
           // Vector3 pos = new Vector3(leftX, initialPositions[i].y, initialPositions[i].z);
            Vector3 pos = new Vector3(initialPositions[i].x, initialPositions[i].y, initialPositions[i].z);
            GameObject goMain = imageObjects[i];

            goMain.GetComponent<SpriteRenderer>().sprite = (Sprite)currentDataSet[i][DataFeed.Attributes[imgColumnIndex]];
            goMain.name = currentDataSet[i][0].ToString();
            goMain.GetComponent<AudioSource>().clip = (AudioClip)currentDataSet[i][DataFeed.Attributes[7]];
            goMain.GetComponent<Draggable>().target = shadowObjects[i].transform;

            Vector3 instancePos;
            int plusOrMinus = UnityEngine.Random.Range(1, 5);
            if (plusOrMinus < 3)
            {
                instancePos = shadowObjects[i].transform.position - new Vector3(0, 10f, 0);
            }
            else
            {
                instancePos = shadowObjects[i].transform.position + new Vector3(0, 10f, 0);
            }
            cardObjects[i].transform.position = instancePos;
            cardObjects[i].GetComponent<SpriteRenderer>().sortingOrder = 3;
            if (UnityEngine.Random.Range(0, 10) < 5)
            {
                cardObjects[i].GetComponent<SpriteRenderer>().sprite = (Sprite)currentDataSet[i][DataFeed.Attributes[5]];
                cardObjects[i].GetComponent<AudioSource>().clip = (AudioClip)currentDataSet[i][DataFeed.Attributes[9]];
            }
            else
            {
                cardObjects[i].GetComponent<SpriteRenderer>().sprite = (Sprite)currentDataSet[i][DataFeed.Attributes[6]];
                cardObjects[i].GetComponent<AudioSource>().clip = (AudioClip)currentDataSet[i][DataFeed.Attributes[10]];
            }
            goMain.GetComponent<Draggable>().cardObject = cardObjects[i];            
            goMain.transform.position = pos;
            goMain.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }   

    void PlayAppreciateSound()
    {
        audioSource.PlayOneShot(appreciateSound);
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

