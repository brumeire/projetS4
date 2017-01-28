using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Compteur : MonoBehaviour {

    float RessourcesGURU;
    float RessourcesA;
    float RessourcesB;
    float RessourcesC;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RessourcesGURU = GameObject.Find("GURU").GetComponent<Guru>().Ressources;
        RessourcesA = GameObject.Find("A").GetComponent<A>().Ressources;
        RessourcesB = GameObject.Find("B").GetComponent<B>().Ressources;
        RessourcesC = GameObject.Find("C").GetComponent<C>().Ressources;
        Text compte = GetComponent<Text>();
        compte.text = "RessourcesGURU" + Mathf.FloorToInt(RessourcesGURU).ToString() + "         RessourcesA" + RessourcesA.ToString() + "         RessourcesB" + RessourcesB.ToString() + "         RessourcesC" + RessourcesC.ToString();
    }
}
