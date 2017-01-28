using UnityEngine;
using System.Collections;

public class A : MonoBehaviour {

    public float Ressources;
    float RessourcesGURU;
    //bool signalA;
    //bool signalB;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void BipA (Guru.signaux signal) {

        //RessourcesGURU = GameObject.Find("GURU").GetComponent<Guru>().Ressources;

        if (signal == Guru.signaux.signalA){
            Ressources -= 10;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources += 1;
        }
        if (signal == Guru.signaux.signalB)
        {
            Ressources += 10;
            GameObject.Find("GURU").GetComponent<Guru>().Ressources -= 1;
        }
    }
}
