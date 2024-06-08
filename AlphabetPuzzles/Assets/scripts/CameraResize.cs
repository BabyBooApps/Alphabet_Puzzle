using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour
{   
    float aspect;// = 1.55f;
    public GameObject background;
   
    Camera cam;

    void Awake()
    {
        cam = Camera.main;

        if (StaticArrays.aspect == 0f)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            StaticArrays.aspect = screenAspect;
            //	Debug.Log(StaticArrays.aspect);
        }
        aspect = StaticArrays.aspect;
        //Debug.Log("Aspect Ratio : " + aspect);
        if (aspect <= 1.6f)
        {
            //Debug.Log((2.298f - (0.749f * aspect)));
            cam.orthographicSize = cam.orthographicSize * (2.298f - (0.749f * aspect));
        }
        Resize();
    }


    void Resize()
    {
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        float worldScreenHeight = 2f * cam.orthographicSize;
        //float worldScreenWidth = worldScreenHeight * cam.aspect;
        float worldScreenWidth = 2f * cam.orthographicSize * aspect;

        float heightScale = (float)worldScreenHeight / (float)sr.sprite.bounds.size.y;
        float widthScale = (float)worldScreenWidth / (float)sr.sprite.bounds.size.x;       

        background.transform.localScale = new Vector3(widthScale, heightScale, 1);
        if (StaticArrays.leftX == 0f)
        {
            //Debug.Log(-(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x));           
            StaticArrays.leftX = -(Camera.main.orthographicSize * Camera.main.aspect) + 2.0f;
        }
    }
}
