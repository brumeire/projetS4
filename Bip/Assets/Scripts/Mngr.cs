using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mngr : MonoBehaviour {

    public static Mngr instance;


    public float tailleAgents = 0.5f;

    public float speedBoostOnInput = 3;

    public float gainSizeSpeed = 2;

    public float lossSizeSpeed = 0.5f;

    public bool avatarMinSizeEnabled;

    public float avatarMinSizeTimeBeforeDestroy = 3;

    public float avatarMinSize = 4;

    public GameObject countDown;



    public int reds = 0;

    public int blues = 0;

    public int yellows = 0;


    // Use this for initialization
    void Awake () {
        instance = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
