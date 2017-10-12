using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {


	public float money;
	float timer30;
	float timerChaine;
	bool inCooldown;
	float cooldown;
	int gainChaine;
	bool first = true;

	// Use this for initialization
	void Start () {
		timer30 = 0;
		timerChaine = 0;
		inCooldown = false;
		gainChaine = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mngr.instance.gameStarted && !Mngr.instance.gamePaused) {
			timer30 += Time.deltaTime;

			if (timer30 >= 30) {
				timer30 = 0;
				money++;
			}


			if (cooldown > 0 && !Guru.Gold) {
				inCooldown = true;
				timerChaine = 0;
				gainChaine = 1;
				cooldown -= Time.deltaTime;
				first = true;
			}

			if (Guru.Gold) {
				cooldown -= Time.deltaTime;
			}

			if (cooldown <= 0) {
				inCooldown = false;
			}
			if (Guru.Gold && !inCooldown) {
				if (first){
					money += gainChaine;
					first = false;
				} 
				timerChaine += Time.deltaTime;
				cooldown = 5 - timerChaine;
				if (timerChaine >= 5) {
					timerChaine = 0;
					gainChaine += 1;
					money += gainChaine;
				}

			}
			//Debug.Log (money);

		}
	}
}
