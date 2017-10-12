using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BckgrndScript : MonoBehaviour {

    Renderer rend;
    public float sizeMax = 0.193f;
    public float sizeMin = -0.0425f;

    void Start ()
    {
        rend = GetComponent<Renderer>();
    }

	// Update is called once per frame
	void Update ()
    {
        rend.material.SetFloat("_Offset", Mathf.Lerp(sizeMin, sizeMax, Guru.instance.Ressources / Guru.instance.RessourcesMax));
        //Debug.Log(Mathf.Lerp(0, 0.25f, Guru.instance.Ressources / Guru.instance.RessourcesMax));
	}
}
