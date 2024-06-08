using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	#region Variables

	public GameObject allLettersObject;
	public GameObject exitScreen;
	GameController controller;

    #endregion

    #region Unity Methods
   
    void Start()
	{
		controller = Camera.main.GetComponent<GameController>();
	}

	public void NavigateToAnotherLetter(int dir)
	{
        controller.currentLetterIndex += dir;		
		controller.PopulateLetter();
	}

	public void SelectTheLetter(int letterIndex)
	{
        controller.currentLetterIndex = letterIndex;
		controller.PopulateLetter();
		allLettersObject.SetActive(false);
	}

	public void ResetLetter()
	{
        controller.ResetLetter();
	}

	public void ShowAllLetters()
	{
        allLettersObject.SetActive(true);
	}

    public void HideAllLetters()
    {
        allLettersObject.SetActive(false);
    }

    public void QuitGame()
	{
		Application.Quit();
	}

	public void ShowQuitDialog()
	{
        exitScreen.SetActive(true);
	}

	public void HideQuitDialog()
	{
        exitScreen.SetActive(false);
	}	

	public void RateTheApp()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.edubuzzkids.abc_tracing_game_kids_toddlers");
	}

    public void LoadMenuScene()
    {       
        SceneManager.LoadScene("MenuScene");
    }

    #endregion
}
