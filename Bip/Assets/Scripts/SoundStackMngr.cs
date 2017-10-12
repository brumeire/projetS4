using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SoundStackMngr : MonoBehaviour {


    public float timeBetweenSounds = 0.1f;

    [EventRef]
    public string eventInputColor;

    //Input Sound Parameters [0-1]
    private float redInputParam = 0;
    private float blueInputParam = 0;
    private float greenInputParam = 0;

    public float inputParamMoveSpeed = 2;

    [EventRef]
    public string eventInputMenu;

    [EventRef]
    public string eventGoldState;

    [EventRef]
    public string eventGameOver;


    [HideInInspector]
    public List<string> destructionStack = new List<string>();


    private List<EventInstance> soundLoops = new List<EventInstance>();
    private List<string> loopNameList = new List<string>();


    private float timerDestruction = 0;

    public static SoundStackMngr instance;

	// Use this for initialization
	void Start ()
    {
        instance = this;

        PlayLoop(eventInputColor);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Mngr.instance.gameStarted)
        {
            //SoundStack (very short delay between simultaneous sounds)
            timerDestruction += Time.deltaTime;
            if (destructionStack.Count > 0 && timerDestruction >= timeBetweenSounds)
            {
                timerDestruction = 0;
                PlayOneShot(destructionStack[0]);
                destructionStack.RemoveAt(0);
            }



            redInputParam = Mathf.Clamp01(redInputParam - inputParamMoveSpeed * Time.deltaTime);
            blueInputParam = Mathf.Clamp01(blueInputParam - inputParamMoveSpeed * Time.deltaTime);
            greenInputParam = Mathf.Clamp01(greenInputParam - inputParamMoveSpeed * Time.deltaTime);

            switch (InputMngr.instance.currentInput)
            {
                case EntityScript.Type.Red:
                    redInputParam = Mathf.Clamp01(redInputParam + inputParamMoveSpeed * Time.deltaTime) * 2;
                    break;

                case EntityScript.Type.Blue:
                    blueInputParam = Mathf.Clamp01(blueInputParam + inputParamMoveSpeed * Time.deltaTime) * 2;
                    break;

                case EntityScript.Type.Yellow:
                    greenInputParam = Mathf.Clamp01(greenInputParam + inputParamMoveSpeed * Time.deltaTime) * 2;
                    break;
            }

            SetEventParameter(eventInputColor, "VolumeRed", redInputParam);
            SetEventParameter(eventInputColor, "VolumeBlue", blueInputParam);
            SetEventParameter(eventInputColor, "VolumeGreen", greenInputParam);

        }
	}


    public void PlayLoop(string eventName)
    {
        if (!loopNameList.Contains(eventName))
        {
            loopNameList.Add(eventName);
            soundLoops.Add(RuntimeManager.CreateInstance(eventName));
        }

        soundLoops[loopNameList.IndexOf(eventName)].start();
    } 

    public void StopLoop(string eventName)
    {
        if (loopNameList.Contains(eventName))
        {
            soundLoops[loopNameList.IndexOf(eventName)].stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void PlayOneShot(string eventName)
    {
        RuntimeManager.PlayOneShot(eventName);
    }

    public void StopAllLoops()
    {
        foreach(EventInstance loopEvent in soundLoops)
        {
            loopEvent.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void SetEventParameter(string eventName, string parameterName, float value)
    {
        soundLoops[loopNameList.IndexOf(eventName)].setParameterValue(parameterName, value);
    }

}
