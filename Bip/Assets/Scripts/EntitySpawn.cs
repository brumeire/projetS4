using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EntitySpawn : MonoBehaviour {

    public GameObject entityPrefab;
    public float timer = 0;

	public GameObject AnnoncePalier;
	public GameObject textPalier;

    public static bool gameStarted = false;

    public GameObject playButton;

    [HideInInspector]
    public float timerMax;
    [HideInInspector]
    public int currentPalier = -1;

    public List<Palier> paliers = new List<Palier>();
	public static List<GameObject> entitiesAlive = new List<GameObject>();

    private int currentIteration = -1;

    private List<Iteration> remainingIterations = new List<Iteration>();


    public static EntitySpawn instance;
    

	// Use this for initialization
	void Start ()
    {
        instance = this;

        gameStarted = false;


    }
	
	// Update is called once per frame
	void Update () {

        if (gameStarted)
        {

            timer += Time.deltaTime;

            if (timer >= timerMax)
            {
                timer -= timerMax;

                if (remainingIterations.Count > 0)
                {
                    currentIteration++;
                    LoadNextIteration();
                }

                else if (currentIteration < paliers[currentPalier].IterationsBeforeNextPalier)
                {
                    ReloadCurrentPalier();
                }

                else
                    ChangePalier();
            }

        } 
        
        else
        {
            if (Guru.instance.Ressources < Guru.instance.RessourcesMax)
            {
                Guru.instance.Ressources += 2 * Time.deltaTime;
            }
        }
           
    }


    public void StartGame()
    {
        timer = 0;
        ChangePalier();
        gameStarted = true;
        playButton.SetActive(false);
    }

    void ChangePalier()
    {
        if (currentPalier < paliers.Count - 1)
            currentPalier++;
		if (currentPalier >= 1) {
			textPalier.GetComponent<Text> ().text = "Palier n°" + currentPalier.ToString () + "\tIteration n°" + currentIteration.ToString();
			AnnoncePalier.GetComponent<Animator> ().SetTrigger ("NewPalier");
		}
        //Debug.Log("Changing palier to " + currentPalier);

        currentIteration = 1;

        remainingIterations = new List<Iteration>();

        foreach (Iteration newIteration in paliers[currentPalier].iterations)
        {
            remainingIterations.Add(newIteration);
        }

        LoadNextIteration();


    }


    void ReloadCurrentPalier()
    {
        currentIteration = 1;

        remainingIterations = new List<Iteration>();

        foreach (Iteration newIteration in paliers[currentPalier].iterations)
        {
            remainingIterations.Add(newIteration);
        }

        LoadNextIteration();
    }

    void LoadNextIteration()
    {

        int rand = Random.Range(0, remainingIterations.Count);

        if (remainingIterations[rand].loadLast && remainingIterations.Count != 1)
        {
            Iteration tempIte = remainingIterations[rand];
            remainingIterations.Remove(tempIte);

            rand = Random.Range(0, remainingIterations.Count);

            remainingIterations.Add(tempIte);
        }

        foreach (GameObject go in remainingIterations[rand].patternsToSpawn)
        {
            Instantiate(go);
        }


        timerMax = remainingIterations[rand].timeBeforeNextIteration;



        if (remainingIterations[rand].skipToNextPalier)
        {
            LoadNextIteration();
            return;
        }

        remainingIterations.RemoveAt(rand);

        /*
        
		//Annonce itération
		if (currentIteration > 1) {
			textPalier.GetComponent<Text> ().text = "Iteration n°" + currentIteration.ToString ();
			AnnoncePalier.GetComponent<Animator> ().SetTrigger ("NewPalier");
		}
        //GET RANDOM MODIFICATION
        //Debug.Log("Loading Iteration " + currentIteration);

        int rand = Random.Range(0, remainingIterations.Count);

        if (remainingIterations[rand].loadLast && remainingIterations.Count != 1)
        {
            Iteration tempIte = remainingIterations[rand];
            remainingIterations.Remove(tempIte);

            rand = Random.Range(0, remainingIterations.Count);

            remainingIterations.Add(tempIte);
        }
       

        //MODIFICATIONS


        timerMax = remainingIterations[rand].timeBeforeNextIteration;


        Mngr.instance.tailleAgents += remainingIterations[rand].globalSizeChange;

        if (remainingIterations[rand].globalSizeSet != 0)
        {
            Mngr.instance.tailleAgents = remainingIterations[rand].globalSizeSet;
        }

        if (remainingIterations[rand].globalSpeedSet != 0)
        {
            EntityScript.minSpeed = remainingIterations[rand].globalSpeedSet;

            EntityScript.maxSpeed = remainingIterations[rand].globalSpeedSet + 4.5f;
        }


        //Debug.Log("Spawning " + remainingIterations[rand].newEntities.Count.ToString() + " Entities");

        foreach (EntityToCreate entity in remainingIterations[rand].newEntities)
        {
            SpawnEntity(GetFreePos(Mngr.instance.tailleAgents), entity);
        }




        if (remainingIterations[rand].globalSpeedChange != 0)
        {
            EntityScript.minSpeed += remainingIterations[rand].globalSpeedChange;

            EntityScript.maxSpeed += remainingIterations[rand].globalSpeedChange;
        }


        


        //Debug.Log("Starting Coroutine Global Color Swap");
        StartCoroutine("GlobalColorSwap", remainingIterations[rand].colorSwapToWhiteAmount);


        Mngr.instance.gainSizeSpeed += remainingIterations[rand].avatarScaleUpSpeedModif;

        Mngr.instance.lossSizeSpeed += remainingIterations[rand].avatarLossSpeedModif;

        //Debug.Log("Starting Coroutine Background Color Swap");
        StartCoroutine("BackgroundColorSwap", remainingIterations[rand].backgroundColor);

        foreach (GameObject entity in entitiesAlive)
        {
            entity.GetComponent<EntityScript>().additionalSize += Random.Range(-remainingIterations[rand].individualSizeRandomChange, remainingIterations[rand].individualSizeRandomChange);
            entity.GetComponent<EntityScript>().speedAdditionalCoefficient += Random.Range(-remainingIterations[rand].individualSpeedRandomChange, remainingIterations[rand].individualSpeedRandomChange);
            entity.GetComponent<EntityScript>().DeriveColor(remainingIterations[rand].individualColorRandomChange);
        }


        if (remainingIterations[rand].skipToNextPalier)
        {
            LoadNextIteration();
            return;
        }

        //DELETE ITERATION

        remainingIterations.RemoveAt(rand);

        */
    }

    /*public void SpawnEntity(Vector3 pos, EntityToCreate entity)
    {
        GameObject newEntity = Instantiate(entityPrefab);


        if (!entity.typeChosenToEquilibrate)
        {
            newEntity.GetComponent<EntityScript>().type = entity.type;
            newEntity.GetComponent<EntityScript>().ChangeType(entity.type);
        }
        else
        {
            EntityScript.Type typeToCreate = EntityScript.Type.None;
            List<EntityScript.Type> availableTypes = new List<EntityScript.Type>();

            if (Mngr.instance.reds < Mngr.instance.blues && Mngr.instance.reds < Mngr.instance.yellows)
                typeToCreate = EntityScript.Type.Red;

            else if (Mngr.instance.blues < Mngr.instance.reds && Mngr.instance.blues < Mngr.instance.yellows)
                typeToCreate = EntityScript.Type.Blue;

            else if (Mngr.instance.yellows < Mngr.instance.reds && Mngr.instance.yellows < Mngr.instance.blues)
                typeToCreate = EntityScript.Type.Yellow;

            else if (Mngr.instance.reds == Mngr.instance.blues && Mngr.instance.reds == Mngr.instance.yellows)
            {
                availableTypes.Add(EntityScript.Type.Red);
                availableTypes.Add(EntityScript.Type.Blue);
                availableTypes.Add(EntityScript.Type.Yellow);
            }

            else if (Mngr.instance.reds == Mngr.instance.blues)
            {
                availableTypes.Add(EntityScript.Type.Red);
                availableTypes.Add(EntityScript.Type.Blue);
            }
            
            else if (Mngr.instance.reds == Mngr.instance.yellows)
            {
                availableTypes.Add(EntityScript.Type.Red);
                availableTypes.Add(EntityScript.Type.Yellow);
            }
            else if (Mngr.instance.blues == Mngr.instance.yellows)
            {
                availableTypes.Add(EntityScript.Type.Blue);
                availableTypes.Add(EntityScript.Type.Yellow);
            }
            else
            {
                Debug.Log("Unmanaged Case /!\' ");
            }

            if (typeToCreate == EntityScript.Type.None)
            {
                int rand = Random.Range(0, availableTypes.Count);

                typeToCreate = availableTypes[rand];
                
            }

            newEntity.GetComponent<EntityScript>().type = typeToCreate;
            newEntity.GetComponent<EntityScript>().ChangeType(typeToCreate);

        }

		newEntity.GetComponent<EntityScript>().speedAdditionalCoefficient += entity.additionalSpeed;

        newEntity.transform.position = pos;

        entitiesAlive.Add(newEntity);

		//newEntity.GetComponent<EntityScriptV2> ().indexInList = entitiesAlive.Count;



        switch (newEntity.GetComponent<EntityScript>().type)
        {
            case EntityScript.Type.Red:
                Mngr.instance.reds++;
                break;

            case EntityScript.Type.Blue:
                Mngr.instance.blues++;
                break;

            case EntityScript.Type.Yellow:
                Mngr.instance.yellows++;
                break;

        }

    }
    */

	/*public void SpawnEntity(EntityScript.Type type, float additionalSpeed = 0)
	{
		Vector3 pos = GetFreePos(paliers[currentPalier].globalSize);



		GameObject newEntity = Instantiate (entityPrefab);
		newEntity.GetComponent<EntityScript>().type = type;
		newEntity.GetComponent<EntityScript>().ChangeType(type);
		newEntity.GetComponent<EntityScript>().speedAdditionalCoefficient += additionalSpeed;

		newEntity.transform.position = pos;

		entitiesAlive.Add(newEntity);

	}*/

    public EntityScript.Type GetRandomType(EntityScript.Type[] types)
    {
        int rand = Random.Range(0, types.Length);

        return types[rand];
    }

    /*IEnumerator GlobalColorSwap(float swapAmount)
    {
        //Debug.Log("Started Coroutine Global Color Swap");
        float localTimer = 0;
        float timerMax = 1;

        while (localTimer < timerMax)
        {

            foreach (GameObject entity in entitiesAlive)
            {
                entity.GetComponent<SpriteRenderer>().color = Color.Lerp(entity.GetComponent<EntityScript>().baseColor, Color.Lerp(entity.GetComponent<SpriteRenderer>().color, Color.white, swapAmount), localTimer / timerMax);
            }

            localTimer += Time.deltaTime;
            yield return null;
        }


        foreach (GameObject entity in entitiesAlive)
        {
            entity.GetComponent<SpriteRenderer>().color = Color.Lerp(entity.GetComponent<EntityScript>().baseColor, Color.white, swapAmount);
        }


    }*/

    IEnumerator BackgroundColorSwap(Color newColor)
    {
        float localTimer = 0;
        float timerMax = 1;

        while (localTimer < timerMax)
        {
            ResizeCam.background.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, newColor, localTimer / timerMax);
            localTimer += Time.deltaTime;
            yield return null;
        }

        ResizeCam.background.GetComponent<Renderer>().material.color = newColor;

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

        foreach (GameObject entity in entitiesAlive)
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


        if (reds / entitiesAlive.Count < 1f / 3f)
            return EntityScript.Type.Red;

        else if (blues / entitiesAlive.Count < 1f / 3f)
            return EntityScript.Type.Blue;

        else if (yellows / entitiesAlive.Count < 1f / 3f)
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
