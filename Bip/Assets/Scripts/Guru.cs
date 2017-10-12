using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Guru : MonoBehaviour {

    #region variables
    public enum signaux {
        signalA,
        signalB,
        signalC,
        signalD
    }
    public signaux signal;

    float ressources;
    public float lostOnFail = 0.5f;


	public bool inputRed = false;
	public bool inputBlue = false;
	public bool inputGreen = false;


    [HideInInspector] public float RessourcesMax = 5;
    [HideInInspector] public float SizeMin = 0.59f;
	static bool gold = false;

    [HideInInspector]
    public float size;

    public Material[] materials;

    public GameObject jauge;

    public GameObject goldParticuleSystem;

    public GameObject critParticuleSystem;

    [HideInInspector]
    public float timerJauge = 0;


    private float timerDeath = 0;
    private bool timerDeathLaunched = false;




    public static bool Gold
    {
        get
        {
            return gold;
        }

        set
        {
            if (value != gold)
            {
                gold = value;
                if (gold)
                {
                    //Mngr.instance.backLayer.GetComponent<SpriteRenderer>().color = Color.yellow;
                    instance.goldParticuleSystem.SetActive(true);

                    //Param sound gold state
                    SoundStackMngr.instance.SetEventParameter(SoundStackMngr.instance.eventGoldState, "Gold", 1);
                }

                else
                {
                    //Mngr.instance.backLayer.GetComponent<SpriteRenderer>().color = Color.white;
                    instance.goldParticuleSystem.SetActive(false);

                    //Param sound gold state
                    SoundStackMngr.instance.SetEventParameter(SoundStackMngr.instance.eventGoldState, "Gold", 0);
                }

                

            }
        }
    }

    public static bool CriticalState = false;
    public float Ressources
    {
        get
        {
            return ressources;
        }

        set
        {

            if (CriticalState)
            {
                if (value > 0)
                    timerJauge -= Time.deltaTime * 2;
                else
                    timerJauge += Time.deltaTime;

                if (timerJauge < 0)
                {
                    timerJauge = 0;
                    CriticalState = false;
                    jauge.SetActive(false);
                }

            }

            else if (ressources > 0 && value <= 0)
            {
                CriticalState = true;
                jauge.SetActive(true);
                ressources = value;
            }

            else
            {
                ressources = value;
            }
           

            if (ressources > RessourcesMax)
            {
                ressources = RessourcesMax;
            }

            else if (ressources < 0 /*&& !Mngr.instance.avatarMinSizeEnabled*/)
            {
                ressources = 0;
            }
        }
    }

    public static Guru instance;

    #endregion variables

    #region Unity Functions
    // Use this for initialization
    void Start () {
        instance = this;
        RessourcesMax = Mngr.instance.avatarMaxResources;
		gameObject.GetComponent<Renderer> ().material.color = Color.black;

    }


    void Update () {

        size = Mathf.Lerp(SizeMin * 2, RessourcesMax, Ressources / RessourcesMax);
        transform.localScale = new Vector3(size, size, 0.1f);

        if (Mngr.instance.gameStarted && !Mngr.instance.gamePaused)
        {

            //if (InputMngr.instance.redActivated || InputMngr.instance.blueActivated || InputMngr.instance.yellowActivated)
            //{
                Influence();
            //}
          


            //transform.localScale = new Vector3(Ressources * 2, Ressources * 2, 0.1f);

                
            if (CriticalState)
            {
                //timerJauge += Time.deltaTime;
                jauge.GetComponent<Image>().fillAmount = timerJauge / Mngr.instance.tempsJaugeMax;

                if (timerJauge >= Mngr.instance.tempsJaugeMax)
                {
                    CriticalState = false;
                    jauge.SetActive(false);
                    Mngr.instance.EndGame();
                }
            }


        }

       


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, size);
    }


    #endregion Unity Functions

    #region Custom Functions
    void Influence ()
    {
        Vector3 pos = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(pos,size);

        int numbOfRed = 0;
        int numbOfBlue = 0;
        int numbOfGreen = 0;

        foreach (Collider col in hitColliders)
        {
            col.GetComponent<EntityScript>().overlapAvatar = true;

            switch (col.tag)
            {
                case "A":
                    numbOfRed++;
                    break;

                case "B":
                    numbOfBlue++;
                    break;

                case "C":
                    numbOfGreen++;
                    break;

            }
            
                
        }

        float modifRessource = 0;




        if (InputMngr.instance.redActivated
            && (
                (numbOfRed > numbOfBlue && numbOfRed > numbOfGreen)
                || (numbOfRed == numbOfBlue && numbOfRed > 0 && numbOfRed > numbOfGreen)
                || (numbOfRed == numbOfGreen && numbOfRed > 0 && numbOfRed > numbOfBlue)
                )
            )
        {
            modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime * numbOfRed;
        }


        else if (InputMngr.instance.blueActivated
            && (
                (numbOfBlue > numbOfRed && numbOfBlue > numbOfGreen)
                || (numbOfBlue == numbOfRed && numbOfBlue > 0 && numbOfBlue > numbOfGreen)
                || (numbOfBlue == numbOfGreen && numbOfBlue > 0 && numbOfBlue > numbOfRed)
                )
            )
        {
            modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime * numbOfBlue;
        }


        else if (InputMngr.instance.yellowActivated
            && (
                (numbOfGreen > numbOfRed && numbOfGreen > numbOfBlue)
                || (numbOfGreen == numbOfRed && numbOfGreen > 0 && numbOfGreen > numbOfBlue)
                || (numbOfGreen == numbOfBlue && numbOfGreen > 0 && numbOfGreen > numbOfRed)
                )
            )
        {
            modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime * numbOfGreen;
        }

        else if ((InputMngr.instance.redActivated || InputMngr.instance.blueActivated || InputMngr.instance.yellowActivated) 
            && (numbOfRed == numbOfBlue && numbOfRed == numbOfGreen && numbOfRed > 0)
            )
        {
            modifRessource += numbOfRed;
        }


        
        else if (numbOfRed + numbOfBlue + numbOfGreen > 0)
        {
            Ressources -= Time.deltaTime * Mngr.instance.lossSizeSpeed * (numbOfRed + numbOfBlue + numbOfGreen);

            if (InputMngr.instance.yellowActivated || InputMngr.instance.blueActivated || InputMngr.instance.redActivated)
            {
                Ressources -= lostOnFail * Time.deltaTime;
            }
        }

        else if (InputMngr.instance.yellowActivated || InputMngr.instance.blueActivated || InputMngr.instance.redActivated)
        {
            Ressources -= lostOnFail * Time.deltaTime;
        }


        if (modifRessource > 0)
        {
            Ressources += Mngr.instance.gainSizeSpeed * Time.deltaTime;
            ScoreMngr.AddScore(modifRessource);
        }

    }


    int ShouldLooseSize()
    {

        Vector3 pos = transform.position;
        float res = Ressources;
        int layerMask = 1 << 8;

        Collider[] hitColliders = Physics.OverlapSphere(pos, res, layerMask);

        if (hitColliders.Length > 0)
        {
            return hitColliders.Length;
        }

        else
            return 0;
    }

	


    #endregion Custom Functions
}
