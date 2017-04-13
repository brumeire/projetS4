using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScriptV2 : MonoBehaviour {

	public Color baseColor;


	public EntityScript.Type COULOUR;

	public Color[] entityColors;
	public float taille;
	public int positionInCircle;

	void Start(){
		ChangeType(COULOUR);

	}

	void Update(){
		taille = Mngr.instance.tailleAgents;
		transform.localScale =  new Vector3(taille, taille, taille);

	}

	public void ChangeType (EntityScript.Type newType)
	{
		COULOUR = newType;

		switch (COULOUR)
		{
		case EntityScript.Type.Red:
			baseColor = entityColors[0];
			GetComponent<SpriteRenderer>().color = baseColor;
			tag = "A";
			break;

		case EntityScript.Type.Blue:
			baseColor = entityColors[1];
			tag = "B";
			break;

		case EntityScript.Type.Yellow:
			baseColor = entityColors[2];
			tag = "C";
			break;

		}

		GetComponent<SpriteRenderer>().color = baseColor;

	}
	private void OnDestroy()
	{
		EntitySpawn.entitiesAlive.Remove(gameObject);

		switch (COULOUR)
		{
		case EntityScript.Type.Red:
			Mngr.instance.reds--;
			break;

		case EntityScript.Type.Blue:
			Mngr.instance.blues--;
			break;

		case EntityScript.Type.Yellow:
			Mngr.instance.yellows--;
			break;

		}
	}

}


