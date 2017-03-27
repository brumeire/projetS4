using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour {

    [EventRef]
    public string eventCriticalState;

    private FMOD.Studio.EventInstance criticalStateInstance = null;

    public static SoundManager instance;

    public void Start()
    {
        instance = this;
        criticalStateInstance = RuntimeManager.CreateInstance(eventCriticalState);
    }


    public void PlayCriticalState()
    {
        criticalStateInstance.start();
    }

    public void StopCriticalState()
    {
        criticalStateInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
