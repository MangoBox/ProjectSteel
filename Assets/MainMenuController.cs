using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    //This is a class specifically for controlling the main menu.
    //To be placed on the Canvas object.

    //The main menu GameObject, to be enabled and disabled when needed.
    public GameObject mainMenu;
    //Exit prompt, to be enabled and disabled when needed.
    public GameObject exitPrompt;
    //About screen, to be enabled and disabled when needed.
    public GameObject aboutMenu;
    //Loading screen, to be enabled and disabled when needed.
    public GameObject loadScreen;
    //The pre-game load screen.
    public GameObject playSettingsScreen;
    public InputField seedField;

    //The generation settings to be submitted to MapGenerator, which is updated every time the player changes a value.
    public static GenerationSettings finalGenerationSettings;
    //The default generation settings
    public static GenerationSettings defaultGenerationSettings;


    //Generation settings
    public Slider lacunaritySlider;
    public Slider persistanceSlider;
    public Slider noiseSlider;
    public Slider octavesSlider;

    public Text lacunarityText;
    public Text persistanceText;
    public Text noiseText;
    public Text octavesText;

    public float lacunarityDefault;
    public float persistanceDefault;
    public float noiseDefault;
    public int octavesDefault;
    public int seed;



	// Use this for initialization
	void Start () {
        //Ensures the current screens are enabled upon load.
        mainMenu.SetActive(true);
        exitPrompt.SetActive(false);
        aboutMenu.SetActive(false);
        playSettingsScreen.SetActive(false);

	}

    void Awake()
    {
        setDefaultGenerationSettings();
    }
	
	// Update is called once per frame
	void Update () {
	
        //Just constantly applies slider values to the text below them.
        //Check if this can be revised later.
        applyGenerationSettingsToFields();
	}

    //When a button is clicked, called directly by the button.
    public void onPlayButtonClicked()
    {
        mainMenu.SetActive(false);
        playSettingsScreen.SetActive(true);

        //Sets the map generation values back to default when the menu is loaded.
        setDefaultGenerationSettings();

    }

    public GenerationSettings returnGenerationSettings()
    {
        GenerationSettings returnSettings;
        returnSettings.lacunarity = lacunaritySlider.value;
        returnSettings.noiseScale = noiseSlider.value;
        returnSettings.persistance = persistanceSlider.value;
        //Simply converts the string into a integer hashcode.
        returnSettings.seed = seedField.text.GetHashCode();
        //An explicit cast is fine considering the slider is forced to use whole numbers.
        returnSettings.octaves = (int) octavesSlider.value;
        //Returns the settings.
        return returnSettings;
    }

    //Very similar to the method above, however it returns the default values instead.
    public GenerationSettings returnDefaultGenerationSettings()
    {
        GenerationSettings returnSettings;
        returnSettings.lacunarity = lacunarityDefault;
        returnSettings.noiseScale = noiseDefault;
        returnSettings.octaves = octavesDefault;
        returnSettings.persistance = persistanceDefault;
        //Left empty upon initialisation of the default settings, will be controlled by logic later.
        returnSettings.seed = 0;
        return returnSettings;

    }

    //Sets the finalGenerationSettings seed to the hashcode of the seed.
    public void seedChanged()
    {
        if (seedField.text != null && seedField.text != "")
        {
            finalGenerationSettings.seed = seedField.text.GetHashCode();
        } else
        {
            //Sets the users seed to 0 if they have not defined one.
            finalGenerationSettings.seed = 0;
        }
    }

    public void setDefaultGenerationSettings()
    {
        //Unfortunately, we are not able to set the values of defaultGenerationSettings upon declaration, but this way should be fine.
        finalGenerationSettings.lacunarity = 2;
        finalGenerationSettings.octaves = 3;
        finalGenerationSettings.persistance = 0.2f;
        finalGenerationSettings.noiseScale = 60;
        setSliderValues(finalGenerationSettings);
    }



    //Applies the generation settings to the fields.
    public void applyGenerationSettingsToFields(GenerationSettings genSettings)
    {
        lacunarityText.text = genSettings.lacunarity.ToString();
        persistanceText.text = genSettings.persistance.ToString();
        octavesText.text = genSettings.octaves.ToString();
        noiseText.text = genSettings.noiseScale.ToString();
    }

    //Overload (Not really, but close enough) for the method above, just applying the bar values to the text.
    public void applyGenerationSettingsToFields()
    {
        lacunarityText.text = lacunaritySlider.value.ToString();
        persistanceText.text = persistanceSlider.value.ToString();
        octavesText.text = octavesSlider.value.ToString();
        noiseText.text = noiseSlider.value.ToString();
    }


    public void setSliderValues(GenerationSettings genSettings)
    {
        lacunaritySlider.value = genSettings.lacunarity;
        persistanceSlider.value = genSettings.persistance;
        noiseSlider.value = genSettings.noiseScale;
        octavesSlider.value = genSettings.octaves;
    }


    public void onGenerateButtonClicked()
    {
        playSettingsScreen.SetActive(false);
        loadScreen.SetActive(true);
        //Sets the seed to the system time if the user has not defined a value.
        if (finalGenerationSettings.seed == 0)
        {
            finalGenerationSettings.seed = System.DateTime.Now.Millisecond;
        }
        Application.LoadLevel("ProceduralWorld");
    }

    public void onAboutButtonClicked()
    {
        mainMenu.SetActive(false);
        aboutMenu.SetActive(true);
        exitPrompt.SetActive(false);
    }

    public void onExitButtonClicked()
    {
        //Opens the exit prompt.
        exitPrompt.SetActive(true);

    }
    
    //Buttons relevant to the exit prompt.
    public void onQuitButtonClicked()
    {
        //Quits the application.
        Application.Quit();
    }

    public void onBackButtonClicked()
    {
        //Closes the exit prompt.
        exitPrompt.SetActive(false);
    }

    //This is the back button in the about menu()
    public void onAboutBackButtonClicked()
    {
        aboutMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void onPlaySettingsBackButtonClicked()
    {
        playSettingsScreen.SetActive(false);
        mainMenu.SetActive(true);
    }
}

//A static struct for passing generation settings from the menu to the level.
//Its worth noting that static objects can be passed between levels.
public struct GenerationSettings
{
    //TODO: Check if containing variables need to be defined as static in addition to struct definition.
    public float lacunarity;
    public float persistance;
    public float noiseScale;
    public int octaves;
    public int seed;
}
