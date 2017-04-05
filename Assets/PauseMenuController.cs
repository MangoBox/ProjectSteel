using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuController : MonoBehaviour {
    //This class is intended for the control of the pause menu, including the options menu.
    //Variables for the pause menu
    
    //Option display settings, all read-only

    int defaultResolutionIndex = 7; //This is intended for referencing to the default resoltion, 1920x1080

    //The pause screen.
    public GameObject pauseScreen;
    public GameObject optionsScreen;

    //Data to be loaded upon runtime begin.
    public ResolutionData currentResData;
    //Data to be loaded when user modifies resolutions.
    private ResolutionData tempResData = new ResolutionData(6, false);

    //The options screen.
    public Text resolutionText;

	// Use this for initialization
	void Start () {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(false);

        tempResData = new ResolutionData(6, Screen.fullScreen);
        
        ResolutionData resData = new ResolutionData(6, true);
        resData.applySettings();
        resolutionText.text = resData.displayResName;
	}
	
	// Update is called once per frame
	void Update () {
        //If escape button is held
        if (Input.GetKeyDown(KeyCode.Escape) && optionsScreen.activeInHierarchy == false)
        {
            pauseScreen.SetActive(true);
        }
	}

    //Button from pause screen to close menu.
    public void onContinueButtonPressed()
    {
        pauseScreen.SetActive(false);
    }

    public void onBackOptionsButtonPressed()
    {
        optionsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void onExitButtonPressed()
    {
        Application.LoadLevel("MainMenu");
    }

    public void onOptionsButtonPressed()
    {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void onLeftResolutionButtonPressed()
    {
        tempResData.previousResolution();
        resolutionText.text = tempResData.displayResName;
    }

    public void onRightResolutionButtonPressed()
    {
        tempResData.nextResolution();
        resolutionText.text = tempResData.displayResName;
        print(tempResData.resIndex);

    }

    public void onApplyButtonPressed()
    {
        currentResData = tempResData;
        currentResData.applySettings();
        print(tempResData.displayResName);
    }

}

//A custom class + constructor specifcally for using and managing resolution data.
public class ResolutionData
{
    public int screenHeight;
    public int screenWidth;
    public string displayResName;
    public bool isFullscreen;

    public int resIndex;

    int[] widthArray = { 800, 1024, 1280, 1366, 1440, 1600, 1920 };
    int[] heightArray = { 600, 768, 800, 768, 900, 900, 1080 };
    string[] resolutionArray = { "800 x 600", "1024 x 768", "1280 x 800", "1366 x 768", "1440 x 900", "1600 x 900", "1920 x 1080" };

    public ResolutionData(int resolutionIndex, bool fullscreen)
    {
        screenHeight = heightArray[resolutionIndex];
        screenWidth = widthArray[resolutionIndex];
        displayResName = resolutionArray[resolutionIndex];
        isFullscreen = fullscreen;
        resIndex = resolutionIndex;
    }

    //Applies the settings
    public void applySettings()
    {
        Screen.SetResolution(screenWidth, screenHeight, isFullscreen);
    }

    //Cycles to the previous resolution.
    public void previousResolution()
    {
        resIndex = resIndex <= 0 ? resolutionArray.Length - 1 : resIndex--;
    }

    //Cycles to the next resolution.
    public void nextResolution()
    {
        resIndex = resIndex >= resolutionArray.Length - 1 ? 0 : resIndex++;
    }

}