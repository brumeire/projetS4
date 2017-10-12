using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMngr : MonoBehaviour {


    public float scoreBySec = 10;

    public Text afficheScore;

   //public Text afficheTimer;

    public Text afterGameText;

    public GameObject newHighScore;

    public static float currentScore;

    public static float currentMoney;

    public float currentTime;

    public float highScore;
    

    static float multiplicateur;



    
    public static ScoreMngr instance;

	// Use this for initialization
	void Awake ()
    {
        instance = this;
        currentTime = 0;
        multiplicateur = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Mngr.instance.gameStarted && !Mngr.instance.gamePaused)
        {
            currentTime += Time.deltaTime;

            Scoring();
            Display();
        }
	}


    public static void AddScore(float addScore)
    {
        currentScore += addScore * multiplicateur;
    }

    public static void AddMoney(float addMoney)
    {
        currentMoney += addMoney * multiplicateur;
    }



    public void Scoring()
    {
        if (Mngr.instance.gameStarted && !Mngr.instance.gamePaused)
        {

            if (Guru.instance.Ressources < Guru.instance.RessourcesMax * 0.05f)
            {
                multiplicateur = 1;
                Guru.Gold = false;
            }

            else if (Guru.instance.Ressources < Guru.instance.RessourcesMax * 0.25f)
            {
                multiplicateur = 2;
                Guru.Gold = false;
            }

            else if (Guru.instance.Ressources < Guru.instance.RessourcesMax * 0.5f)
            {
                multiplicateur = 3;
                Guru.Gold = false;
            }

            else if (Guru.instance.Ressources < Guru.instance.RessourcesMax * 0.75f)
            {
                multiplicateur = 4;
                Guru.Gold = false;
            }

            else if (Guru.instance.Ressources < Guru.instance.RessourcesMax * 0.90f)
            {
                multiplicateur = 5;
                Guru.Gold = false;
            }

            else
            {
                multiplicateur = 10;
                Guru.Gold = true;
            }

        }
    }


    void Display()
    {
        afficheScore.text = Mathf.FloorToInt(currentScore).ToString() + "  (x " + multiplicateur + ")";

        //afficheTimer.text = "Temps = " + (Mathf.Round(currentTime * 100) / 100).ToString();
    }


    public void AfterGame()
    {
        if (highScore < currentScore)
        {
            highScore = currentScore;
            newHighScore.SetActive(true);
            Mngr.instance.SetSave();
        }

        string minutes = Mathf.Floor(currentTime / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");


        afterGameText.text = "Score : " + Mathf.FloorToInt(currentScore).ToString() + "\n\nTemps : " + minutes + ":" + seconds
            + "\n\n\nHigh Score : " + Mathf.FloorToInt(highScore).ToString();
    }

}
