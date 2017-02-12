using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {
    public Vector3 normal;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "A" || collision.gameObject.tag == "B" || collision.gameObject.tag == "C")
        {
            Debug.Log("Rebond");
            //collision.gameObject.GetComponent<Rigidbody>().velocity = -collision.gameObject.transform.position;
            //Vector3 dirOpposee = collision.contacts[0].normal;
            
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity, normal);
        }
    }


    /*private void OnCollisionStay (Collision collision)
    {
        if (collision.gameObject.tag == "A" || collision.gameObject.tag == "B" || collision.gameObject.tag == "C")
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)

                collision.gameObject.GetComponent<Rigidbody>().velocity = collision.contacts[0].normal;
        }
    }*/
}
