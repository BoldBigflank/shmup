﻿using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D other){
		//		Debug.Log ("onCollision");
//		if(other.gameObject.CompareTag("Bullet"))
//			gameObject.SetActive(false);
		
	}
}
