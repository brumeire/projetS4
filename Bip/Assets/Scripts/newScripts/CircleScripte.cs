using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScripte : MonoBehaviour {


	public EntityScript.Type COULOUR;
	public EntitiesMove.Type MovementType;

	public GameObject entity;

	GameObject newEntity;



	// Use this for initialization
	void Start () {

		for (int i = 1; i < CircleMng.instance.entityNb + 1; i++) {

			newEntity = Instantiate (entity);
			newEntity.GetComponent<EntityScript> ().positionInCircle = i;
			ChangeCOULOUR (COULOUR);
			ChangeMovement (MovementType);
            newEntity.transform.SetParent(transform);

		}
	}
	
	// Update is called once per frame
	void Update () {
		CircleMng.instance.center = transform.position;


	}
	public void ChangeCOULOUR (EntityScript.Type newType)
	{
		newEntity.GetComponent<EntityScript>().COULOUR = newType;
	}

	public void ChangeMovement (EntitiesMove.Type newType){
		newEntity.GetComponent<EntitiesMove> ().MovementType = newType;
	}


}
