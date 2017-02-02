using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour {

    public enum Type
    {
        A,
        B,
        C,
        D
    }

    public Type type;

    public Material[] entityMaterials;

    public Sprite[] arrows;

    public float timerChange = 2.5f;

    private float timer = 0;

	// Use this for initialization
	void Start () {

        int rand = Random.Range(0, 4);

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

            case 3:
                type = Type.D;
                break;
        }

        ChangeType(type);
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += Vector3.up;
        }

        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;
        }


        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
        }

        else if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
        }


    }


    public void ChangeType (Type newType)
    {
        type = newType;

        switch (type)
        {
            case Type.A:
                GetComponent<Renderer>().material = entityMaterials[0];
                tag = "A";
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = arrows[0];
                break;

            case Type.B:
                GetComponent<Renderer>().material = entityMaterials[1];
                tag = "B";
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = arrows[1];
                break;

            case Type.C:
                GetComponent<Renderer>().material = entityMaterials[2];
                tag = "C";
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = arrows[2];
                break;

            case Type.D:
                GetComponent<Renderer>().material = entityMaterials[3];
                tag = "D";
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = arrows[3];
                break;
        }
        

    }

    public void ReceiveSignal(Guru.signaux signal)
    {

        switch (tag)
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

                else if (signal == Guru.signaux.signalA)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 0.3f;
                }

                break;



            case "C":

                if (signal == Guru.signaux.signalC)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 0.3f;
                }

                else if (signal == Guru.signaux.signalD)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 0.3f;
                }

                break;



            case "D":

                if (signal == Guru.signaux.signalD)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 0.3f;
                }

                else if (signal == Guru.signaux.signalC)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 0.3f;
                }

                break;

        }


    }

}
