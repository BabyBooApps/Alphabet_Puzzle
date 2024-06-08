using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Random : MonoBehaviour
{
    string theObject;
    GameObject prefab;
    GameObject theDestroyable;

    public int ObjectCount = 40;
    public AudioSource blastAudioSource;
    public AudioClip[] balloonPop;
    public AudioClip[] fruitPop;
    public AudioClip rocketPop;

    public GameObject fruit;
    public GameObject rocket;
    public GameObject balloon;


    public Sprite[] fruitSprites;
    public Sprite[] balloonSprites;
    public Sprite[] rocketSprites;

    float lowerY = -7F;
    int sceneNo;
    void Awake()
    {
        lowerY = -(Camera.main.orthographicSize - 2.5f);
        Debug.Log(lowerY);
    }

   
    void Start()
    {
        //if (StaticArrays.random == null)
        //{
        //    StaticArrays.random = new System.Random();
        //}
        //sceneNo = StaticArrays.random.Next(1, 7);
        sceneNo = UnityEngine.Random.Range(1, 7);
        StartCoroutine(InstantiateOverTime(sceneNo));
        Invoke("ReloadLevel", 9F);

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (sceneNo == 1 || sceneNo == 4)
                handleBalloonClicks();
            else
                handleClicks();
        }
    }

    /*
    int GenerateRand()
    {
        return StaticArrays.random.Next(1, 23);
    }
    */

    void handleBalloonClicks()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            blastAudioSource.PlayOneShot(balloonPop[UnityEngine.Random.Range(0, balloonPop.Length)]);
            theDestroyable = hit.transform.gameObject;
            StartCoroutine(DestroyObject(theDestroyable, 0.1f));
        }
    }


    void DestroyNow(GameObject theDestroyable)
    {
        Destroy(theDestroyable);
    }


    void handleClicks()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            theDestroyable = hit.transform.gameObject;
            if (theObject == "Rocket")
            {
                blastAudioSource.PlayOneShot(rocketPop);
                hit.transform.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                hit.transform.gameObject.GetComponent<Animator>().enabled = true;
                hit.transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = -4f;
            }
            else if (theObject == "Fall")
            {
                blastAudioSource.PlayOneShot(fruitPop[UnityEngine.Random.Range(0,fruitPop.Length)]);
                hit.transform.gameObject.GetComponent<Fall>().SplashMe();
            }
            StartCoroutine(DestroyObject(theDestroyable, 5f));
        }
    }


    IEnumerator DestroyObject(GameObject theDestroyable, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(theDestroyable);
    }


    IEnumerator InstantiateOverTime(int sceneNo)
    {
        int spawned = 0;
        GameObject SpawnedObj;
        switch (sceneNo)
        {
            case 1:
            case 4:
                prefab = balloon;
                while (spawned < 60)
                {
                    Vector3 position;
                    position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), lowerY, UnityEngine.Random.Range(-9.5F, -0.5F));
                    int balloonNo = UnityEngine.Random.Range(0, 9);
                    prefab.GetComponent<SpriteRenderer>().sprite = balloonSprites[balloonNo];

                    //(Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.SetParent(gameObject.transform);
                    spawned++;

                    SpawnedObj = Instantiate(prefab, position, Quaternion.identity, gameObject.transform);
                    SpawnedObj.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                    SpawnedObj.SetActive(true);
                    iTween.ScaleFrom(SpawnedObj, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeOutCubic));
                }
                break;

            case 2:
            case 5:
                prefab = rocket;
                while (spawned < 60)
                {
                    Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), lowerY, UnityEngine.Random.Range(-9.5F, -0.5F));
                    int rocketNo = UnityEngine.Random.Range(0, 4);
                    prefab.GetComponent<SpriteRenderer>().sprite = rocketSprites[rocketNo];
                    //(Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.SetParent(gameObject.transform);
                    theObject = prefab.name;

                    spawned++;
                    SpawnedObj = Instantiate(prefab, position, Quaternion.identity, gameObject.transform);
                    SpawnedObj.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                    SpawnedObj.SetActive(true);
                    iTween.ScaleFrom(SpawnedObj, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeOutCubic));
                }
                break;

            case 3:
            case 6:
                prefab = fruit;
                while (spawned < 60)
                {
                    int fruitNo = UnityEngine.Random.Range(0, 4);
                    prefab.GetComponent<SpriteRenderer>().sprite = fruitSprites[fruitNo];
                    //DestroyImmediate(prefab.GetComponent<PolygonCollider2D>(), true);
                    //prefab.AddComponent<PolygonCollider2D>();
                    Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), -(lowerY - 2f), UnityEngine.Random.Range(-9.5F, -0.5F));
                    //(Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.SetParent(gameObject.transform);
                    theObject = prefab.name;


                    spawned++;
                    SpawnedObj = Instantiate(prefab, position, Quaternion.identity, gameObject.transform);
                    SpawnedObj.SetActive(false);
                    yield return new WaitForSeconds(0.3f);
                    SpawnedObj.SetActive(true);
                    iTween.ScaleFrom(SpawnedObj, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeOutCubic));
                }
                break;


            default:
                prefab = rocket;
                while (spawned < 60)
                {
                    Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), lowerY, UnityEngine.Random.Range(-9.5F, -0.5F));
                    int rocketNo = UnityEngine.Random.Range(0, 4);
                    prefab.GetComponent<SpriteRenderer>().sprite = rocketSprites[rocketNo];
                    //(Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.SetParent(gameObject.transform);
                    theObject = prefab.name;

                    spawned++;
                    SpawnedObj = Instantiate(prefab, position, Quaternion.identity, gameObject.transform);
                    SpawnedObj.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                    SpawnedObj.SetActive(true);
                    iTween.ScaleFrom(SpawnedObj, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeOutCubic));
                }
                break;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    /*
    string theObject;
    GameObject prefab;
    //GameObject theBalloon;
    GameObject theDestroyable;
    public int ObjectCount = 40;
    public AudioSource blastAudioSource;
    public AudioClip balloonPop;
    public AudioClip fruitPop;
    public AudioClip rocketPop;

    float lowerY = -7F;
    //float upperY = -50F;
    int sceneNo;
   
    
    void Awake()
    {
        lowerY = -(Camera.main.orthographicSize) - 2f; 
    }
    
    // Use this for initialization
    void Start()
    {
        //blastAudioSource = GameObject.Find ("EduBuzzLogo").GetComponent<AudioSource> ();

        //sceneNo = UnityEngine.Random.Range (1,4);
        if (StaticArrays.random == null)
        {
            StaticArrays.random = new System.Random();
        }
        sceneNo = StaticArrays.random.Next(1, 7);
        //sceneNo = StaticArrays.random.
        //Debug.Log (sceneNo);

        StartCoroutine(InstantiateOverTime( sceneNo));
        Invoke("ReloadLevel", 8F);

    }


    void Update()
    {       
       
        if (Input.GetMouseButtonDown(0) )
        {
            if (sceneNo == 1 || sceneNo == 4)
                handleBalloonClicks();
            else
                handleClicks();
        }
    }
    int GenerateRand()
    {
        return StaticArrays.random.Next(1, 23);
    }
    void handleBalloonClicks()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.name.Contains("balloon"))
            {
                blastAudioSource.PlayOneShot(balloonPop);
                theDestroyable = hit.transform.gameObject;
                StartCoroutine(DestroyObject(theDestroyable, 0.1f));
            }
        }
    }

    void DestroyNow(GameObject theDestroyable)
    {
        Destroy(theDestroyable);
    }

    void handleClicks()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            theDestroyable = hit.transform.gameObject;
            if (theObject == "Rocket")
            {
                if (hit.transform.gameObject.name.Contains("Rocket"))
                {
                    blastAudioSource.PlayOneShot(rocketPop);
                    hit.transform.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    hit.transform.gameObject.GetComponent<Animator>().enabled = true;
                    hit.transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = -4f;
                    StartCoroutine(DestroyObject(theDestroyable, 5f));
                }
            }
            else if (theObject == "Fall")
            {
                if (hit.transform.gameObject.name.Contains("Fall"))
                {
                    blastAudioSource.PlayOneShot(fruitPop);
                    hit.transform.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    hit.transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;
                    StartCoroutine(DestroyObject(theDestroyable, 5f));
                }
            }

        }
    }

    IEnumerator DestroyObject(GameObject theDestroyable, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(theDestroyable);
    }

    IEnumerator InstantiateOverTime( int sceneNo)
    {
       
        int spawned = 0;
        switch (sceneNo)
        {
            case 1:
            case 4:
                prefab = Resources.Load<GameObject>("balloon");
                while (spawned < 60)
                {
                   
                        Vector3 position;
                            position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F),lowerY, UnityEngine.Random.Range(-9.5F, -0.5F));
                        int balloonNo = UnityEngine.Random.Range(1, 10);
                        prefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("balloon/" + balloonNo);
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
                        spawned++;
                    yield return new WaitForSeconds(0.2f);

                }
                break;

            case 2:
            case 5:
                prefab = Resources.Load<GameObject>("Rocket");
                while (spawned < 60)
                {

                        Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), lowerY, UnityEngine.Random.Range(-9.5F, -0.5F));
                        int rocketNo = UnityEngine.Random.Range(1, 5);
                        prefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("rocket/" + rocketNo);
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.SetParent(gameObject.transform);
                        theObject = prefab.name;

                        spawned++;
                    yield return new WaitForSeconds(0.2f);
                }
                break;

            case 3:
            case 6:
                prefab = Resources.Load<GameObject>("Fall");
                while (spawned < 60)
                {
                        prefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("fruit" + UnityEngine.Random.Range(1, 5));
                        DestroyImmediate(prefab.GetComponent<PolygonCollider2D>(), true);
                        prefab.AddComponent<PolygonCollider2D>();

                        Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F),-lowerY, UnityEngine.Random.Range(-9.5F, -0.5F));
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.SetParent(gameObject.transform);
                        theObject = prefab.name;

                        spawned++;
                    yield return new WaitForSeconds(0.25f);
                }
                break;


            default:
                prefab = Resources.Load<GameObject>("Rocket");
                while (spawned < 60)
                {
                        Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), lowerY, UnityEngine.Random.Range(-9.5F, -0.5F));
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
                        theObject = prefab.name;
                        spawned++;
                    yield return new WaitForSeconds(0.2f);
                }
                break;

        }

    }



    void ReloadLevel()
    {
        SceneManager.LoadScene("MapShadows");
    }


}
*/
