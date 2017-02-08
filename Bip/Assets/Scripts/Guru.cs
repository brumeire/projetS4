﻿using UnityEngine;
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

    public Material[] materials;

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

        if (numbOfRed > numbOfBlue && numbOfRed > numbOfGreen && signal == signaux.signalA)
        {
            Ressources += 0.3f;
            GameObject[] targets = GameObject.FindGameObjectsWithTag("A");

            foreach(GameObject go in targets)
            {
                go.GetComponent<ConstantForce>().relativeForce += new Vector3(0, 30, 0);
            }
        }
        else if (numbOfBlue > numbOfRed && numbOfBlue > numbOfGreen && signal == signaux.signalB)
        {
            Ressources += 0.3f;
            GameObject[] targets = GameObject.FindGameObjectsWithTag("B");

            foreach (GameObject go in targets)
            {
                go.GetComponent<ConstantForce>().relativeForce += new Vector3(0, 30, 0);
            }
        }

        else if (numbOfGreen > numbOfRed && numbOfGreen > numbOfBlue && signal == signaux.signalC)
        {
            Ressources += 0.3f;
            GameObject[] targets = GameObject.FindGameObjectsWithTag("C");

            foreach (GameObject go in targets)
            {
                if (Vector3.Distance(go.transform.position, transform.position) <= res/2)
                    go.GetComponent<ConstantForce>().relativeForce += new Vector3(0, 120, 0);
            }
        }

        else if (!(numbOfRed == numbOfBlue || numbOfRed == numbOfGreen || numbOfBlue == numbOfGreen))
            Ressources -= 0.6f;



    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Ressources);
    }

}
