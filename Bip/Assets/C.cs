using UnityEngine;
using System.Collections;

public class C : MonoBehaviour {

    public int Ressources;
    float RessourcesGURU;
    //bool signalA;
    //bool signalC;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void BipC(Guru.signaux signal)
    {

        RessourcesGURU = GameObject.Find("GURU").GetComponent<Guru>().Ressources;

        if (signal == Guru.signaux.signalC)
        {
            Ressources -= 10;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
        }
        if (signal == Guru.signaux.signalA)
        {
            Ressources += 10;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
        }

    }
}
