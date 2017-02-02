using UnityEngine;
using System.Collections;

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

    /*public bool signalA;
    public bool signalB;
    public bool signalC;*/

    // Use this for initialization
    void Start () {
        /*signalA = false;
        signalB = false;
        signalC = false;*/
    }

    // Update is called once per frame
    void Update () {
        Ressources -= Time.deltaTime * lostPerTime;

        transform.localScale = new Vector3(Ressources * 2, Ressources * 2, 0.1f);

        if (Ressources <= 0)
            Destroy(gameObject);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            signal = signaux.signalA;
            Influence();
        }
       
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            signal = signaux.signalB;
            Influence();
        }
        

        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            signal = signaux.signalC;
            Influence();
        }


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

        if (numbOfRed > numbOfBlue && numbOfRed > numbOfGreen && signal == signaux.signalA)
            Ressources += 0.3f;

        else if (numbOfBlue > numbOfRed && numbOfBlue > numbOfGreen && signal == signaux.signalB)
            Ressources += 0.3f;

        else if (numbOfGreen > numbOfRed && numbOfGreen > numbOfBlue)
            Ressources += 0.3f;

        else if (!(numbOfRed == numbOfBlue || numbOfRed == numbOfGreen || numbOfBlue == numbOfGreen))
            Ressources -= 0.3f;



    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Ressources);
    }

}
