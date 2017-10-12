﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreMove : MonoBehaviour
{

    Vector3 direction;
    public Vector3 halfExtents;
    public float speed = 1;

    public PatternBar movementType;

    public EntityScript.Type type;

    public int lifePoints;

    private float timerOverlap;

    [FMODUnity.EventRef]
    public string sonDestruction;

    public void StartBarre(Vector3 rotation)
    {
        lifePoints = 0;

        ChangeChildsTypes(type);

        switch (movementType)
        {
            case PatternBar.Move:
                direction = rotation;
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
    void Update()
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
                SoundStackMngr.instance.destructionStack.Add(sonDestruction);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<EntityScript>().SpawnDeathParticles();
                }
                Destroy(gameObject);
            }
        }
    }

}