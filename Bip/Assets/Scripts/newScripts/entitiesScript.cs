using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entitiesScript : MonoBehaviour {

	public enum Type
	{
		Red,
		Blue,
		Yellow
	}
	public Type COULOUR;

	public float taille;
	public int positionInCircle;


	// Use this for initialization
	void Start () {
		ChangeType (COULOUR);
		transform.localScale = new Vector3 (taille, taille, taille);
	}




	// Update is called once per frame
	void Update () {
		ChangeType (COULOUR);
		transform.localScale = new Vector3 (taille, taille, taille);
	}


	public void ChangeType (Type newType)
	{
		COULOUR = newType;

		switch (COULOUR) {
		case Type.Red:
			GetComponent<Renderer> ().material.color = Color.red;
			break;

		case Type.Blue:
			GetComponent<Renderer> ().material.color = Color.blue;
			break;

		case Type.Yellow:
			GetComponent<Renderer> ().material.color = Color.yellow;
			break;

		}
	}
}
