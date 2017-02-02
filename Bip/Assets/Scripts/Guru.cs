using UnityEngine;
using System.Collections;

public class Guru : MonoBehaviour {

    public enum signaux {
        signalA,
        signalB,
        signalC,
        signalD
    }
    public signaux signal;

    public float Ressources;
    public float lostPerTime;

    /*public bool signalA;
    public bool signalB;
    public bool signalC;*/

    // Use this for initialization
    void Start () {
        /*signalA = false;
        signalB = false;
        signalC = false;*/
    }

    // Update is called once per frame
    void Update () {
        Ressources -= Time.deltaTime * lostPerTime;

        transform.localScale = new Vector3(Ressources * 2, Ressources * 2, 0.1f);

        if (Ressources <= 0)
            Destroy(gameObject);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            signal = signaux.signalA;
            Influence();
        }
       
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            signal = signaux.signalB;
            Influence();
        }
        

        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            signal = signaux.signalC;
            Influence();
        }

        else if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            signal = signaux.signalD;
            Influence();
        }



    }

    void Influence ()
    {
        Vector3 pos = transform.position;
        float res = Ressources;

        Collider[] hitColliders = Physics.OverlapSphere(pos,res);

        foreach (Collider col in hitColliders)
        {
            col.GetComponent<EntityScript>().ReceiveSignal(signal);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Ressources);
    }

}
