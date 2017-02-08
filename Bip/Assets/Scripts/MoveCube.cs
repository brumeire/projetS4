using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 100;
    ConstantForce constantForce;
    // Use this for initialization
    void Start()
    {
        constantForce = GetComponent<ConstantForce>();
        //GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * 0.1f);
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
    }

    // Update is called once per frame
    void Update()
    {

        if (constantForce.relativeForce.y > speed)
            constantForce.relativeForce -= new Vector3(0, 0.2f * Time.deltaTime, 0);

        if (constantForce.relativeForce.y < speed)
            constantForce.relativeForce = new Vector3(0, speed, 0);

        else if (constantForce.relativeForce.y > 500)
            constantForce.relativeForce = new Vector3(0, 500, 0);


        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Wall")
        {
            Vector3 dirOpposee = collision.contacts[0].normal;
            Vector3 dirInc = transform.up;

            transform.up = -dirInc;
            transform.eulerAngles += new Vector3(0, 0, Vector3.Angle(-dirInc, dirOpposee) * 2);
        }
        else
        {
            transform.up = -transform.position;
        }
    }

}
