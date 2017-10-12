using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour {


	public enum Type
	{
		Red,
		Blue,
		Yellow,
		None
	}


	public Color baseColor;


	public EntityScript.Type color;

	//public Color[] entityColors;
    public Sprite[] entitySprites;
    public Sprite[] haloSprites;

	public float taille;
	public int positionInCircle;

    public bool overlapAvatar;
    private bool overlapAvatarPreviousFrame;

    public Sprite[] lifePointsSprites;

    public GameObject vieAgentPrefab;
    private GameObject vieAgentSprite;

    public GameObject[] deathParticuleSystems;



	void Start(){

        EntitySpawn.entitiesAlive.Add(gameObject);

        switch (color)
        {
            case Type.Red:
                Mngr.instance.reds++;
                break;

            case Type.Blue:
                Mngr.instance.blues++;
                break;

            case Type.Yellow:
                Mngr.instance.yellows++;
                break;
        }

        vieAgentSprite = Instantiate(vieAgentPrefab, transform.position, Quaternion.identity, transform);
        vieAgentSprite.SetActive(false);

        ChangeType(color);

        taille = Mngr.instance.tailleAgents;
        transform.localScale = new Vector3(taille, taille, taille);
        
    }

	void Update(){

        //taille = Mngr.instance.tailleAgents;
        //transform.localScale =  new Vector3(taille, taille, taille);
        if (Mngr.instance.gameStarted && !Mngr.instance.gamePaused)
        {
            if (overlapAvatar != overlapAvatarPreviousFrame)
            {
                overlapAvatarPreviousFrame = overlapAvatar;

                if (overlapAvatar)
                {
                    vieAgentSprite.gameObject.SetActive(true);

                    switch (color)
                    {
                        case Type.Red:
                            GetComponent<SpriteRenderer>().sprite = haloSprites[0];
                            break;

                        case Type.Blue:
                            GetComponent<SpriteRenderer>().sprite = haloSprites[1];
                            break;

                        case Type.Yellow:
                            GetComponent<SpriteRenderer>().sprite = haloSprites[2];
                            break;
                    }

                    //GetComponent<_2dxFX_EdgeColor>().enabled = true;
                }

                else
                {
                    vieAgentSprite.gameObject.SetActive(false);

                    switch (color)
                    {
                        case Type.Red:
                            GetComponent<SpriteRenderer>().sprite = entitySprites[0];
                            break;

                        case Type.Blue:
                            GetComponent<SpriteRenderer>().sprite = entitySprites[1];
                            break;

                        case Type.Yellow:
                            GetComponent<SpriteRenderer>().sprite = entitySprites[2];
                            break;
                    }
                    //GetComponent<_2dxFX_EdgeColor>().enabled = false;
                }
            }

            if (overlapAvatar)
            {
                SendMessageUpwards("OverlapInProgress", color);
            }
        }
	}

    private void LateUpdate()
    {
        if (Mngr.instance.gameStarted && !Mngr.instance.gamePaused)
            overlapAvatar = false;
    }

    public void ChangeType (EntityScript.Type newType)
	{
        color = newType;

		switch (color)
		{
		    case EntityScript.Type.Red:
			    GetComponent<SpriteRenderer>().sprite = entitySprites[0];
			    tag = "A";
			    break;

		    case EntityScript.Type.Blue:
                GetComponent<SpriteRenderer>().sprite = entitySprites[1];
			    tag = "B";
                break;

		    case EntityScript.Type.Yellow:
                GetComponent<SpriteRenderer>().sprite = entitySprites[2];
			    tag = "C";
                break;

		}

	}

	private void OnDestroy()
	{
		EntitySpawn.entitiesAlive.Remove(gameObject);

        switch (color)
		{
		    case EntityScript.Type.Red:
			    Mngr.instance.reds--;
                break;

		    case EntityScript.Type.Blue:
			    Mngr.instance.blues--;
                break;

		    case EntityScript.Type.Yellow:
			    Mngr.instance.yellows--;
                break;

		}

    }

    public void SpawnDeathParticles()
    {
        GameObject deathParticuleSystem = null;

        switch (color)
        {
            case EntityScript.Type.Red:
                deathParticuleSystem = Instantiate(deathParticuleSystems[0], transform.position, Quaternion.identity);
                break;

            case EntityScript.Type.Blue:
                deathParticuleSystem = Instantiate(deathParticuleSystems[1], transform.position, Quaternion.identity);
                break;

            case EntityScript.Type.Yellow:
                deathParticuleSystem = Instantiate(deathParticuleSystems[2], transform.position, Quaternion.identity);
                break;

        }

        Mngr.instance.StartCoroutine(Mngr.instance.Destroyer(deathParticuleSystem, 1));
	
    }


	// OLD

    /*public float taille;
    public float additionalSize;

	public static float minSpeed = 4;  //vitesse de base
	public static float maxSpeed = 8.5f; //vitesse max si input
	public float speedAdditionalCoefficient = 0; //vitesse aditionnel
	public float speedReductionPerSecond = 0.2f; //reduction de la vitesse si pas input

    public Color baseColor;



    public enum Type
    {
        Red,
        Blue,
        Yellow,
        None
    }

    public Type type;

    public Color[] entityColors;

    public float timerChange = 2.5f;

    private float timer = 0;

	[HideInInspector]
	public float velocityAmount = 100; 

	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * (minSpeed + speedAdditionalCoefficient);
		velocityAmount = minSpeed + speedAdditionalCoefficient;


        ChangeType(type);
		
	}
	
	// Update is called once per frame
	void Update () {
        taille = Mngr.instance.tailleAgents + additionalSize;
        transform.localScale =  new Vector3(taille, taille, taille);



		if (velocityAmount > minSpeed + speedAdditionalCoefficient)
			velocityAmount -= speedReductionPerSecond * Time.deltaTime;

		if (velocityAmount < minSpeed + speedAdditionalCoefficient)
			velocityAmount = minSpeed + speedAdditionalCoefficient;

		else if (velocityAmount > maxSpeed + speedAdditionalCoefficient)
			velocityAmount = maxSpeed + speedAdditionalCoefficient;


		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * velocityAmount;

    }


    public void ChangeType (Type newType)
    {
        type = newType;

        switch (type)
        {
            case Type.Red:
                baseColor = entityColors[0];
                GetComponent<SpriteRenderer>().color = baseColor;
                tag = "A";
                break;

            case Type.Blue:
                baseColor = entityColors[1];
                tag = "B";
                break;

            case Type.Yellow:
                baseColor = entityColors[2];
                tag = "C";
                break;

        }

        GetComponent<SpriteRenderer>().color = baseColor;

    }

    public IEnumerator DeriveColor (float amount)
    {
        float h;
        float s;
        float v;
        Color.RGBToHSV(GetComponent<SpriteRenderer>().color, out h, out s, out v);
        Color targetColor = Color.HSVToRGB(Random.Range(h - amount, h + amount), s, v);

        float timer = 0;
        float timerMax = 1;

        while (timer < timerMax)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(baseColor, targetColor, timer / timerMax);

            timer += Time.deltaTime;

            yield return null;
        }

        GetComponent<SpriteRenderer>().color = targetColor;

    }

    private void OnDestroy()
    {
        EntitySpawn.entitiesAlive.Remove(gameObject);

        switch (type)
        {
            case Type.Red:
                Mngr.instance.reds--;
                break;

            case Type.Blue:
                Mngr.instance.blues--;
                break;

            case Type.Yellow:
                Mngr.instance.yellows--;
                break;

        }
    }*/

}
