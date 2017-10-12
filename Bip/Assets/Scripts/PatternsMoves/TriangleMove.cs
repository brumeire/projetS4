using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMove : MonoBehaviour {

    Vector3 direction;
    public float speed = 1;

    public PatternTriangle movementType;

    public EntityScript.Type type;

    public float size = 3.3f;

    public int lifePoints;

    private float timerOverlap;

	[FMODUnity.EventRef]
	public string sonDestruction;

	public void StartTriangle ()
    {
        lifePoints = 0;

        ChangeChildsTypes(type);

        switch (movementType)
        {
            case PatternTriangle.Line:
                direction = -transform.position;
                direction += (Vector3)Random.insideUnitCircle.normalized * Guru.instance.size * Mathf.Lerp(1.2f, 1.2f, Guru.instance.Ressources / Guru.instance.RessourcesMax);
                break;
        }

        Vector3 dir = direction;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Rotation aléatoire sprites agents
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 newRot = new Vector3(0, 0, Random.Range(-180f, 180f));
            transform.GetChild(i).localEulerAngles = newRot;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!Mngr.instance.gamePaused)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right * 50, speed * Time.deltaTime);

            if (transform.position.magnitude > 20)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }     
                Destroy(gameObject);
            }


            timerOverlap += Time.deltaTime;

            if (timerOverlap >= 0.4f)
            {
                lifePoints = 0;
                DisplayChildrenLifepoint();
            }

        }
	}


    void ChangeChildsTypes(EntityScript.Type newType)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<EntityScript>().ChangeType(newType);
        }
    }


    private void DisplayChildrenLifepoint()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = transform.GetChild(i).GetComponent<EntityScript>().lifePointsSprites[lifePoints];
        }
    }


    public void OverlapInProgress(EntityScript.Type inputType)
    {
        if (timerOverlap >= Mngr.instance.timerPerAgentLifepoint && inputType == InputMngr.instance.currentInput)
        {
            lifePoints++;
            timerOverlap = 0;
            DisplayChildrenLifepoint();

            if (lifePoints >= 6)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<EntityScript>().SpawnDeathParticles();
                }
                SoundStackMngr.instance.destructionStack.Add(sonDestruction);
                Destroy(gameObject);
            }
        }
    }
}
