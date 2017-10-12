using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputMngr : MonoBehaviour {


    /*public Transform[] redButtons;

    public Transform[] blueButtons;

    public Transform[] yellowButtons;*/


    public GameObject[] coulees;

    public GameObject options;
    bool optionsOpen = false;


    public GameObject ingameCanvas;

    public GameObject mainMenuCanvas;

	public GameObject skinsMenu;
	bool skinsOpen = false;

    public GameObject restartCanvas;

    public GameObject pauseCanvas;

    public GameObject redHalo;

    public GameObject blueHalo;

    public GameObject greenHalo;

    public Toggle musicToggle;
    
    public Toggle soundToggle;

    public Toggle daltonianToggle;

    public Toggle tutoToggle;

    public InputType[] inputs;

    public enum Side { Left, Right}


    public enum UIMenus
    {
        Ingame,
        MainMenu,
        Restart,

    }


    public static InputMngr instance;

    public bool rightActivated;

    public EntityScript.Type rightPushed;

    public bool leftActivated;

    public EntityScript.Type leftPushed;

    public bool redActivated;

    public bool blueActivated;

    public bool yellowActivated;

    public bool pushed = false;

    public EntityScript.Type currentInput;


    void Start()
    {
        instance = this;
        rightActivated = false;
        leftActivated = false;
        rightPushed = EntityScript.Type.None;
        leftPushed = EntityScript.Type.None;
        options.SetActive(false);
        optionsOpen = false;
		//skinsMenu.SetActive (false);
		skinsOpen = false;


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Mngr.instance.SetGamePause(!Mngr.instance.gamePaused);
        }

        if (Input.GetKey(KeyCode.F1))
        {
            Guru.instance.Ressources += 5 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.F2))
        {
            Guru.instance.Ressources -= 5 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Pushed(inputs[0]);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Pushed(inputs[1]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Pushed(inputs[2]);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Released(inputs[0]);
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            Released(inputs[1]);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Released(inputs[2]);
        }

    }


    //In-Game Canvas
    public void Pushed (InputType input)
    {
        redActivated = false;
        blueActivated = false;
        yellowActivated = false;

        redHalo.SetActive(false);
        blueHalo.SetActive(false);
        greenHalo.SetActive(false);

      
        switch (input.type)
        {
            case EntityScript.Type.Red: 
                redActivated = true;
                redHalo.SetActive(true);
                currentInput = EntityScript.Type.Red;
                Vibration.Vibrate(50);
                break;

            case EntityScript.Type.Blue:                    
                blueActivated = true;
                blueHalo.SetActive(true);
                currentInput = EntityScript.Type.Blue;
                Vibration.Vibrate(50);
                break;

            case EntityScript.Type.Yellow:
                yellowActivated = true;
                greenHalo.SetActive(true);
                currentInput = EntityScript.Type.Yellow;
                Vibration.Vibrate(50);
                break;
        }
            
        ResetButtonsColors();
		//FMODUnity.RuntimeManager.PlayOneShot (sonInput);        
    }

    public void PauseButton ()
    {
        if (!Mngr.instance.gamePaused)
        {
            pauseCanvas.SetActive(true);
        }

        else
        {
            pauseCanvas.SetActive(false);
        }

        SoundStackMngr.instance.PlayOneShot(SoundStackMngr.instance.eventInputMenu);
        Mngr.instance.SetGamePause(!Mngr.instance.gamePaused);
    }


    public void Released (InputType input)
    {
        switch (input.type)
        {
            case EntityScript.Type.Red:
                redActivated = false;
                redHalo.SetActive(false);
                break;

            case EntityScript.Type.Blue:
                blueActivated = false;
                blueHalo.SetActive(false);
                break;

            case EntityScript.Type.Yellow:
                yellowActivated = false;
                greenHalo.SetActive(false);
                break;

        }

        if (!redActivated && !blueActivated && !yellowActivated)
        {
            currentInput = EntityScript.Type.None;
        }

        ResetButtonsColors();
        


    }

    public void ChangeCurrentUI(UIMenus uiToShow)
    {
        ingameCanvas.SetActive(false);
        restartCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);

        switch (uiToShow)
        {
            case UIMenus.Ingame:
                ingameCanvas.SetActive(true);
                break;

            case UIMenus.MainMenu:
                mainMenuCanvas.SetActive(true);
                break;

            case UIMenus.Restart:
                restartCanvas.SetActive(true);
                break;
        } 
    }
    
    //Main Menu Canvas Functions
    public void PlayButton()
    {
        SoundStackMngr.instance.PlayOneShot(SoundStackMngr.instance.eventInputMenu);
        Mngr.instance.StartGame();
        options.SetActive(false);
        optionsOpen = false;
		//skinsMenu.SetActive (false);
		skinsOpen = false;
    }


    public void SkinsButton()
    {
        SoundStackMngr.instance.PlayOneShot(SoundStackMngr.instance.eventInputMenu);
        options.SetActive(false);
        optionsOpen = false;
		skinsOpen = !skinsOpen;
		skinsMenu.SetActive (skinsOpen);
    }


    public void OptionsButton()
    {
        SoundStackMngr.instance.PlayOneShot(SoundStackMngr.instance.eventInputMenu);
        optionsOpen = !optionsOpen;
        options.SetActive(optionsOpen);
		skinsOpen = false;
		//skinsMenu.SetActive (false);
    }

    //Options functions
    public void ToggleMusic()
    {
        if (musicToggle.isOn)
        {
            Mngr.instance.EnableMusic();
        }

        else
        {
            Mngr.instance.DisableMusic();
        }

        musicToggle.GetComponent<Image>().fillCenter = musicToggle.isOn;
    }

    public void ToggleSound()
    {
        if (soundToggle.isOn)
        {
            Mngr.instance.EnableSound();
        }

        else
        {
            Mngr.instance.DisableSound();
        }

        soundToggle.GetComponent<Image>().fillCenter = soundToggle.isOn;
    }

    public void ToggleDaltonianMode()
    {
        if (daltonianToggle.isOn)
        {
            Mngr.instance.EnableDaltonianMode();
        }

        else
        {
            Mngr.instance.DisableDaltonianMode();
        }

        daltonianToggle.GetComponent<Image>().fillCenter = daltonianToggle.isOn;
    }

    public void ToggleTuto()
    {
        Mngr.instance.tutoToogled = tutoToggle.isOn;

        tutoToggle.GetComponent<Image>().fillCenter = tutoToggle.isOn;
    }

    //Restart Menu Canvas Functions

    public void ReturnToMainMenuButton()
    {
        SoundStackMngr.instance.PlayOneShot(SoundStackMngr.instance.eventInputMenu);
        Mngr.instance.Replay();
        ChangeCurrentUI(UIMenus.MainMenu);
    }

    public void RestartButton()
    {
        /*SceneManager.LoadScene(0, LoadSceneMode.Single);
        PlayButton();
        options.SetActive(false);
        optionsOpen = false;
		skinsOpen = false;
		//skinsMenu.SetActive (false);*/
        SoundStackMngr.instance.PlayOneShot(SoundStackMngr.instance.eventInputMenu);
        Mngr.instance.Replay();
        PlayButton();
    }
		



    public void ResetButtonsColors()
    {

        foreach(GameObject go in coulees)
        {
            go.SetActive(false);
        }

        if (redActivated)
            coulees[0].SetActive(true);

        else if (blueActivated)
            coulees[1].SetActive(true);

        else if (yellowActivated)
            coulees[2].SetActive(true);


    } 


}
