using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour {

    public PatternCircle MovementType;
    public float rayonInTime;
    public float angleRad;
    public float angleDegC;
    public float x;
    public float y;
    public bool move = true;
    bool placement;


    CircleScript circleHolder;

    void Start()
    {
        placement = true;


        switch (MovementType)
        {
            case PatternCircle.Rotation:
                circleHolder = transform.parent.GetComponent<CircleScript>();
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {
        ChangeType(MovementType);

    }


    public void ChangeType(PatternCircle newType)
    {
        MovementType = newType;


        switch (MovementType)
        {

            case PatternCircle.Rotation:

                if (move)
                {
                    rayonInTime -= Time.deltaTime * circleHolder.breathSpeed;
                    if (rayonInTime <= circleHolder.rayonMin)
                    {
                        move = false;
                    }
                }
                if (!move)
                {
                    rayonInTime += Time.deltaTime * circleHolder.breathSpeed;
                    if (rayonInTime >= circleHolder.rayonMax)
                    {
                        move = true;
                    }
                }
                if (placement)
                {
                    angleDegC = (360 / circleHolder.entityNb) * GetComponent<EntityScript>().positionInCircle;
                    rayonInTime = circleHolder.rayonMax;
                    placement = false;
                }
                angleDegC += Time.deltaTime * circleHolder.rotationSpeed;

                angleRad = angleDegC * Mathf.Deg2Rad;
                x = Mathf.Cos(angleRad) * rayonInTime;
                y = Mathf.Sin(angleRad) * rayonInTime;
                transform.position = circleHolder.center + new Vector3(x, y, 0);
                break;





                /*case MovementTypes.barre:
                    circleHolder.angleDeg += Time.deltaTime * circleHolder.rotationSpeed;
                    angleRad = circleHolder.angleDeg * Mathf.Deg2Rad;
                    x = Mathf.Cos (angleRad) * circleHolder.espacement * (GetComponent<EntityScript> ().positionInCircle-1);
                    y = Mathf.Sin (angleRad) * circleHolder.espacement * (GetComponent<EntityScript> ().positionInCircle-1);

                    transform.position = circleHolder.center + new Vector3 (x, y, 0);

                    break;*/







        }
    }
}
