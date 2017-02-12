using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "A" || collision.gameObject.tag == "B" || collision.gameObject.tag == "C")
        {
            Debug.Log("Rebond");
            collision.gameObject.GetComponent<Rigidbody>().velocity = -collision.gameObject.transform.position;
        }
    }
}
