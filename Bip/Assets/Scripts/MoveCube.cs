using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour {

    public float speed = 5;

    public float range = 10;

    public enum Actions
    {
        Nothing,
        Flee,
        Chase
    }

    public Actions action;

    public GameObject target;

    private Rigidbody rb;

    private float timer = 0;

    public float timerMax = 1;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        GetTarget();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (action == Actions.Nothing || timer >= timerMax)
        {
            timer = 0;
            GetTarget();
        }


        if (action == Actions.Chase)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, target.transform.position, speed * Time.deltaTime));
        }

        else if (action == Actions.Flee)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, (transform.position - target.transform.position) * 1000, speed * Time.deltaTime));
        }

        rb.velocity = Vector3.zero;
		
	}

    void GetTarget()
    {
        Collider[] surroundings = Physics.OverlapSphere(transform.position, range);

        target = null;
        float targetDist = float.MaxValue;

        foreach (Collider col in surroundings)
        {
            if (col.gameObject != gameObject && Vector3.Distance(transform.position, col.transform.position) < targetDist && col.tag != tag)
            {
                targetDist = Vector3.Distance(transform.position, col.transform.position);
                target = col.gameObject;
            }
        }


        action = Actions.Nothing;

        if (target)
        {
            switch (target.tag)
            {
                case "Aa":
                    switch (tag)
                    {
                        case "Bb":
                            action = Actions.Flee;
                            break;

                        case "Cc":
                            action = Actions.Chase;
                            break;

                    }

                    break;

                case "Bb":
                    switch (tag)
                    {
                        case "Aa":
                            action = Actions.Chase;
                            break;

                        case "Cc":
                            action = Actions.Flee;
                            break;

                    }

                    break;

                case "Cc":
                    switch (tag)
                    {
                        case "Aa":
                            action = Actions.Flee;
                            break;

                        case "Bb":
                            action = Actions.Chase;
                            break;

                    }

                    break;

            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, range);
    }

}
