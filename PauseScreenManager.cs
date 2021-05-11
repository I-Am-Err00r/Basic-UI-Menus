using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This script can be used as a template for any UI menu type; for the tutorial I made this script for, I discuss a how to make a Pause Screen, but you can use this exact logic to control any menu type and switch between the different screens on that menu.
public class PauseScreenManager : MonoBehaviour
{
    //This will be the default screen that pops up whenever the game is paused and all navigation through the different menu screens will start here
    public GameObject pauseScreen;
    //One of the initial options from the main menu; in short we want to create a reference to each different screen that would pop up, and depending on how complex your UI is, it could be quite a few screens.
    public GameObject optionsScreen;
    //Another initial option from the main menu; just like above varibale states, you will need to make a different GameObject variable for each different screen you want to navigate
    public GameObject areYouSureScreen;
    //The default button that is selected when a particular menu screen is active; for this button, it is the main pause screen
    public Button resumeGameButton;
    //The default button that is selected when a particular menu screen is active; for this button, it is the main options screen
    public Button audioButton;
    //The default button that is selected when a particular menu screen is active; for this button, it is the main are you sure screen
    public Button noButton;
    //A bool that will handle pausing the game when necassary
    private bool paused;
    //A bool that will manage activating and deactivating the main pause screen more automatically; depending on how many different layers each screen has, you might need to make more than one bool like this for each different sub menu in the different screens
    private bool otherMenu;

    //The start method will deactivate all screens so you don't see any UI when the game runs; you could disable the GameObjects of these screens in Unity if you want to manage it manually.
    void Start()
    {
        optionsScreen.SetActive(false);
        pauseScreen.SetActive(false);
        areYouSureScreen.SetActive(false);
    }

    //Runs the Paused method; needs to run through Update because it checks for Input
    void Update()
    {
        Paused();
    }

    //A method that handles toggling on and off the PauseGame State
    public void Paused()
    {
        //Checks for input; in this case it is checking the Escape key, but you can change this input to whatever you would need for your game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Changes the value of the paused bool to whatever it's current opposite value is; so if it is true it will set it to false, and if false it will set it to true
            paused = !paused;
        }
        //If the pause bool is in fact true
        if(paused == true)
        {
            //If the otherMenu bool is also set to false
            if (otherMenu == false)
            {
                //Then the pause screen should be the active screen right now
                pauseScreen.SetActive(true);
            }
            else
            {
                //If the otherMenu bool is in fact true, then the pause screen should not be active
                pauseScreen.SetActive(false);
            }
            //This essentially pauses the game, you might have a different way to pause the game already in your project or not need the game paused, so this line is really for those truly looking for a simple pause
            Time.timeScale = 0;
            //If there currently isn't any button selected in the event system, then it sets the resumeGameButton as selected by default
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                resumeGameButton.Select();
            }
            
        }
        //If the paused is false
        else
        {
            //Set the otherMenu bool to false
            otherMenu = false;
            //Deactivate all the menu screens; you should more screens than this if you made a real menu
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(false);
            areYouSureScreen.SetActive(false);
            //Changes the current selected gameobject in the event system to null
            EventSystem.current.SetSelectedGameObject(null);
            //Activates time again essentially unpausing the game; you might not need this line for your menu
            Time.timeScale = 1;
        }
    }

    //This method will be called by pressing the options button on the UI and will run when setup thorugh the Unity Editor
    public void Options()
    {
        //Sets the otherMenu bool to true
        otherMenu = true;
        //Turns on the options screen
        optionsScreen.SetActive(true);
        //Auto selects the button of your choice when the options is open
        audioButton.Select();
    }

    //This method will be called by pressing the QuitGame button on the pause screen; this is a quick window that pops up to make sure you want to do something
    public void AreYouSure()
    {
        //Sets the otherMenu button to true
        otherMenu = true;
        //Sets the areYouSureScreen as the active screen
        areYouSureScreen.SetActive(true);
        //Auto selects the no button from the areYouSure screen
        noButton.Select();
    }

    //This method will be called by pressing the Back button on the UI and will run when setup thorugh the Unity Editor
    public void Back()
    {
        //If the options screen is active, sets it back to inactive
        optionsScreen.SetActive(false);
        //If the areYouSure screen is active, sets it back to inactive
        areYouSureScreen.SetActive(false);
        //Sets the otherMenu bool to false
        otherMenu = false;
        //Sets the pauseScreen to active here so it can auto select the resumeGameButton
        pauseScreen.SetActive(true);
        //Selects the default button when the pause screen is active
        resumeGameButton.Select();
    }

    //This method will be called by pressing the yes button on the areYouSure screen; this will stop playtesting if in Unity, or quit the game if not in Unity
    public void QuitGame()
    {
        //This code will only run if playtesting in Unity; we do this so if we want to build the game, this code will be ommitted from the actual build, allowing you to playtest as a developer and not have to delete chunks of code when you need to actually build the game
        #if UNITY_EDITOR
        if (Application.isEditor)
        {
            //Stops playtesting if playing in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        else
        {
            //Quits the application if you are playing a built version of your game
            Application.Quit();
        }
    }

    //Runs whenever the ResumeGame button is pressed from the main pause screen
    public void UnPause()
    {
        paused = false;
    }
}