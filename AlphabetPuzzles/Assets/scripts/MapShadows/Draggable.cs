using UnityEngine;
using System.Collections;
using System;

public class Draggable : MonoBehaviour
{
    public AudioClip[] wrongSnapClips;   
    public Transform target;
    public GameObject cardObject;
    public GameObject sparkles;
    //public GameObject alphacard;

    AudioSource mainAudio;
    string goName;
    bool drag = false;
    bool snapped = false;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    [HideInInspector]
    public float scaleTo = 1.8F;
    private SpriteRenderer sprite;
    private bool isTouchDevice = false;
    

    void Awake()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            isTouchDevice = true;
        else
            isTouchDevice = false;
    }

    void Start()
    {
        snapped = false;
        sprite = GetComponent<SpriteRenderer>();
        goName = gameObject.name;
        //target = GameObject.Find(goName.Replace("(Clone)", "") + "_shadow(Clone)").transform;
        mainAudio = gameObject.GetComponent<AudioSource>();
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (drag)
        {
            DragMe();
            if (TestCollision(target))
            {
                drag = false;
                SnapMe();
            }
        }
    }

    float clickTime = 0f;
    void OnMouseDown()
    {
        clickTime = Time.time;
        if (!snapped)
        {
            drag = true;
            mainAudio.Play();
            sprite.sortingOrder = 4;
            iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one * scaleTo, "time", 0.5f, "easetype", iTween.EaseType.spring));
        }
    }

    void OnMouseUp()
    {
        if (drag)
        {
            drag = false;
            if (TestCollision(target))
            {
                SnapMe();
            }
            else
            {
                if (Time.time - clickTime > 0.5f)
                    mainAudio.PlayOneShot(wrongSnapClips[UnityEngine.Random.Range(0, wrongSnapClips.Length)]);
                iTween.ScaleTo(gameObject, iTween.Hash("scale", initialScale, "time", 0.5f, "easetype", iTween.EaseType.spring));
                iTween.MoveTo(transform.gameObject, iTween.Hash("position", initialPosition, "easetype", iTween.EaseType.spring, "time", 0.5f));
                sprite.sortingOrder = 2;

            }
        }
    }

    IEnumerator MoveCard()
    {
        yield return new WaitForSeconds(0.1f);
        try
        {
            iTween.MoveTo(cardObject, target.position, 0.5f);
            /*
            int rotationIndex = UnityEngine.Random.Range(1, 7);
            if (rotationIndex < 3)
            {
                iTween.RotateFrom(go, new Vector3(180f, 0, 0), 0.75f);
            }
            else if (rotationIndex < 5)
            {
                iTween.RotateFrom(go, new Vector3(0f, 180f, 0), 0.75f);
            }
            else
            {
                iTween.RotateFrom(go, new Vector3(0f, 0f, 180f), 0.75f);
            }
            */
            cardObject.SetActive(true);
        }
        catch (System.Exception)
        {
            Debug.Log("error");
        }
    }


    void EnableDrag()
    {
        drag = true;
    }

    void DragMe()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move;
        if (isTouchDevice)
        {
            pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distance_to_screen));
        }
        else
        {
            pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        }
        transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z);
    }

    bool TestCollision(Transform target)
    {
        return Vector3.Distance(target.position, transform.position) < 1.5;
    }

    void SnapMe()
    {
        if (!snapped)
        {
            snapped = true;
            iTween.MoveTo(gameObject, target.position, 0.3f);
            transform.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            sprite.sortingOrder = 2;
            Controller.objCount -= 1;

            //GameObject instance = Instantiate(Resources.Load("sparkles", typeof(ParticleSystem)), transform.position, transform.rotation) as GameObject;
            GameObject.Instantiate(sparkles, target.position, Quaternion.identity);

            target.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Fadeout>().enabled = true;    

            StartCoroutine(MoveCard());
        }
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
