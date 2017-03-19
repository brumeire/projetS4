using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour {


    public float Taille;

    public enum Type
    {
        A,
        B,
        C
    }

    public Type type;

    public Sprite[] entitySprites;

    public float timerChange = 2.5f;

    private float timer = 0;

	// Use this for initialization
	void Start () {


      //  transform.localScale = transform.localScale * Taille;


        ChangeType(type);
		
	}
	
	// Update is called once per frame
	void Update () {
        Taille = EntitySpawn.taille;
        transform.localScale =  new Vector3(Taille,Taille,Taille);

    }


    public void ChangeType (Type newType)
    {
        type = newType;

        switch (type)
        {
            case Type.A:
                GetComponent<SpriteRenderer>().sprite = entitySprites[0];
                tag = "A";
                break;

            case Type.B:
                GetComponent<SpriteRenderer>().sprite = entitySprites[1];
                tag = "B";
                break;

            case Type.C:
                GetComponent<SpriteRenderer>().sprite = entitySprites[2];
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
