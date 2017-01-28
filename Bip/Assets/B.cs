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
	public void BipB (Guru.signaux signal) {

        RessourcesGURU = GameObject.Find("GURU").GetComponent<Guru>().Ressources;

        if (signal == Guru.signaux.signalB)
        {
            Ressources -= 10;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
        }
        if (signal == Guru.signaux.signalC)
        {
            Ressources += 10;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
        }

    }
}
