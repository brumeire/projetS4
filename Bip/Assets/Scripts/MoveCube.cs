using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{

	public float[] paliay;
	public int indexPaliay = 0;
	public float reductionPaliay;
	public float timer;

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

		timer += Time.deltaTime;
		if (timer >= reductionPaliay) {
			timer = 0;
			if (indexPaliay > 0) {
				indexPaliay--;
			}
		}
		if (indexPaliay <= 0) {
			indexPaliay = 0;
		}
		constantForce.relativeForce = new Vector3 (0,paliay[indexPaliay], 0);


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
