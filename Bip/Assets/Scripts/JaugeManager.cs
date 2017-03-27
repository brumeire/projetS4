using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeManager : MonoBehaviour {

    public Transform redJauge;
    public Transform blueJauge;
    public Transform yellowJauge;

    public float jaugeFillSpeed = 1;


    public bool redJaugeFilled;
    public bool blueJaugeFilled;
    public bool yellowJaugeFilled;

    public static JaugeManager instance;

    private void Start()
    {
        instance = this;

        redJaugeFilled = false;
        blueJaugeFilled = false;
        yellowJaugeFilled = false;

    }


    public void FillJauge(EntityScript.Type type)
    {
        switch (type)
        {
            case EntityScript.Type.Red:
                FillRedJauge();
                break;

            case EntityScript.Type.Blue:
                FillBlueJauge();
                break;

            case EntityScript.Type.Yellow:
                FillYellowJauge();
                break;

        }

    }


    void FillRedJauge()
    {
        redJauge.FindChild("Fill").GetComponent<Image>().fillAmount += jaugeFillSpeed * Time.deltaTime;

        if (redJauge.FindChild("Fill").GetComponent<Image>().fillAmount >= 1)
        {
            redJauge.FindChild("Center").GetComponent<Image>().color = new Color(1, 0, 90f / 255f); // old = 740000FF
            redJaugeFilled = true;
            redJauge.FindChild("Button").GetComponent<Button>().interactable = true;
        }
    }


    void FillBlueJauge()
    {
        blueJauge.FindChild("Fill").GetComponent<Image>().fillAmount += jaugeFillSpeed * Time.deltaTime;

        if (blueJauge.FindChild("Fill").GetComponent<Image>().fillAmount >= 1)
        {
            blueJauge.FindChild("Center").GetComponent<Image>().color = new Color(0, 213f / 255f, 1); // old = 33 31 96
            blueJaugeFilled = true;
            blueJauge.FindChild("Button").GetComponent<Button>().interactable = true;
        }
    }


    void FillYellowJauge()
    {
        yellowJauge.FindChild("Fill").GetComponent<Image>().fillAmount += jaugeFillSpeed * Time.deltaTime;

        if (yellowJauge.FindChild("Fill").GetComponent<Image>().fillAmount >= 1)
        {
            yellowJauge.FindChild("Center").GetComponent<Image>().color = new Color(1, 248f / 255f, 0); // old = 169 123 0
            yellowJaugeFilled = true;
            yellowJauge.FindChild("Button").GetComponent<Button>().interactable = true;
        }
    }







    public void ActivateRed()
    {
        if (redJaugeFilled)
        {
            redJauge.FindChild("Fill").GetComponent<Image>().fillAmount = 0;
            redJauge.FindChild("Center").GetComponent<Image>().color = new Color(116f / 255f, 0, 0);
            redJauge.FindChild("Button").GetComponent<Button>().interactable = false;
            redJaugeFilled = false;

            foreach (GameObject go in SpawnerScript.entitiesAlive)
            {
                if (go.tag == "A")
                    Destroy(go);
            }
        }
    }


    public void ActivateBlue()
    {
        if (blueJaugeFilled)
        {
            blueJauge.FindChild("Fill").GetComponent<Image>().fillAmount = 0;
            blueJauge.FindChild("Center").GetComponent<Image>().color = new Color(33f / 255f, 31f / 255f, 96f / 255f);
            blueJauge.FindChild("Button").GetComponent<Button>().interactable = false;
            blueJaugeFilled = false;

            foreach (GameObject go in SpawnerScript.entitiesAlive)
            {
                if (go.tag == "B")
                    Destroy(go);
            }
        }
    }


    public void ActivateYellow()
    {
        if (yellowJaugeFilled)
        {
            yellowJauge.FindChild("Fill").GetComponent<Image>().fillAmount = 0;
            yellowJauge.FindChild("Center").GetComponent<Image>().color = new Color(169f / 255f, 123f / 255f, 0);
            yellowJauge.FindChild("Button").GetComponent<Button>().interactable = false;
            yellowJaugeFilled = false;

            foreach (GameObject go in SpawnerScript.entitiesAlive)
            {
                if (go.tag == "C")
                    Destroy(go);
            }
        }
    }
}
