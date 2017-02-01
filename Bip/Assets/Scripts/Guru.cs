using UnityEngine;
using System.Collections;

public class Guru : MonoBehaviour {

    public enum signaux {
        signalA,
        signalB,
        signalC
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            signal = signaux.signalA;
            Influence();
        }
       
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            signal = signaux.signalB;
            Influence();
        }
        

        else if (Input.GetKeyDown(KeyCode.E))
        {
            signal = signaux.signalC;
            Influence();
        }



    }

    void Influence ()
    {
        Vector3 pos = transform.position;
        float res = Ressources;

        Collider[] hitColliders = Physics.OverlapSphere(pos,res*0.2f);

        foreach (Collider col in hitColliders)
        {
            col.GetComponent<EntityScript>().ReceiveSignal(signal);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Ressources*0.2f);
    }

}
