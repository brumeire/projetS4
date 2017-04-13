using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.R))
            ReloadCurrentScene();*/
	}

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
		//EntityScript.minSpeed = 2;
		//EntityScript.maxSpeed = 5;
        EntitySpawn.instance.StartGame();
    }

}
