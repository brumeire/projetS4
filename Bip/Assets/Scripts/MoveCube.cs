using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * 0.1f);
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dirOpposee = collision.contacts[0].normal;
        Vector3 dirInc = transform.up;

        transform.up = -dirInc;
        transform.eulerAngles += new Vector3(0, 0, Vector3.Angle(-dirInc, dirOpposee) * 2);
    }

}
