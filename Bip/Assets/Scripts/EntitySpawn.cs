using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EntitySpawn : MonoBehaviour {

    public GameObject entityPrefab;
    public GameObject circlePrefab;
    public GameObject triangle2Prefab;
    public GameObject triangle3Prefab;
    public GameObject triangle4Prefab;
    public GameObject squarePrefab2x2;
    public GameObject squarePrefab3x3;
    public GameObject squarePrefab5x5;
    public GameObject barrePrefab;



    public float timer = 0;

	public GameObject AnnoncePalier;
	public GameObject textPalier;

    public GameObject playButton;

    [HideInInspector]
    public float timerMax;
    [HideInInspector]
    public int currentPalier = -1;

    public List<Palier> paliers = new List<Palier>();
	public static List<GameObject> entitiesAlive = new List<GameObject>();

    private int currentIteration = 1;

    private List<Iteration> remainingIterations = new List<Iteration>();


    public static EntitySpawn instance;


    private int square2Weight = 4;
    private int square3Weight = 9;
    private int square5Weight = 25;

    private int triangle2Weight = 3;
    private int triangle3Weight = 6;
    private int triangle4Weight = 10;

    private int barreWeight = 7;
    

	// Use this for initialization
	void Start ()
    {
        instance = this;

        Mngr.instance.gameStarted = false;


    }
	
	// Update is called once per frame
	void Update () {

        if (Mngr.instance.gameStarted && !Mngr.instance.gamePaused)
        {

            timer += Time.deltaTime;

            if (timer >= timerMax)
            {
                timer -= timerMax;

                if (remainingIterations.Count > 0)
                {
                    LoadNextIteration();
                }

                else if (currentIteration < paliers[currentPalier].IterationsBeforeNextPalier)
                {
                    currentIteration++;
                    ReloadCurrentPalier();
                }

                else
                    LoadNextPalier();
            }

        } 
        
        
           
    }


    public void StartGame()
    {
        timer = 0;
        currentPalier = -1;

        LoadNextPalier();
    }

    void LoadNextPalier()
    {
        if (currentPalier + 1 < paliers.Count)
            currentPalier++;

		/*if (currentPalier >= 1) {
			textPalier.GetComponent<Text> ().text = "Palier n°" + currentPalier.ToString () + "\tIteration n°" + currentIteration.ToString();
			AnnoncePalier.GetComponent<Animator> ().SetTrigger ("NewPalier");
		}*/
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

            rand = Random.Range(0, remainingIterations.Count - 1);

            remainingIterations.Add(tempIte);
        }



        //Un seul pattern à faire spawn à la fois
        if (remainingIterations[rand].patternsToSpawn.Count == 1)
        {
            Pattern pattern = remainingIterations[rand].patternsToSpawn[0];

            EntityScript.Type objectType = pattern.type;

            if (pattern.chooseRandomType)
            {
                objectType = GetRandomType();
            }

            else
                objectType = pattern.type;


            SpawnPattern(pattern, objectType);

        }


        //Plusieurs patterns à faire spawn => gestion des problèmes d'égalité
        else
        {

            EntityScript.Type[] objectTypes = new EntityScript.Type[remainingIterations[rand].patternsToSpawn.Count];

            int redWeight =  0, blueWeight = 0, greenWeight = 0;


            //Initial setup
            for (int i = 0; i < remainingIterations[rand].patternsToSpawn.Count; i++)
            {


                //Set patterns types
                if (remainingIterations[rand].patternsToSpawn[i].chooseRandomType)
                {
                    objectTypes[i] = GetRandomType();
                }

                else
                    objectTypes[i] = remainingIterations[rand].patternsToSpawn[i].type;




                //Additionning to color weights
                int weightToAdd = 0;


                switch (remainingIterations[rand].patternsToSpawn[i].objectToSpawn)
                {
                    case Pattern.Type.Square:

                        switch (remainingIterations[rand].patternsToSpawn[i].squareSize)
                        {
                            case SquareSizes.s2x2:
                                weightToAdd = square2Weight;
                                break;

                            case SquareSizes.s3x3:
                                weightToAdd = square3Weight;
                                break;

                            case SquareSizes.s5x5:
                                weightToAdd = square5Weight;
                                break;
                        }

                        break;


                    case Pattern.Type.Triangle:

                        switch (remainingIterations[rand].patternsToSpawn[i].triangleSize)
                        {
                            case TriangleSizes.s2:
                                weightToAdd = triangle2Weight;
                                break;

                            case TriangleSizes.s3:
                                weightToAdd = triangle3Weight;
                                break;

                            case TriangleSizes.s4:
                                weightToAdd = triangle4Weight;
                                break;
                        }

                        break;


                    case Pattern.Type.Bar:
                        weightToAdd = barreWeight;
                        break;
                }


                switch (objectTypes[i])
                {
                    case EntityScript.Type.Red:
                        redWeight += weightToAdd;
                        break;

                    case EntityScript.Type.Blue:
                        blueWeight += weightToAdd;
                        break;

                    case EntityScript.Type.Yellow:
                        greenWeight += weightToAdd;
                        break;
                }



            }
            //End of Initial Setup


            //Gestion des égalités
            bool isMajorityConflict = false;
            List<EntityScript.Type> conflictingTypes = new List<EntityScript.Type>();

            //Check conflits
            if (redWeight == blueWeight && redWeight > greenWeight) 
            {
                isMajorityConflict = true;
                conflictingTypes.Add(EntityScript.Type.Red);
                conflictingTypes.Add(EntityScript.Type.Blue);
            }

            else if (redWeight == greenWeight && redWeight > blueWeight)
            {
                isMajorityConflict = true;
                conflictingTypes.Add(EntityScript.Type.Red);
                conflictingTypes.Add(EntityScript.Type.Yellow);
            }

            else if (blueWeight == greenWeight && blueWeight > redWeight)
            {
                isMajorityConflict = true;
                conflictingTypes.Add(EntityScript.Type.Blue);
                conflictingTypes.Add(EntityScript.Type.Yellow);
            }

            else if (redWeight == blueWeight && redWeight == greenWeight)
            {
                isMajorityConflict = true;
                conflictingTypes.Add(EntityScript.Type.Red);
                conflictingTypes.Add(EntityScript.Type.Blue);
                conflictingTypes.Add(EntityScript.Type.Yellow);
            }

            
            //Gestion des cas où couleur unique
            else if ((redWeight == 0 && blueWeight == 0) || (blueWeight == 0 && greenWeight == 0) || (redWeight == 0 && greenWeight == 0))
            {
                isMajorityConflict = true;
            }


            //Gestion des conflits
            if (isMajorityConflict)
            {

                //Conflit de 2
                if (conflictingTypes.Count == 2)
                {
                    int randomColor = Random.Range(0, 2);

                    EntityScript.Type targetColor = conflictingTypes[randomColor];
                    EntityScript.Type changeToColor = conflictingTypes[(randomColor + 1) % 2];

                    for(int i = 0; i < objectTypes.Length; i++)
                    {
                        if (objectTypes[i] == targetColor)
                        {
                            objectTypes[i] = changeToColor;
                            break;
                        }
                    }


                }


                //Conflit de 3
                else if (conflictingTypes.Count == 3)
                {
                    EntityScript.Type targetColor = conflictingTypes[Random.Range(0, 3)];


                    for (int i = 0; i < objectTypes.Length; i++)
                    {
                        if (objectTypes[i] != targetColor)
                        {
                            objectTypes[i] = targetColor;
                            break;
                        }
                    }

                }

                



                //Gestion des cas d'une seule couleur
                else if (conflictingTypes.Count == 0)
                {

                    switch (objectTypes[0])
                    {
                        case EntityScript.Type.Red:
                            conflictingTypes.Add(EntityScript.Type.Blue);
                            conflictingTypes.Add(EntityScript.Type.Yellow);
                            break;

                        case EntityScript.Type.Blue:
                            conflictingTypes.Add(EntityScript.Type.Red);
                            conflictingTypes.Add(EntityScript.Type.Yellow);
                            break;

                        case EntityScript.Type.Yellow:
                            conflictingTypes.Add(EntityScript.Type.Red);
                            conflictingTypes.Add(EntityScript.Type.Blue);
                            break;
                    }


                    int randomColor = Random.Range(0, 2);
                    EntityScript.Type changeToColor = conflictingTypes[(randomColor + 1) % 2];

                    objectTypes[Random.Range(0, objectTypes.Length)] = changeToColor;


                }



            }
            //Fin gestion des conflits



            //Spawn Agents

            for (int i = 0; i < remainingIterations[rand].patternsToSpawn.Count; i++)
            {
                SpawnPattern(remainingIterations[rand].patternsToSpawn[i], objectTypes[i]);
            }


        }


        timerMax = remainingIterations[rand].timeBeforeNextIteration;




        if (remainingIterations[rand].skipToNextPalier)
        {
            if (remainingIterations.Count > 1)
                LoadNextIteration();
            else
                LoadNextPalier();

            return;
        }


        remainingIterations.RemoveAt(rand);


    }

    
    private void SpawnPattern(Pattern pattern, EntityScript.Type objectType)
    {
        GameObject newGo;


        switch (pattern.objectToSpawn)
        {
            case Pattern.Type.Circle:

                newGo = Instantiate(circlePrefab);
                newGo.transform.position = pattern.startPosition;
                CircleScript mngr = newGo.GetComponent<CircleScript>();

                mngr.rayonMax = pattern.circleRayonMax;
                mngr.rayonMin = pattern.circleRayonMin;
                mngr.breathSpeed = pattern.circleBreathSpeed;
                mngr.rotationSpeed = pattern.circleRotationSpeed;
                mngr.entityNb = pattern.circleEntityNb;

                mngr.StartCircle(objectType, pattern.circleMovement);

                break;



            case Pattern.Type.Triangle:

                switch (pattern.triangleSize)
                {
                    case TriangleSizes.s2:
                        newGo = Instantiate(triangle2Prefab);
                        break;

                    case TriangleSizes.s3:
                        newGo = Instantiate(triangle3Prefab);
                        break;

                    case TriangleSizes.s4:
                        newGo = Instantiate(triangle4Prefab);
                        break;

                    default:
                        newGo = Instantiate(triangle2Prefab);
                        break;
                }


                if (pattern.randomStartPosition)
                {
                    newGo.transform.position = GetFreePos(newGo.GetComponent<TriangleMove>().size);
                }

                else
                {
                    newGo.transform.position = pattern.startPosition;
                }

                newGo.GetComponent<TriangleMove>().movementType = pattern.triangleMovement;
                newGo.GetComponent<TriangleMove>().speed = newGo.transform.position.magnitude/pattern.triangleSpeed;
                newGo.GetComponent<TriangleMove>().type = objectType;

                newGo.GetComponent<TriangleMove>().StartTriangle();
                break;



            case Pattern.Type.Square:

                switch (pattern.squareSize)
                {
                    case SquareSizes.s2x2:
                        newGo = Instantiate(squarePrefab2x2);
                        break;

                    case SquareSizes.s3x3:
                        newGo = Instantiate(squarePrefab3x3);
                        break;

                    case SquareSizes.s5x5:
                        newGo = Instantiate(squarePrefab5x5);
                        break;

                    default:
                        newGo = Instantiate(squarePrefab2x2);
                        break;
                }
                

                if (pattern.randomStartPosition)
                {
                    newGo.transform.position = GetFreePos(newGo.GetComponent<SquareMove>().size);
                }

                else
                {
                    newGo.transform.position = pattern.startPosition;
                }

                newGo.GetComponent<SquareMove>().movementType = pattern.squareMovement;
                newGo.GetComponent<SquareMove>().speed = newGo.transform.position.magnitude/pattern.squareSpeed;
                newGo.GetComponent<SquareMove>().type = objectType;

                newGo.GetComponent<SquareMove>().StartSquare();
                break;

            case Pattern.Type.Bar:

                newGo = Instantiate(barrePrefab);

                Vector3 position;

                Vector3 direction;

                if (pattern.randomStartPosition)
                {
                    GetFreePos(newGo.GetComponent<BarreMove>().halfExtents, out position, out direction);
                    newGo.transform.position = position;
                }

                else
                {
                    newGo.transform.position = pattern.startPosition;
                    direction = -newGo.transform.position + (Vector3)Random.insideUnitCircle.normalized * Guru.instance.RessourcesMax * 1.3f;
                }

                newGo.GetComponent<BarreMove>().movementType = pattern.barMovement;
                newGo.GetComponent<BarreMove>().speed = newGo.transform.position.magnitude/pattern.squareSpeed;
                newGo.GetComponent<BarreMove>().type = objectType;

                newGo.GetComponent<BarreMove>().StartBarre(direction);
                break;
        }
    }


    public EntityScript.Type GetRandomType(EntityScript.Type[] types)
    {
        int rand = Random.Range(0, types.Length);

        return types[rand];
    }



    public EntityScript.Type GetRandomType()
    {
        switch(Random.Range(0, 3))
        {
            case 0:
                return EntityScript.Type.Red;
                break;

            case 1:
                return EntityScript.Type.Blue;
                break;

            case 2:
                return EntityScript.Type.Yellow;
                break;

            default:
                return EntityScript.Type.None;
                break;
        }


    }

    


    Vector3 GetFreePos(float size)
    {
        Vector3 newPos = Vector3.zero;

        for (int i = 0; i < 200; i++)
        {
            newPos = (Vector3)Random.insideUnitCircle.normalized * 15;

            int layerMask = 1 << 9;

            Collider[] surroundings = Physics.OverlapSphere(newPos, size, layerMask);
            if (surroundings.Length <= 0)
            {
                return newPos;
            }
        }

        return Vector3.zero;


    }


    void GetFreePos(Vector3 halfExtents, out Vector3 newPos, out Vector3 direction)
    {
        newPos = Vector3.zero;
        direction = Vector3.zero;

        Vector3 targetPoint = (Vector3)Random.insideUnitCircle.normalized * Guru.instance.RessourcesMax;

        for (int i = 0; i < 200; i++)
        {
            newPos = (Vector3)Random.insideUnitCircle.normalized * 15;
            direction = -newPos + targetPoint;
            
            int layerMask = 1 << 9;

            Collider[] surroundings = Physics.OverlapBox(newPos, halfExtents, Quaternion.Euler(direction), layerMask);
            if (surroundings.Length == 0)
            {
                return;
            }
        }

        return;
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
