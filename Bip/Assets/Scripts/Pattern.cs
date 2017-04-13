using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour {

    Vector3 direction;

    public Vector3 startPosition;

    public float speed;

    private void Start()
    {
        transform.position = startPosition;
        direction = Guru.instance.transform.position - transform.position;

    }


}
