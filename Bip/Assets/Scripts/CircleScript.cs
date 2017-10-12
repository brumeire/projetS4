 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour {


	public EntityScript.Type color;
	public PatternCircle MovementType;

	public GameObject entity;


    /// /// /// /// /// /// /////////////////////////////////////////////////////////////////////////


    public int entityNb;

    //Pour la rotation

    [HideInInspector]
    public Vector3 center;

    public float rayonMax;
    public float rayonMin;
    public float breathSpeed; //vitesse de respiration des entités
    public float rotationSpeed; //vitesse de rotation des entités

    //Pour la barre

    public float espacement;
    public float angleDeg;





    void Awake()
    {
        center = transform.position;
    }

    void Update()
    {
        center = transform.position;
    }




    public void StartCircle (EntityScript.Type col, PatternCircle mov) {

        color = col;
        MovementType = mov;

		for (int i = 1; i < entityNb + 1; i++) {

            GameObject newEntity = Instantiate (entity);
            newEntity.AddComponent<CircleMove>();
			newEntity.GetComponent<EntityScript> ().positionInCircle = i;
			ChangeColor (color, newEntity);
			ChangeMovement (MovementType, newEntity);
            newEntity.transform.SetParent(transform);

		}
	}


	public void ChangeColor(EntityScript.Type newType, GameObject newEntity)
	{
		newEntity.GetComponent<EntityScript>().color = newType;
	}

	public void ChangeMovement (PatternCircle newType, GameObject newEntity)
    {
		newEntity.GetComponent<CircleMove> ().MovementType = newType;
	}


}
