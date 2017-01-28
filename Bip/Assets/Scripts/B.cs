using UnityEngine;
using System.Collections;

public class B : MonoBehaviour {

    public float Ressources;
    float RessourcesGURU;
    //bool signalB;
    //bool signalC;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        transform.GetChild(0).GetComponent<TextMesh>().text = "B \n" + Ressources.ToString();

    }
    public void BipB (Guru.signaux signal) {

        RessourcesGURU = GameObject.Find("GURU").GetComponent<Guru>().Ressources;

        if (signal == Guru.signaux.signalB && Ressources >= 1)
        {
            Ressources -= 1;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
        }
        if (signal == Guru.signaux.signalC)
        {
            Ressources += 1;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
        }

    }
}
