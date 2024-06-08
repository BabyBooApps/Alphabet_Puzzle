using UnityEngine;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    #region Variables
    public GameObject letterObject;
    public SpriteRenderer letterRenderer;
    public GameObject handObject;
    public GameObject[] sparkleObjects;
    [Space]
    public GameObject linePrefab;
    public GameObject dotPrefab;
    //public GameObject dotPrefab_Orange;
    //public GameObject dotPrefab_Red;
    public GameObject flarePrefab;
    [Space]
    public Sprite homeDot;
    public Sprite nextDot;
    public Sprite nextDotBlink;
    public Sprite doneDot;
    public Sprite otherDot;
    [Space]
    public AudioClip[] maleVoiceAlphabets;
    public AudioClip[] femaleVoiceAlphabets;
    [Space]
    public AudioClip[] appreciations;
    public AudioClip dotClip;

    DataTable currentLetterTable = new DataTable();
    DataRow currentRow;

    [HideInInspector]
    public int currentLetterIndex = 0;

    int strokeCount = 0;
    int currentStrokeIndex = 0;
    [HideInInspector]
    public int currentDotIndex = 0;
    Vector3[] currentDotPositions = new Vector3[] { };

    GameObject currentLine;
    LineRenderer currentLineRenderer;

    List<GameObject> dotsList = new List<GameObject>();
    List<GameObject> linesList = new List<GameObject>();
    List<GameObject> strokesList = new List<GameObject>();

    bool isTouchDevice = false;
    bool isMousePressed = false;
    bool startTracing = false;
    bool blink = false;

    Vector3 pos1;
    Vector3 pos2;

    AudioSource audioSource;
    ParticleSystem[] particleSystems;
    bool showering = false;
    GameObject sparkles;
    #endregion

    #region Unity Methods

    void Awake()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            isTouchDevice = true;
        else
            isTouchDevice = false;

        isMousePressed = false;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentLetterIndex = 0;
        PopulateLetter();
    }

    void Update()
    {
        if (isMousePressed)
        {
            Vector3 tempPos = GetTouchPosition();
            pos2 = GetComponent<Camera>().ScreenToWorldPoint(tempPos);
            pos2.z = 10;
            currentLineRenderer.positionCount = 2;
            currentLineRenderer.SetPosition(0, currentDotPositions[currentDotIndex]);
            currentLineRenderer.SetPosition(1, pos2);
        }

        if (currentDotIndex < (currentDotPositions.Length - 1))
        {
            if (startTracing && TouchBegan())
            {
                Vector3 flarePos = GetTouchPosition();
                flarePos = GetComponent<Camera>().ScreenToWorldPoint(flarePos);
                flarePos.z = 9;
                GameObject flare = Instantiate(flarePrefab, flarePos, Quaternion.identity);
                handObject.GetComponent<HandMovement>().StopShowingPath();
                if (TouchedTheSourcePoint(Camera.main.ScreenToWorldPoint(GetTouchPosition())))
                {
                    StopAllCoroutines();
                    audioSource.PlayOneShot(dotClip);
                    //audioSource.PlayOneShot(dotConnectedSound);
                    blink = false;
                    Vector3 tempPos = Input.mousePosition;
                    tempPos.z = 10;
                    pos1 = GetComponent<Camera>().ScreenToWorldPoint(tempPos);
                    pos1.x = currentDotPositions[currentDotIndex].x;
                    pos1.y = currentDotPositions[currentDotIndex].y;
                    isMousePressed = true;
                    currentLine = GameObject.Instantiate(linePrefab) as GameObject;
                    currentLineRenderer = currentLine.GetComponent<LineRenderer>();
                    //currentLineRenderer.numPositions = 1;
                    currentLine.name = "Line_L" + currentStrokeIndex + "_D" + currentDotIndex;

                    dotsList[currentDotIndex].GetComponent<SpriteRenderer>().sprite = doneDot;
                    if (currentDotIndex < currentDotPositions.Length - 1)
                    {
                        dotsList[currentDotIndex + 1].GetComponent<SpriteRenderer>().sprite = nextDot;
                        StartCoroutine(DotBlinkRoutine(dotsList[currentDotIndex + 1]));
                    }
                    linesList.Add(currentLine);
                }
            }

            if (isMousePressed && startTracing)
            {
                if (TouchedTheDestinationPpoint(Camera.main.ScreenToWorldPoint(GetTouchPosition())))
                {
                    StopAllCoroutines();
                    audioSource.PlayOneShot(dotClip);

                    Vector3 flarePos = currentDotPositions[currentDotIndex];
                    flarePos.z = 9;
                    GameObject flare = Instantiate(flarePrefab, flarePos, Quaternion.identity);

                    currentDotIndex += 1;

                    blink = false;
                    if (currentDotIndex == currentDotPositions.Length - 1)
                    {
                        startTracing = false;
                        isMousePressed = false;
                        flarePos = currentDotPositions[currentDotIndex];
                        flarePos.z = 9;
                        GameObject flareLast = Instantiate(flarePrefab, flarePos, Quaternion.identity);

                        pos2 = currentDotPositions[currentDotIndex];
                        pos2.z = 10f;
                        RedrawLine();


                        dotsList[0].GetComponent<SpriteRenderer>().sprite = doneDot;

                        currentStrokeIndex += 1;

                        #region Testing to add single line for entire stroke
                        if (linesList.Count > 0)
                        {
                            List<Vector3> points = new List<Vector3>();
                            //int index = 0;
                            //int linesCount = linesList.Count;
                            //for (int i = 0; i < linesList.Count; i++)
                            //{
                            //	GameObject lineObject = linesList[i];
                            //	LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
                            //	points.Add(lineRenderer.GetPosition(0));
                            //	index++;
                            //	if (i == linesCount - 1)
                            //	{
                            //		points.Add(lineRenderer.GetPosition(1));
                            //		index++;
                            //	}
                            //}
                            points.AddRange(currentDotPositions);
                            ClearLines();
                            currentLine = GameObject.Instantiate(linePrefab) as GameObject;
                            currentLineRenderer = currentLine.GetComponent<LineRenderer>();
                            currentLineRenderer.positionCount = points.Count;
                            currentLineRenderer.SetPositions(points.ToArray());

                            strokesList.Add(currentLine);

                        }
                        #endregion

                        if (strokeCount == currentStrokeIndex)
                        {
                            //audioSource.PlayOneShot(femaleVoiceAlphabets[currentLetterIndex]);
                            audioSource.PlayOneShot((AudioClip)currentRow[7]);
                            CLearDots();
                            //letterRenderer.sprite = (Sprite)currentLetterTable.Rows[0][5];
                            //Invoke("PopulateLetter", 1f);
                            StartCoroutine(DisplayColorAlphabetRoutine());
                        }
                        else
                        {
                            PopulateStrokePoints();
                        }
                    }
                    else
                    {
                        pos2 = currentDotPositions[currentDotIndex];
                        pos2.z = 10f;
                        RedrawLine();
                        pos1 = pos2;

                        dotsList[currentDotIndex].GetComponent<SpriteRenderer>().sprite = doneDot;
                        if (currentDotIndex == currentDotPositions.Length - 1)
                        {
                            dotsList[0].GetComponent<SpriteRenderer>().sprite = nextDot;
                            StartCoroutine(DotBlinkRoutine(dotsList[0]));
                        }
                        else
                        {
                            if (!(currentDotIndex == 0))
                                dotsList[currentDotIndex + 1].GetComponent<SpriteRenderer>().sprite = nextDot;
                            //StartCoroutine(DotBlinkRoutine(dotsList[currentDotIndex + 1]));
                            StartCoroutine(DotBlinkRoutine(dotsList[currentDotIndex]));
                        }
                        CreateNewLine();
                    }
                }
                else
                {
                    if (TouchEnded())
                    {
                        handObject.GetComponent<HandMovement>().InitiateShowingPath();
                        OnMouseUpOrLineOutside();
                    }
                }
            }
        }

        if (TouchEnded())
        {
            isMousePressed = false;
            if (strokeCount != currentStrokeIndex)
                handObject.GetComponent<HandMovement>().InitiateShowingPath();

        }

        #region To test if the Line is out of the Letter Area

        if (isMousePressed)
        {
            //Ray ray = Camera.main.ScreenPointToRay(GetTouchPosition());
            //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            //if (hit.collider != null)
            //{
            //	if (hit.collider.gameObject.name == "bg")
            //	{
            //		//letterObject.GetComponent<LetterScript>().OutsideMe();
            //		Debug.Log("bg");
            //		OnMouseUpOrLineOutside();
            //	}
            //}

            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(GetTouchPosition()), Vector2.zero);
            if (hitInfo.transform.gameObject.name == "bg")
            {
                //Debug.Log("bg");
                OnMouseUpOrLineOutside();
            }
        }

        #endregion

        /*
		if (Input.GetMouseButtonDown(0))
		{
			currentStrokeIndex += 1;
			if (strokeCount == currentStrokeIndex)
			{
				currentLetterIndex += 1;
				PopulateLetter();
			}
			else
			{
				PopulateStrokePoints();
			}
		}		
		*/
        //foreach (Touch touch in Input.touches)
        //{
        //	if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        //	{
        //		Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //		RaycastHit hit;
        //		if (Physics.Raycast(ray, out hit, 1000.0f) /*&& hit.tag == "Selectable"*/)
        //		{
        //			hit.collider.GetComponent<LetterScript>().SelectMe();
        //		}
        //	}
        //}

        #region To test if the Line is out of the Letter Not Used Now
        /*****
		if (Input.GetMouseButtonDown(0))
		{
			mouseDown = true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (mouseDown)
			{
				mouseDown = false;				
				letterObject.GetComponent<LetterScript>().UnSelectMe();
			}
		}
		if (mouseDown)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
			if (hit.collider != null  )
			{
				if (hit.collider.gameObject.name == "Letter")
				{
					letterObject.GetComponent<LetterScript>().SelectMe();
				}else 
				if (hit.collider.gameObject.name == "bg")
				{
					letterObject.GetComponent<LetterScript>().OutsideMe();
					Debug.Log("bg");
				}

			}
			else
			{
				letterObject.GetComponent<LetterScript>().OutsideMe();
			}
		}
		*/
        #endregion
    }

    void OnMouseUpOrLineOutside()
    {
        isMousePressed = false;
        StopAllCoroutines();

        int count = linesList.Count;
        if (count > 0)
        {
            Destroy(linesList[count - 1]);
            linesList.RemoveAt(count - 1);
        }
        blink = false;
        if (currentDotIndex == 0)
        {
            dotsList[0].GetComponent<SpriteRenderer>().sprite = homeDot;
            dotsList[1].GetComponent<SpriteRenderer>().sprite = otherDot;
        }
        //else if (currentDotIndex == currentDotPositions.Length - 1)
        //{
        //	dotsList[0].GetComponent<SpriteRenderer>().sprite = homeDot;
        //	dotsList[currentDotIndex].GetComponent<SpriteRenderer>().sprite = nextDot;

        //	StartCoroutine(DotBlinkRoutine(dotsList[currentDotIndex]));
        //}
        else
        {
            dotsList[currentDotIndex].GetComponent<SpriteRenderer>().sprite = nextDot;
            dotsList[currentDotIndex + 1].GetComponent<SpriteRenderer>().sprite = otherDot;
            StartCoroutine(DotBlinkRoutine(dotsList[currentDotIndex]));
        }
    }

    public void ClearStrokes()
    {
        if (strokesList.Count > 0)
        {
            for (int i = 0; i < strokesList.Count; i++)
            {
                Destroy(strokesList[i]);
            }
            strokesList.Clear();
        }
    }

    public void ClearLines()
    {
        if (linesList.Count > 0)
        {
            for (int i = 0; i < linesList.Count; i++)
            {
                Destroy(linesList[i]);
            }
            linesList.Clear();
        }
    }

    void CLearDots()
    {
        if (dotsList.Count > 0)
        {
            for (int i = 0; i < dotsList.Count; i++)
            {
                StopAllCoroutines();
                Destroy(dotsList[i]);
            }
            dotsList.Clear();
        }
    }

    public void PopulateLetter()
    {
        isMousePressed = false;
        StopAllCoroutines();
        StartCoroutine(StopParticleShower());
        ClearStrokes();
        ClearLines();
        CLearDots();

        try
        {
            if (DisplayAds_IAP.instance != null)
                DisplayAds_IAP.DisplayInterstitial();
        }
        catch (System.Exception)
        { }

        if (currentLetterIndex > 25)
        {
            currentLetterIndex = 0;
        }
        else if (currentLetterIndex < 0)
        {
            currentLetterIndex = 25;
        }
        AssignCurrentSparkles();
        string whereClause = DataFeed.Attributes[0] + "='" + DataFeed.alphabets[currentLetterIndex] + "'";

        currentRow = DAL.getSingleDataRow(whereClause);
        Vector3[][] positionsArrays = (Vector3[][])currentRow[13];
        string[] Attributes = new string[] { "LETTER", "LETTER_INDEX", "STROKE_NUMBER", "POSITIONS", "INITIAL_IMAGE", "FINAL_IMAGE" };
        currentLetterTable = new DataTable();
        for (int col = 0; col < Attributes.Length; col++)
        {
            currentLetterTable.Columns.Add(Attributes[col].ToString());
        }
        currentLetterTable.Columns[1].DataType = typeof(System.Int32);
        currentLetterTable.Columns[2].DataType = typeof(System.Int32);
        currentLetterTable.Columns[3].DataType = typeof(Vector3[][]);
        currentLetterTable.Columns[4].DataType = typeof(Sprite);
        currentLetterTable.Columns[5].DataType = typeof(Sprite);

        int rocount = currentLetterTable.Rows.Count;

        for (int i = 0; i < positionsArrays.Length; i++)
        {
            currentLetterTable.Rows.Add(currentLetterTable.NewRow());
            currentLetterTable.Rows[rocount + i][0] = currentRow[0];
            currentLetterTable.Rows[rocount + i][1] = currentLetterIndex;
            currentLetterTable.Rows[rocount + i][2] = i;
            currentLetterTable.Rows[rocount + i][3] = positionsArrays;//positionsArrays[i]; // S MyCode
            currentLetterTable.Rows[rocount + i][4] = currentRow[11];
            currentLetterTable.Rows[rocount + i][5] = currentRow[12];
        }


        //currentLetterTable = DAL.getSortedResults(whereClause).CopyToDataTable();


        strokeCount = currentLetterTable.Rows.Count;
        currentStrokeIndex = 0;

        //audioSource.PlayOneShot(maleVoiceAlphabets[currentLetterIndex]);
        audioSource.PlayOneShot((AudioClip)currentRow[8]);

        //PopulateStrokePoints();
        handObject.GetComponent<HandMovement>().StopShowingPath();
        StartCoroutine(DisplayWhiteAlphabetRoutine());

    }

    void PopulateStrokePoints()
    {
        startTracing = false;
        handObject.GetComponent<HandMovement>().StopShowingPath();

        CLearDots();

        Vector3[][] temp = (Vector3[][])currentLetterTable.Rows[currentStrokeIndex][3]; // S MyCode

        currentDotPositions = temp[currentStrokeIndex];//(Vector3[])currentLetterTable.Rows[currentStrokeIndex][3]; // S MyCode
        currentDotIndex = 0;
        for (int k = 0; k < currentDotPositions.Length; k++)
        {
            Vector3 currentPos = currentDotPositions[k];
            if (k == 0)
            {
                //GameObject dotObj = Instantiate(dotPrefab_Red, currentPos, Quaternion.identity);
                GameObject dotObj = Instantiate(dotPrefab, currentPos, Quaternion.identity);
                dotObj.GetComponent<SpriteRenderer>().sprite = homeDot;
                dotsList.Add(dotObj);
            }
            else
            {
                //GameObject dotObj = Instantiate(dotPrefab_Orange, currentPos, Quaternion.identity);
                GameObject dotObj = Instantiate(dotPrefab, currentPos, Quaternion.identity);
                dotObj.GetComponent<SpriteRenderer>().sprite = otherDot;
                dotsList.Add(dotObj);
            }
        }
        startTracing = true;
        StartCoroutine(DotBlinkRoutine(dotsList[currentDotIndex]));
        handObject.GetComponent<HandMovement>().waypoints = currentDotPositions;
        handObject.GetComponent<HandMovement>().InitiateShowingPath();
    }

    public void ResetLetter()
    {
        CLearDots();
        ClearLines();
        currentStrokeIndex = 0;
        currentDotIndex = 0;
        //PopulateStrokePoints();
        PopulateLetter();
    }

    void CreateNewLine()
    {

        currentLine = GameObject.Instantiate(linePrefab) as GameObject;
        currentLineRenderer = currentLine.GetComponent<LineRenderer>();
        //currentLineRenderer.SetColors(c1, c1);
        currentLine.name = "Line_L" + currentStrokeIndex + "_D" + currentDotIndex;
        linesList.Add(currentLine);

    }

    void RedrawLine()
    {
        //currentLineRenderer.SetColors(c1, c1);
        pos2.z = 10;
        pos1.z = 10;
        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, pos1);
        currentLineRenderer.SetPosition(1, pos2);
    }

    private bool TouchBegan()
    {
        bool touched = false;
        if (isTouchDevice)
        {
            touched = Input.GetTouch(0).phase.Equals(TouchPhase.Began);
        }
        else
        {
            touched = Input.GetMouseButtonDown(0);
        }
        return touched;
    }

    private bool TouchEnded()
    {
        bool touchEnded = false;
        if (isTouchDevice)
        {
            touchEnded = Input.GetTouch(0).phase.Equals(TouchPhase.Ended);
        }
        else
        {
            touchEnded = Input.GetMouseButtonUp(0);
        }
        return touchEnded;
    }

    private Vector3 GetTouchPosition()
    {
        Vector3 touchPosition;
        if (isTouchDevice)
        {
            //clickDetected = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
            touchPosition = Input.GetTouch(0).position;
        }
        else
        {
            //clickDetected = (Input.GetMouseButtonDown(0));
            touchPosition = Input.mousePosition;
        }
        return touchPosition;
    }

    bool TouchedTheSourcePoint(Vector3 touchedPos)
    {
        return Vector2.Distance(new Vector2(touchedPos.x, touchedPos.y),
            new Vector2(currentDotPositions[currentDotIndex].x, currentDotPositions[currentDotIndex].y)) < 0.7;
    }

    bool TouchedTheDestinationPpoint(Vector3 touchedPos)
    {
        //if (currentDotIndex == (currentDotPositions.Length - 1))
        //{
        //	return Vector2.Distance(new Vector2(touchedPos.x, touchedPos.y), new Vector2(currentDotPositions[0].x, currentDotPositions[0].y)) < 0.5;
        //}
        //else
        //{
        return Vector2.Distance(new Vector2(touchedPos.x, touchedPos.y),
        new Vector2(currentDotPositions[currentDotIndex + 1].x, currentDotPositions[currentDotIndex + 1].y)) < 0.4;
        //}
    }

    IEnumerator FadeOutLineRoutine(GameObject fadeOutObject)
    {
        Color c1 = fadeOutObject.GetComponent<LineRenderer>().startColor;
        for (int i = 0; i < 11; i++)
        {
            c1.a = 1 - (0.1f * i);
            //fadeOutObject.GetComponent<LineRenderer>().SetColors(c1, c1);
            fadeOutObject.GetComponent<LineRenderer>().startColor = c1;
            fadeOutObject.GetComponent<LineRenderer>().endColor = c1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DotBlinkRoutine(GameObject blinkDot)
    {
        yield return new WaitForSeconds(0.1f);
        blink = true;
        while (blink)
        {
            if (blinkDot.GetComponent<SpriteRenderer>().sprite.name.Equals(nextDotBlink.name) && blink)
                blinkDot.GetComponent<SpriteRenderer>().sprite = nextDot;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.1f);
                if (!blink) break;
            }

            if (blinkDot.GetComponent<SpriteRenderer>().sprite.name.Equals(nextDot.name) && blink)
                blinkDot.GetComponent<SpriteRenderer>().sprite = nextDotBlink;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.1f);
                if (!blink) break;
            }
        }
    }

    IEnumerator DisplayWhiteAlphabetRoutine()
    {
        startTracing = false;
        letterRenderer.sprite = (Sprite)currentLetterTable.Rows[0][4];
        Destroy(letterRenderer.gameObject.GetComponent<BoxCollider2D>());
        letterRenderer.gameObject.AddComponent<BoxCollider2D>();

        Color imagecolor = letterRenderer.color;

        for (int i = 0; i < 11; i++)
        {
            imagecolor.a = 0.1f * i;
            letterRenderer.color = imagecolor;
            yield return new WaitForSeconds(0.05f);
        }
        PopulateStrokePoints();
    }

    IEnumerator DisplayColorAlphabetRoutine()
    {
        StartCoroutine(StartParticleShower());
        yield return new WaitForSeconds(1f);
        ClearStrokes();
        ClearLines();
        Color imagecolor = letterRenderer.color;
        for (int i = 0; i < 11; i++)
        {
            imagecolor.a = 1 - (0.1f * i);
            letterRenderer.color = imagecolor;
            yield return new WaitForSeconds(0.05f);
        }
        audioSource.PlayOneShot(appreciations[UnityEngine.Random.Range(0, appreciations.Length)]);

        letterRenderer.sprite = (Sprite)currentLetterTable.Rows[0][5];
        imagecolor = letterRenderer.color;
        for (int i = 0; i < 11; i++)
        {
            imagecolor.a = 0.1f * i;
            letterRenderer.color = imagecolor;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        //ParticleSystem[] particleSystems = sparkles.GetComponentsInChildren<ParticleSystem>();
        StartCoroutine(StopParticleShower());
        yield return new WaitForSeconds(1f);
        currentLetterIndex += 1;
        PopulateLetter();
    }

    IEnumerator StartParticleShower()
    {
        showering = true;
        sparkles.SetActive(true);

        foreach (ParticleSystem item in particleSystems)
        {
            item.Play();
        }

        yield return null;
    }

    IEnumerator StopParticleShower()
    {
        if (showering)
        {
            foreach (ParticleSystem item in particleSystems)
            {
                item.Stop();
            }
            yield return new WaitForSeconds(3f);
            sparkles.SetActive(false);
            showering = false;
        }
        yield return null;
    }

    void AssignCurrentSparkles()
    {
        sparkles = sparkleObjects[UnityEngine.Random.Range(0, sparkleObjects.Length)];
        particleSystems = sparkles.GetComponentsInChildren<ParticleSystem>();
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
