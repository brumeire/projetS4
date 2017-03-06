using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Guru : MonoBehaviour {

    public enum signaux {
        signalA,
        signalB,
        signalC,
        signalD
    }
    public signaux signal;

    public float Ressources;
    public float lostPerTime;
    public float lostOnFail = 0.3f;
    /*public bool signalA;
    public bool signalB;
    public bool signalC;*/

	public float timer;
	public Text score;
	public float RessourcesMax = 5f;



    public Material[] materials;

    // Use this for initialization
    void Start () {
        /*signalA = false;
        signalB = false;
        signalC = false;*/
    }

    // Update is called once per frame
    void Update () {
		scoring ();
        /*if (Ressources < 2.5f)
        {
            Ressources -= Time.deltaTime * lostPerTime;
        }

        else if (Ressources >= 2.5f)
        {*/
        Ressources -= Time.deltaTime * lostPerTime;
       // }


        transform.localScale = new Vector3(Ressources * 2, Ressources * 2, 0.1f);

        /*if (Ressources >= 5 || Ressources <= 0)
            Destroy(gameObject);*/
		if (Ressources <= 0) {
			Ressources = 0;
		}

		if (Ressources >= RessourcesMax) {
			Ressources = RessourcesMax;
		}


        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
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
        }



        /*if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Renderer>().material = materials[1];
        }

        else if (Input.GetKey(KeyCode.Z))
        {
            GetComponent<Renderer>().material = materials[2];
        }

        else if (Input.GetKey(KeyCode.E))
        {
            GetComponent<Renderer>().material = materials[3];
        }
        else
        {
            GetComponent<Renderer>().material = materials[0];
        }*/


    }

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

        if (numbOfRed > numbOfBlue && numbOfRed > numbOfGreen && signal == signaux.signalA)
        {
            modifRessource += 0.3f;
            Collider[] targets = Physics.OverlapSphere(pos, res);

            foreach (Collider col in targets)
            {
                if (col.tag == "A")
                {
                    col.GetComponent<MoveCube>().velocityAmount += 3;
                    col.GetComponent<Rigidbody>().velocity = col.transform.position;
                }
            }
        }
        else if (numbOfBlue > numbOfRed && numbOfBlue > numbOfGreen && signal == signaux.signalB)
        {
            modifRessource += 0.3f;
            Collider[] targets = Physics.OverlapSphere(pos, res);

            foreach (Collider col in targets)
            {
                if (col.tag == "B")
                {
                    col.GetComponent<MoveCube>().velocityAmount += 3;
                    col.GetComponent<Rigidbody>().velocity = col.transform.position;
                }
            }
        }

        else if (numbOfGreen > numbOfRed && numbOfGreen > numbOfBlue && signal == signaux.signalC)
        {
            modifRessource += 0.3f;
            Collider[] targets = Physics.OverlapSphere(pos, res);

            foreach (Collider col in targets)
            {
                if (col.tag == "C")
                {
                    col.GetComponent<MoveCube>().velocityAmount += 3;
                    col.GetComponent<Rigidbody>().velocity = col.transform.position;
                }
            }
        }

        else if (!((numbOfRed == numbOfBlue && numbOfRed >= numbOfGreen) || (numbOfRed == numbOfGreen && numbOfRed >= numbOfBlue) || (numbOfBlue == numbOfGreen && numbOfBlue >= numbOfRed)))
            modifRessource -= lostOnFail;

        /*if (Ressources < 2.5f)
            Ressources += modifRessource;
        else if (Ressources >= 2.5f)*/
        Ressources += modifRessource;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Ressources);
    }

    public void InputRed()
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
    }

	public void scoring(){
		if (Ressources > 0f) {
			timer += Time.deltaTime;
			score.text = "score  =  " + Mathf.RoundToInt (timer).ToString ();
		} else {
			score.text = "GAME OVER";
		}
	}
}
