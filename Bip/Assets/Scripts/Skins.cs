using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{
	public GameObject skin1;
	public GameObject skin2;
	public GameObject skin3;

	int skinNumber = 1;

	public Slider swiper;

    private Vector2 startPosition;
    private Vector2 endPosition;
    bool swipe = false;

    // Use this for initialization
    void Start()
    {
		skinNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        swipping();
		if (skinNumber== 1) {
			swiper.value = 0;
		}
		if (skinNumber== 2) {
			swiper.value = swiper.maxValue/2f;
		}
		if (skinNumber== 3) {
			swiper.value = swiper.maxValue;
		}    
	}

    void swipping()
    {

        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Stockage du point de départ
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    // Stockage du point de fin
                    endPosition = touch.position;
                    break;
            }
            swipe = true;
        }

        if (swipe && startPosition.x > endPosition.x)
        {
			if (skinNumber== 1) {
				skinNumber = 2;
				skin2.SetActive (true);
				skin1.SetActive (false);
				skin3.SetActive (false);
				swipe = false;
			}
			if (skinNumber== 2) {
				skinNumber = 3;
				skin2.SetActive (false);
				skin1.SetActive (false);
				skin3.SetActive (true);
				swipe = false;
			}
			if (skinNumber== 3) {
				skinNumber = 1;
				skin2.SetActive (false);
				skin1.SetActive (true);
				skin3.SetActive (false);
				swipe = false;
			}
        }

        if (swipe && startPosition.x < endPosition.x)
        {
			if (skinNumber== 1) {
				skinNumber = 3;
				skin2.SetActive (false);
				skin1.SetActive (false);
				skin3.SetActive (true);
				swipe = false;
			}
			if (skinNumber== 2) {
				skinNumber = 1;
				skin2.SetActive (false);
				skin1.SetActive (true);
				skin3.SetActive (false);
				swipe = false;
			}
			if (skinNumber== 3) {
				skinNumber = 2;
				skin2.SetActive (true);
				skin1.SetActive (false);
				skin3.SetActive (false);
				swipe = false;
			}
        }
    }
}
