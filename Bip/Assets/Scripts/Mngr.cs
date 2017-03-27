using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mngr : MonoBehaviour {

    public static Mngr instance;


    public float speedBoostOnInput = 3;

    public float gainSizeSpeed = 2;

    public bool avatarMinSizeEnabled;

    public float avatarMinSizeTimeBeforeDestroy = 3;

    public float avatarMinSize = 4;

    public GameObject countDown;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
