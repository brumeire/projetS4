using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMngr : MonoBehaviour {


    public Transform[] redButtons;

    public Transform[] blueButtons;

    public Transform[] yellowButtons;

    public enum Side { Left, Right}

    public static InputMngr instance;

    public bool rightActivated;

    public EntityScript.Type rightPushed;

    public bool leftActivated;

    public EntityScript.Type leftPushed;

    public bool redActivated;

    public bool blueActivated;

    public bool yellowActivated;


    void Start()
    {
        instance = this;

        rightActivated = false;
        leftActivated = false;
        rightPushed = EntityScript.Type.None;
        leftPushed = EntityScript.Type.None;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!redActivated /*&& (!blueActivated || !yellowActivated)*/)
            {
                redActivated = true;
            }
            ResetButtonsColors();
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!blueActivated /*&& (!redActivated || !yellowActivated)*/)
            {
                blueActivated = true;
            }
            ResetButtonsColors();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!yellowActivated /*&& (!redActivated || !blueActivated)*/)
            {
                yellowActivated = true;
            }
            ResetButtonsColors();
        }



        if (Input.GetKeyUp(KeyCode.A))
        {
            redActivated = false;
            ResetButtonsColors();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            blueActivated = false;
            ResetButtonsColors();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            yellowActivated = false;
            ResetButtonsColors();
        }

    }

    public void Pushed (InputType input)
    {
        switch (input.type)
        {
            case EntityScript.Type.Red:
                if (!redActivated /*&& (!blueActivated || !yellowActivated)*/)
                {
                    redActivated = true;
                }
                break;

            case EntityScript.Type.Blue:
                if (!blueActivated /*&& (!redActivated || !yellowActivated)*/)
                {
                    blueActivated = true;
                }
                break;

            case EntityScript.Type.Yellow:
                if (!yellowActivated /*&& (!redActivated || !blueActivated)*/)
                {
                    yellowActivated = true;
                }
                break;
        }

        ResetButtonsColors();


    }


    public void Released (InputType input)
    {
        switch (input.type)
        {
            case EntityScript.Type.Red:
                redActivated = false;
                break;

            case EntityScript.Type.Blue:
                blueActivated = false;
                break;

            case EntityScript.Type.Yellow:
                yellowActivated = false;
                break;

        }

        ResetButtonsColors();


    }


    void ResetButtonsColors()
    {

        if (redActivated)
        {
            foreach (Transform button in redButtons)
            {
                button.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(137f / 255f, 137f / 255f, 137f / 255f);
            }
        }

        else
        {
            foreach (Transform button in redButtons)
            {
                button.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }




        if (blueActivated)
        {
            foreach (Transform button in blueButtons)
            {
                button.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(137f / 255f, 137f / 255f, 137f / 255f);
            }
        }

        else
        {
            foreach (Transform button in blueButtons)
            {
                button.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }


        if (yellowActivated)
        {
            foreach (Transform button in yellowButtons)
            {
                button.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(137f / 255f, 137f / 255f, 137f / 255f);
            }
        }

        else
        {
            foreach (Transform button in yellowButtons)
            {
                button.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }


    } 

}
