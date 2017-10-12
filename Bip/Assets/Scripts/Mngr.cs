using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mngr : MonoBehaviour {

    public int seed;

    public static Mngr instance;

    public bool gameStarted = false;

    public bool gamePaused = false;

    public bool afterGame = false;


    public float tailleAgents = 0.5f;

    public float speedBoostOnInput = 3;

    public float gainSizeSpeed = 2;

    public float lossSizeSpeed = 0.5f;

    public float avatarMaxResources = 5;

    public bool avatarMinSizeEnabled;

    public float avatarMinSizeTimeBeforeDestroy = 3;

    public float avatarMinSize = 4;

    public float tempsJaugeMax = 3;

    public GameObject countDown;

    public GameObject backLayer;


    public bool musicToogled = true;
    public bool soundToogled = true;
    public bool tutoToogled = false;
    public bool daltonianMode = false;



    public int reds = 0;

    public int blues = 0;

    public int yellows = 0;

    public float timerPerAgentLifepoint = 0.5f;

    private Guru guru;
    private EntitySpawn entitySpawn;

    public GameObject finalEntityDeathParticules;

    // Use this for initialization
    void Awake ()
    {
        instance = this;
        gameStarted = false;
        //Random.InitState(seed);

        //Input.multiTouchEnabled = false;

	}

    void Start()
    {

#if UNITY_STANDALONE
        Screen.SetResolution(800, 1280, true);
#endif
        GetSave();
        guru = Guru.instance;
        entitySpawn = EntitySpawn.instance;
    }

    void Update()
    {
        if (!gameStarted && !afterGame)
        {
            if (Guru.instance.Ressources < Guru.instance.RessourcesMax * 0.75f)
            {
                Guru.instance.Ressources += 2 * Time.deltaTime;
            }

        }
    }

    private void GetSave()
    {
        //HighScore
        ScoreMngr.instance.highScore = PlayerPrefs.GetFloat("High Score");


    }

    public void SetSave()
    {
        PlayerPrefs.SetFloat("High Score", ScoreMngr.instance.highScore);
    }

    public void StartGame()
    {
        Vibration.Vibrate(300);

        gameStarted = true;
        afterGame = false;

        InputMngr.instance.ChangeCurrentUI(InputMngr.UIMenus.Ingame);

        Guru.instance.Ressources = Guru.instance.RessourcesMax * 0.75f;
        Guru.instance.timerJauge = 0;

        EntitySpawn.instance.StartGame();

        SoundStackMngr.instance.PlayLoop(SoundStackMngr.instance.eventGoldState);
    }

	
    public void EndGame()
    {
        gameStarted = false;
        afterGame = true;

        foreach(GameObject entity in EntitySpawn.entitiesAlive)
		{
            StartCoroutine(Destroyer(Instantiate(finalEntityDeathParticules, entity.transform.position, Quaternion.identity), 1));
            Destroy(entity);    
        }
        
        InputMngr.instance.redActivated = false;
        InputMngr.instance.blueActivated = false;
        InputMngr.instance.yellowActivated = false;
        InputMngr.instance.redHalo.SetActive(false);
        InputMngr.instance.blueHalo.SetActive(false);
        InputMngr.instance.greenHalo.SetActive(false);
        InputMngr.instance.ResetButtonsColors();

        InputMngr.instance.ChangeCurrentUI(InputMngr.UIMenus.Restart);

        ScoreMngr.instance.AfterGame();

        SoundStackMngr.instance.StopAllLoops();
        SoundStackMngr.instance.PlayOneShot(SoundStackMngr.instance.eventGameOver);
    }

    public void SetGamePause(bool state)
    {
        if (state)
        {

        }
        gamePaused = state;
    }

    public void Replay()
    {
        afterGame = false;
        //Guru.instance = guru;
        EntitySpawn.instance = entitySpawn;
        ScoreMngr.currentScore = 0;
        ScoreMngr.instance.currentTime = 0;

        SoundStackMngr.instance.PlayLoop(SoundStackMngr.instance.eventInputColor);
    }

    public void EnableMusic()
    {
        musicToogled = true;
    }

    public void DisableMusic()
    {
        musicToogled = false;
    }


    public void EnableSound()
    {
        soundToogled = true;
    }

    public void DisableSound()
    {
        soundToogled = false;
    }

    public void EnableDaltonianMode()
    {
        daltonianMode = true;
    }

    public void DisableDaltonianMode()
    {
        daltonianMode = false;
    }



    public void OnApplicationQuit()
    {
        SetSave();
    }

    public IEnumerator Destroyer(GameObject objectToDestroy, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(objectToDestroy);
    }

}
