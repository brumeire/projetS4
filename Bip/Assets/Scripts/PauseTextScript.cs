using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseTextScript : MonoBehaviour {

    public Color[] colors;

    private Text textComponent;

    private int state = 0;

    private float timer = 0;

    public float timerBetweenColors = 3;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case 0:
                textComponent.color = Color.Lerp(colors[0], colors[1], timer / timerBetweenColors);
                break;

            case 1:
                textComponent.color = Color.Lerp(colors[1], colors[2], timer / timerBetweenColors);
                break;

            case 2:
                textComponent.color = Color.Lerp(colors[2], colors[0], timer / timerBetweenColors);
                break;
        }

        if (timer >= timerBetweenColors)
        {
            timer -= timerBetweenColors;

            state = (state + 1) % 3;
        }
	}
}
