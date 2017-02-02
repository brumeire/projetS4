using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour {

    public enum Type
    {
        A,
        B,
        C
    }

    public Type type;

    public Material[] entityMaterials;

    public float timerChange = 2.5f;

    private float timer = 0;

	// Use this for initialization
	void Start () {

        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                type = Type.A;
                break;

            case 1:
                type = Type.B;
                break;

            case 2:
                type = Type.C;
                break;

        }

        ChangeType(type);
		
	}
	
	// Update is called once per frame
	void Update () {

    }


    public void ChangeType (Type newType)
    {
        type = newType;

        switch (type)
        {
            case Type.A:
                GetComponent<Renderer>().material = entityMaterials[0];
                tag = "A";
                break;

            case Type.B:
                GetComponent<Renderer>().material = entityMaterials[1];
                tag = "B";
                break;

            case Type.C:
                GetComponent<Renderer>().material = entityMaterials[2];
                tag = "C";
                break;

        }
        

    }

    public void ReceiveSignal(Guru.signaux signal)
    {

        /*switch (tag)
        {
            case "A":

                if (signal == Guru.signaux.signalA)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 0.3f;
                }

                else if (signal == Guru.signaux.signalB)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 0.3f;
                }

                break;



            case "B":

                if (signal == Guru.signaux.signalB)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 0.3f;
                }

                else if (signal == Guru.signaux.signalC)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 0.3f;
                }

                break;



            case "C":

                if (signal == Guru.signaux.signalC)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 0.3f;
                }

                else if (signal == Guru.signaux.signalA)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 0.3f;
                }

                break;

        }*/


    }

}
