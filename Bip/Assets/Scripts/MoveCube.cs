using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    //private ConstantForce constantForce;
    public static float minSpeed = 3;
    public static float maxSpeed = 7.5f;
    public float speedAdditionalCoefficient = 0;
    public float speedReductionPerSecond = 0.2f;
    //public Vector3 velocityDebug;

    [HideInInspector]
    public float velocityAmount = 100;
    // Use this for initialization
    void Start()
    {
        //constantForce = GetComponent<ConstantForce>();
        //GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * 0.1f);
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * (minSpeed + speedAdditionalCoefficient);
        //transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        velocityAmount = minSpeed + speedAdditionalCoefficient;
    }

    // Update is called once per frame
    void Update()
    {
        //velocityDebug = GetComponent<Rigidbody>().velocity;


        if (velocityAmount > minSpeed + speedAdditionalCoefficient)
            velocityAmount -= speedReductionPerSecond * Time.deltaTime;

        if (velocityAmount < minSpeed + speedAdditionalCoefficient)
            velocityAmount = minSpeed + speedAdditionalCoefficient;

        else if (velocityAmount > maxSpeed + speedAdditionalCoefficient)
            velocityAmount = maxSpeed + speedAdditionalCoefficient;


        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * velocityAmount;
    }

}
