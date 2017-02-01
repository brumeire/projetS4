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
    void Update()
    {

        transform.GetChild(0).GetComponent<TextMesh>().text = "C \n" + Ressources.ToString();

    }
public void BipC(Guru.signaux signal)
    {
        
        RessourcesGURU = GameObject.Find("GURU").GetComponent<Guru>().Ressources;

        if (signal == Guru.signaux.signalC && Ressources >= 1)
        {
            //Ressources -= 1;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
        }
        if (signal == Guru.signaux.signalA)
        {
            //Ressources += 1;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
        }

    }
}
