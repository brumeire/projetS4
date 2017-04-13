using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMng : MonoBehaviour {

	public static CircleMng instance;

	public int entityNb;

	//Pour la rotation

	public Vector3 center;
	public float rayonMax;
	public float rayonMin;
	public float breathSpeed; //vitesse de respiration des entités
	public float rotationSpeed; //vitesse de rotation des entités

	//Pour la barre

	public float espacement;
	public float angleDeg;





	void Awake (){
		instance = this;
        center = transform.position;
	}
}
