using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour {

    [EventRef]
    public string eventBackgroundMusic;

    private FMOD.Studio.EventInstance bckgrndMusic = null;

    public FMOD.Studio.ParameterInstance musicParam;

    public float initialTimeToBaseMusic = 2;

    private float musicParamFloat;

    public static SoundManager instance;

    public void Awake()
    {
        instance = this;
        bckgrndMusic = RuntimeManager.CreateInstance(eventBackgroundMusic);
        bckgrndMusic.getParameter("Parameter 1", out musicParam);
        musicParamFloat = 0.2f;
        musicParam.setValue(musicParamFloat);
        bckgrndMusic.start();
    }

    public void Update()
    {
        if (!Guru.Gold && !Guru.CriticalState && musicParamFloat != 0.4f)
        {
            musicParamFloat = Mathf.MoveTowards(musicParamFloat, 0.4f, initialTimeToBaseMusic * Time.deltaTime);

            musicParam.setValue(musicParamFloat);
        }

        else if (Guru.Gold)
        {
            musicParamFloat = Mathf.MoveTowards(musicParamFloat, 0, initialTimeToBaseMusic * Time.deltaTime);

            musicParam.setValue(musicParamFloat);
        }

        else if (Guru.CriticalState)
        {
            musicParamFloat = Mathf.MoveTowards(musicParamFloat, 0.65f, initialTimeToBaseMusic * Time.deltaTime);

            musicParam.setValue(musicParamFloat);
        }




    }

}
