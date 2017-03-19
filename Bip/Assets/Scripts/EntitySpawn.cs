using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawn : MonoBehaviour {

    public GameObject Rouge;
    public GameObject Vert;
    public GameObject Bleue;
    public static float taille;
    public float timer;
    public int palier = 0;
    bool active = false;

	// Use this for initialization
	void Start () {
        Instantiate(Rouge);
        Instantiate(Vert);
        taille = 0.5f;

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer>= 20f)
        {
            palier++;
            active = true;
            timer = 0;
        }
        if (palier == 1){
            if (active == true){ 
            event1();
        }
        }
        if (palier == 2)
        {
            if (active == true)
            {
                event2();
            }
        }
        if (palier == 3)
        {
            if (active == true)
            {
                event3();
            }
        }
        if (palier == 4)
        {
            if (active == true)
            {
                event4();
            }
        }
        if (palier == 5)
        {
            if (active == true)
            {
                event5();
            }
        }


    }

    void event1()
    {
        taille = 0.4f;
        Instantiate(Rouge);
        active = false;
        MoveCube.minSpeed *= 1.5f;
        MoveCube.maxSpeed *= 1.5f;
    }
    void event2()
    {
        taille = 0.3f;
        Instantiate(Vert);
        active = false;
        MoveCube.minSpeed *= 1.5f;
        MoveCube.maxSpeed *= 1.5f;
    }
    void event3()
    {
        taille = 0.2f;
        Instantiate(Bleue);
        Instantiate(Bleue);
        active = false;
        MoveCube.minSpeed *= 1.5f;
        MoveCube.maxSpeed *= 1.5f;
    }
    void event4()
    {
        taille = 0.1f;
        Instantiate(Rouge);
        Instantiate(Vert);
        active = false;
        MoveCube.minSpeed *= 1.5f;
        MoveCube.maxSpeed *= 1.5f;
    }
    void event5()
    {
        taille = 0.05f;
        Instantiate(Bleue);
        active = false;
        MoveCube.minSpeed *= 1.5f;
        MoveCube.maxSpeed *= 1.5f;
    }

}
