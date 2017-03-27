using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour {


    public float Taille;

    public enum Type
    {
        Red,
        Blue,
        Yellow,
        None
    }

    public Type type;

    public Sprite[] entitySprites;

    public float timerChange = 2.5f;

    private float timer = 0;

	// Use this for initialization
	void Start () {


      //  transform.localScale = transform.localScale * Taille;


        ChangeType(type);
		
	}
	
	// Update is called once per frame
	void Update () {
        Taille = EntitySpawn.taille;
        transform.localScale =  new Vector3(Taille,Taille,Taille);

    }


    public void ChangeType (Type newType)
    {
        type = newType;

        switch (type)
        {
            case Type.Red:
                GetComponent<SpriteRenderer>().sprite = entitySprites[0];
                tag = "A";
                break;

            case Type.Blue:
                GetComponent<SpriteRenderer>().sprite = entitySprites[1];
                tag = "B";
                break;

            case Type.Yellow:
                GetComponent<SpriteRenderer>().sprite = entitySprites[2];
                tag = "C";
                break;

        }
        

    }

    private void OnDestroy()
    {
        SpawnerScript.entitiesAlive.Remove(gameObject);
    }

}
