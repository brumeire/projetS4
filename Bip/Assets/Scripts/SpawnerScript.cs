using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public GameObject[] prefabs;


    public float timerSpawn = 2;

    private float timer = 0;

    public int numberToSpawn = 1;


    public static List<GameObject> entitiesAlive = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= timerSpawn)
        {
            timer -= timerSpawn;

            for (int i = 0; i < numberToSpawn; i++)
            {
                int rand = Random.Range(0, prefabs.Length);

                for (int j = 0; j < 200; j++)
                {

                    Vector3 newPos = new Vector3(Random.Range(Camera.main.ScreenToWorldPoint(Vector3.zero).x, Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)).x), transform.position.y);

                    if (Physics.OverlapSphere(newPos, prefabs[rand].GetComponent<SphereCollider>().radius).Length == 0)
                    {
                        GameObject newEntity = Instantiate(prefabs[rand]);

                        newEntity.transform.position = newPos;

                        break;
                    }

                }

                

            }

        }
		
	}
}
