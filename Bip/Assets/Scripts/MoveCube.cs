using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour {

    public float speed = 5;


    private Rigidbody rb;

  
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

       
		
	}

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, range);
    }

}
