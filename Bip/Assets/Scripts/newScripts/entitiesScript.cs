using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesScript : MonoBehaviour {

	public enum Type
	{
		Red,
		Blue,
		Yellow
	}
	public Type color;

	public float taille;
	public int positionInCircle;


	// Use this for initialization
	void Start () {
		ChangeType (color);
		transform.localScale = new Vector3 (taille, taille, taille);
	}




	// Update is called once per frame
	void Update () {
		ChangeType (color);
		transform.localScale = new Vector3 (taille, taille, taille);
	}


	public void ChangeType (Type newType)
	{
		color = newType;

		switch (color) {
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
