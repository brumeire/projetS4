using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeCam : MonoBehaviour {

	public GameObject LeftWall;
	public GameObject RightWall;
	public GameObject TopWall;
	public GameObject BotWall;
	public GameObject BG;

    public static GameObject background;

    Vector3 CamTopRight;

    Vector3 Top;
    Vector3 Bot;
    Vector3 Left;
    Vector3 Right;


	// Use this for initialization
	void Awake () {

        CamTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight,10));

        /*Top = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight, 10));
         Bot = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0, 10));
         Left = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2, 10));
         Right = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, 10));

         LeftWall.transform.localScale = new Vector3(1, CamTopRight.y*2, 1);
         LeftWall.transform.position = new Vector3(Left.x - 0.5f, Left.y, Left.z);
         Instantiate(LeftWall);

         RightWall.transform.localScale = new Vector3(1, CamTopRight.y * 2, 1);
         RightWall.transform.position = new Vector3(Right.x + 0.5f, Right.y, Right.z);
         Instantiate(RightWall);

         TopWall.transform.localScale = new Vector3(CamTopRight.x * 2, 1, 1);
         TopWall.transform.position = new Vector3 (Top.x, Top.y + 0.5f, Top.z);
         Instantiate(TopWall);

         BotWall.transform.localScale = new Vector3(CamTopRight.x * 2, CamTopRight.y / 2.5f, 1);
         BotWall.transform.position = Bot;
         Instantiate(BotWall);*/

        background = Instantiate(BG);
        background.transform.localScale = new Vector3(CamTopRight.y * 2, CamTopRight.y * 2, 1);
        background.transform.position = new Vector3(0,0,20);



    }

    // Update is called once per frame
    void Update () {


    }
}
