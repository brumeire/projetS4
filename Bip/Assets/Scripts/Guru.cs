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

    public float Ressources;
    public float lostPerTime;
    public float lostOnFail = 0.5f;


	public bool inputRed = false;
	public bool inputBlue = false;
	public bool inputGreen = false;


    public float timer;
    public Text score;
    public float RessourcesMax = 7;
    public float Startmulti = 6;
    float timerMulti;
    public float upMulti = 2;
    public float multiplicateur = 1;


    public Material[] materials;
    public GameObject restartButton;


    private float timerDeath = 0;
    private bool timerDeathLaunched = false;


    public static Guru instance;

    #endregion variables

    #region Unity Functions
    // Use this for initialization
    void Start () {
        instance = this;
		gameObject.GetComponent<Renderer> ().material.color = Color.black;
        multiplicateur = 1;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(Ressources);
        //Scoring();
        //Multi();
        
        Ressources -= Time.deltaTime * lostPerTime;

		if (InputMngr.instance.redActivated || InputMngr.instance.blueActivated || InputMngr.instance.yellowActivated)
        {
		    Influence();
		}


        if (Ressources <= 0 && !Mngr.instance.avatarMinSizeEnabled)
        {
            Ressources = 0;
            restartButton.SetActive(true);
            Destroy(gameObject);
        }

        if (Ressources >= RessourcesMax)
            Ressources = RessourcesMax;
        

        if (Mngr.instance.avatarMinSizeEnabled)
        {
            if (Ressources <= Mngr.instance.avatarMinSize)
            {
                Ressources = Mngr.instance.avatarMinSize;

                if (!timerDeathLaunched)
                {
                    timerDeathLaunched = true;
                    timerDeath = Mngr.instance.avatarMinSizeTimeBeforeDestroy;
                    Mngr.instance.countDown.SetActive(true);
                    SoundManager.instance.PlayCriticalState();
                }

                timerDeath -= Time.deltaTime;

                Mngr.instance.countDown.GetComponent<Text>().text = Mathf.CeilToInt(timerDeath).ToString();

                if (timerDeath <= 0)
                {
                    restartButton.SetActive(true);
                    Mngr.instance.countDown.SetActive(false);
                    SoundManager.instance.StopCriticalState();
                    Destroy(gameObject);
                }
            }

            else if (timerDeathLaunched)
            {
                timerDeathLaunched = false;
                timerDeath = Mngr.instance.avatarMinSizeTimeBeforeDestroy;
                Mngr.instance.countDown.SetActive(false);
                SoundManager.instance.StopCriticalState();
            }
        }


        transform.localScale = new Vector3(Ressources * 2, Ressources * 2, 0.1f);




       /*if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            signal = signaux.signalA;
            Influence();
        }
       
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            signal = signaux.signalB;
            Influence();
        }
        

        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            signal = signaux.signalC;
            Influence();
        }*/

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Ressources);
    }


    #endregion Unity Functions

    #region Custom Functions
    void Influence ()
    {
        Vector3 pos = transform.position;
        float res = Ressources;

        Collider[] hitColliders = Physics.OverlapSphere(pos,res);

        int numbOfRed = 0;
        int numbOfBlue = 0;
        int numbOfGreen = 0;

        foreach (Collider col in hitColliders)
        {
           
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
                (numbOfRed > numbOfBlue && numbOfRed > numbOfGreen && !InputMngr.instance.blueActivated && !InputMngr.instance.yellowActivated)
                || (numbOfRed == numbOfBlue && numbOfRed > 0 && InputMngr.instance.blueActivated && !InputMngr.instance.yellowActivated)
                || (numbOfRed == numbOfGreen && numbOfRed > 0 && InputMngr.instance.yellowActivated && !InputMngr.instance.blueActivated))
                )


        /*(numbOfRed > numbOfBlue || (numbOfRed == numbOfBlue && numbOfRed >= numbOfGreen && numbOfRed > 0)) 
        && (numbOfRed > numbOfGreen || (numbOfRed == numbOfGreen && numbOfRed >= numbOfBlue && numbOfRed > 0))
        && InputMngr.instance.redActivated)*/
        {
            modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
            Collider[] targets = Physics.OverlapSphere(pos, res);

            foreach (Collider col in targets)
            {
                if (col.tag == "A")
                {
                    col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                    JaugeManager.instance.FillJauge(EntityScript.Type.Red);
                    //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                }
            }


            if ((numbOfBlue > numbOfGreen || (numbOfBlue == numbOfGreen && numbOfBlue > 0)) && InputMngr.instance.blueActivated)
            {
                modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
                Collider[] otherTargets = Physics.OverlapSphere(pos, res);

                foreach (Collider col in targets)
                {
                    if (col.tag == "B")
                    {
                        col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                        JaugeManager.instance.FillJauge(EntityScript.Type.Blue);
                        //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                    }
                }
            }

            else if ((numbOfGreen > numbOfBlue || (numbOfGreen == numbOfBlue && numbOfGreen > 0)) && InputMngr.instance.yellowActivated)
            {
                modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
                Collider[] otherTargets = Physics.OverlapSphere(pos, res);

                foreach (Collider col in targets)
                {
                    if (col.tag == "C")
                    {
                        col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                        JaugeManager.instance.FillJauge(EntityScript.Type.Yellow);
                        //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                    }
                }
            }

        }





        else if (InputMngr.instance.blueActivated
            && (
                (numbOfBlue > numbOfRed && numbOfBlue > numbOfGreen && !InputMngr.instance.redActivated && !InputMngr.instance.yellowActivated)
                || (numbOfBlue == numbOfRed && numbOfBlue > 0 && InputMngr.instance.redActivated && !InputMngr.instance.yellowActivated)
                || (numbOfBlue == numbOfGreen && numbOfBlue > 0 && InputMngr.instance.yellowActivated && !InputMngr.instance.redActivated))
                )
        {
            modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
            Collider[] targets = Physics.OverlapSphere(pos, res);
            foreach (Collider col in targets)
            {
                if (col.tag == "B")
                {
                    col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                    JaugeManager.instance.FillJauge(EntityScript.Type.Blue);
                    //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                }
            }


            if ((numbOfRed > numbOfGreen || (numbOfRed == numbOfGreen && numbOfRed > 0)) && InputMngr.instance.redActivated)
            {
                modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
                Collider[] otherTargets = Physics.OverlapSphere(pos, res);

                foreach (Collider col in targets)
                {
                    if (col.tag == "A")
                    {
                        col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                        JaugeManager.instance.FillJauge(EntityScript.Type.Red);
                        //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                    }
                }
            }

            else if ((numbOfGreen > numbOfRed || (numbOfGreen == numbOfRed && numbOfGreen > 0)) && InputMngr.instance.yellowActivated)
            {
                modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
                Collider[] otherTargets = Physics.OverlapSphere(pos, res);

                foreach (Collider col in targets)
                {
                    if (col.tag == "C")
                    {
                        col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                        JaugeManager.instance.FillJauge(EntityScript.Type.Yellow);
                        //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                    }
                }
            }

        }





        else if (InputMngr.instance.yellowActivated
            && (
                (numbOfGreen > numbOfRed && numbOfGreen > numbOfBlue && !InputMngr.instance.redActivated && !InputMngr.instance.blueActivated)
                || (numbOfGreen == numbOfRed && numbOfGreen > 0 && InputMngr.instance.redActivated && !InputMngr.instance.blueActivated)
                || (numbOfGreen == numbOfBlue && numbOfBlue > 0 && InputMngr.instance.blueActivated && !InputMngr.instance.redActivated))
                )
        {
            modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
            Collider[] targets = Physics.OverlapSphere(pos, res);
            foreach (Collider col in targets)
            {
                if (col.tag == "C")
                {
                    col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                    JaugeManager.instance.FillJauge(EntityScript.Type.Yellow);
                    //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                }
            }

            if ((numbOfBlue > numbOfRed || (numbOfBlue == numbOfRed && numbOfBlue > 0)) && InputMngr.instance.blueActivated)
            {
                modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
                Collider[] otherTargets = Physics.OverlapSphere(pos, res);

                foreach (Collider col in targets)
                {
                    if (col.tag == "B")
                    {
                        col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                        JaugeManager.instance.FillJauge(EntityScript.Type.Blue);
                        //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                    }
                }
            }

            else if ((numbOfRed > numbOfBlue || (numbOfRed == numbOfBlue && numbOfRed > 0)) && InputMngr.instance.redActivated)
            {
                modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
                Collider[] otherTargets = Physics.OverlapSphere(pos, res);

                foreach (Collider col in targets)
                {
                    if (col.tag == "A")
                    {
                        col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;

                        JaugeManager.instance.FillJauge(EntityScript.Type.Red);
                        //col.GetComponent<Rigidbody>().velocity = col.transform.position;
                    }
                }
            }
        }

        else if (numbOfRed == numbOfBlue && numbOfRed == numbOfGreen && numbOfRed > 0 && InputMngr.instance.redActivated && InputMngr.instance.blueActivated && InputMngr.instance.yellowActivated)
        {
            modifRessource += Mngr.instance.gainSizeSpeed * Time.deltaTime;
            Collider[] targets = Physics.OverlapSphere(pos, res);

            foreach (Collider col in targets)
            {
                if (col.tag == "A" || col.tag == "B" || col.tag == "C")
                {
                    col.GetComponent<MoveCube>().velocityAmount += Mngr.instance.speedBoostOnInput * Time.deltaTime;
                }


            }
        }
        //else if (!((numbOfRed == numbOfBlue && numbOfRed >= numbOfGreen) || (numbOfRed == numbOfGreen && numbOfRed >= numbOfBlue) || (numbOfBlue == numbOfGreen && numbOfBlue >= numbOfRed)))
		//	modifRessource -= lostOnFail * Time.deltaTime;

        /*if (Ressources < 2.5f)
            Ressources += modifRessource;
        else if (Ressources >= 2.5f)*/
        Ressources += modifRessource;

    }

	/*public void okRed(){
		inputRed = true;
	}
	public void nopRed(){
		inputRed = false;
	}
	public void okBlue(){
		inputBlue = true;
	}
	public void nopBlue(){
		inputBlue = false;
	}
	public void okGreen(){
		inputGreen = true;
	}
	public void nopGreen(){
		inputGreen = false;
	}*/
    
   /* public void InputRed()
    {
        signal = signaux.signalA;
        Influence();
    }

    public void InputBlue()
    {
        signal = signaux.signalB;
        Influence();
    }

    public void InputGreen()
    {
        signal = signaux.signalC;
        Influence();
    }*/


    public void Scoring()
    {
        if (Ressources > 0)
        {
            timer += Time.deltaTime * multiplicateur;
            score.text = "score = " + Mathf.RoundToInt(timer).ToString() + "(X " + multiplicateur.ToString()+")";
        }

        else if (Ressources <= 0)
        {
            score.text = "GAME OVER";
        }
    }

    public void Multi()
    {
        if (Ressources >= Startmulti)
        {
			gameObject.GetComponent<Renderer> ().material.color = Color.cyan;
            timerMulti += Time.deltaTime;
            if (timerMulti >= upMulti)
            {
                multiplicateur++;
                timerMulti = 0;
            }
        }

        else if (Ressources < Startmulti)
        {
			gameObject.GetComponent<Renderer> ().material.color = Color.black;
            multiplicateur = 1;
        }
    }

    #endregion Custom Functions
}
