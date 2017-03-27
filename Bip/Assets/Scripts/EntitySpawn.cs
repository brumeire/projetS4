using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawn : MonoBehaviour {

    public GameObject entityPrefab;
    public static float taille;
    public float timer;
    public int currentPalier = 0;
    bool active = false;

    public List<Palier> paliers = new List<Palier>();

	// Use this for initialization
	void Start () {
        ChangePalier();

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= paliers[currentPalier].timeBeforeNext)
        {
            timer -= paliers[currentPalier].timeBeforeNext;

            if (paliers.Count > currentPalier + 1)
                currentPalier++;

            MoveCube.minSpeed += paliers[currentPalier].speedChange;
            MoveCube.maxSpeed += paliers[currentPalier].speedChange;

            ChangePalier();
        }

        


    }


    void ChangePalier()
    {
        taille = paliers[currentPalier].globalSize;
        foreach (EntityToCreate entity in paliers[currentPalier].entitiesToSpawn)
        {
            if (entity.typeChosenToEquilibrate)
            {
                entity.type = FindFewerEntities();
            }
            SpawnEntity(GetFreePos(taille), entity);
        }
    }

    public void SpawnEntity(Vector3 pos, EntityToCreate entity)
    {
        GameObject newEntity = Instantiate(entityPrefab);
        newEntity.GetComponent<EntityScript>().type = entity.type;
        newEntity.GetComponent<EntityScript>().ChangeType(entity.type);
        newEntity.GetComponent<MoveCube>().speedAdditionalCoefficient += entity.additionalSpeed;

        newEntity.transform.position = pos;

        SpawnerScript.entitiesAlive.Add(newEntity);

    }


    Vector3 GetFreePos(float size)
    {
        Vector3 newPos = Vector3.zero;

        for (int i = 0; i < 200; i++)
        {
            newPos = new Vector3(
                Random.Range(-13.5f, 13.5f),
                Random.Range(-10.5f, 10.5f),
                0
                );

            int layerMask = 1 << 9;

            Collider[] surroundings = Physics.OverlapSphere(newPos, size, layerMask);
            if (surroundings.Length == 0)
            {
                return newPos;
            }
        }

        return Vector3.zero;


    }


    public EntityScript.Type FindFewerEntities()
    {
        float reds = 0;
        float blues = 0;
        float yellows = 0;

        foreach (GameObject entity in SpawnerScript.entitiesAlive)
        {
            switch (entity.tag)
            {

                case "A":
                    reds++;
                    break;

                case "B":
                    blues++;
                    break;

                case "C":
                    yellows++;
                    break;

            }

        }
        

       if (blues == 0)
            return EntityScript.Type.Blue;

        else if (yellows == 0)
            return EntityScript.Type.Yellow;

       else if (reds == 0)
            return EntityScript.Type.Red;


        if (reds / SpawnerScript.entitiesAlive.Count < 1f / 3f)
            return EntityScript.Type.Red;

        else if (blues / SpawnerScript.entitiesAlive.Count < 1f / 3f)
            return EntityScript.Type.Blue;

        else if (yellows / SpawnerScript.entitiesAlive.Count < 1f / 3f)
            return EntityScript.Type.Yellow;

        float rand = Random.value;

        if (rand < 1f/3f)
            return EntityScript.Type.Red;

        else if (rand > 2f/3f)
            return EntityScript.Type.Yellow;

        else
            return EntityScript.Type.Blue;
    }
}
