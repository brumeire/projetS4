using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesMove : MonoBehaviour {

	public enum Type
	{
		rotation,
		barre,
	}

	public Type MovementType;
	public float rayonInTime;
	public float angleRad;
	public float angleDegC;
	public float x;
	public float y;
	public bool move = true;
	bool placement;

	void Start () {
		placement = true;
	}

	
	// Update is called once per frame
	void Update () {
		ChangeType (MovementType);

	}


	public void ChangeType (Type newType)
	{
		MovementType = newType;


		switch (MovementType) {

		case Type.rotation:

			if (move) {
				rayonInTime -= Time.deltaTime * CircleMng.instance.breathSpeed;
				if (rayonInTime <= CircleMng.instance.rayonMin) {
					move = false;
				}
			}
			if (!move) {
				rayonInTime += Time.deltaTime * CircleMng.instance.breathSpeed;
				if (rayonInTime >= CircleMng.instance.rayonMax) {
					move = true;
				}
			}
			if (placement) {
				angleDegC = (360 / CircleMng.instance.entityNb) * GetComponent<EntityScript> ().positionInCircle;
				rayonInTime = CircleMng.instance.rayonMax;
				placement = false;
			}
			angleDegC += Time.deltaTime * CircleMng.instance.rotationSpeed;

			angleRad = angleDegC * Mathf.Deg2Rad;
			x = Mathf.Cos (angleRad) * rayonInTime;
			y = Mathf.Sin (angleRad) * rayonInTime;
			transform.position = CircleMng.instance.center + new Vector3 (x, y, 0);
			break;





		case Type.barre:
			CircleMng.instance.angleDeg += Time.deltaTime * CircleMng.instance.rotationSpeed;
			angleRad = CircleMng.instance.angleDeg * Mathf.Deg2Rad;
			x = Mathf.Cos (angleRad) * CircleMng.instance.espacement * (GetComponent<EntityScript> ().positionInCircle-1);
			y = Mathf.Sin (angleRad) * CircleMng.instance.espacement * (GetComponent<EntityScript> ().positionInCircle-1);

			transform.position = CircleMng.instance.center + new Vector3 (x, y, 0);

			break;
		






		}
	}


}
