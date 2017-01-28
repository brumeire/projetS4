using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Compteur : MonoBehaviour {

    float RessourcesGURU;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RessourcesGURU = GameObject.Find("GURU").GetComponent<Guru>().Ressources;
        
        Text compte = GetComponent<Text>();
        compte.text = "RessourcesGURU" + Mathf.FloorToInt(RessourcesGURU).ToString();
    }
}
