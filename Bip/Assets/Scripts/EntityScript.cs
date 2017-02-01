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

        timer += Time.deltaTime;

        if (timer >= timerChange)
        {
            timer -= timerChange;

            Collider[] surroundings = Physics.OverlapBox(transform.position, new Vector3(0.2f, 0.2f, 0.2f));

            int blueArround = 0;
            int greenArround = 0;
            int redArround = 0;

            foreach (Collider col in surroundings)
            {
                if (col.gameObject != gameObject)
                {
                    switch (col.tag)
                    {
                        case "A":
                            redArround++;
                            break;

                        case "B":
                            blueArround++;
                            break;

                        case "C":
                            greenArround++;
                            break;
                    }
                }
            }

            Type newType = type;

            if (redArround > blueArround && redArround > greenArround)
                newType = Type.A;

            else if (blueArround > redArround && blueArround > greenArround)
                newType = Type.B;

            else if (greenArround > redArround && greenArround > blueArround)
                newType = Type.C;


            ChangeType(newType);

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

        switch (tag)
        {
            case "A":

                if (signal == Guru.signaux.signalA)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
                }

                if (signal == Guru.signaux.signalB)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
                }

                break;



            case "B":

                if (signal == Guru.signaux.signalB)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
                }

                if (signal == Guru.signaux.signalC)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
                }

                break;



            case "C":

                if (signal == Guru.signaux.signalC)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
                }
                if (signal == Guru.signaux.signalA)
                {
                    GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
                }

                break;

        }


    }

}
