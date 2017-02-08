using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationTaille : MonoBehaviour {

	public float tailleMin;
	public float tailleMax;
	float TAILLE;


	// Use this for initialization
	void Start () {
		TAILLE = Random.Range (tailleMin, tailleMax);
		transform.localScale = new Vector3 (TAILLE, TAILLE, TAILLE);
	}
}