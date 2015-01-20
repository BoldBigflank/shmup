﻿using UnityEngine;
using System.Collections;

public class DotScript : MonoBehaviour {
	public float speed = 15.0F;
	public GameObject source;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable(){
		Invoke ("Destroy", 2.0F);
		rigidbody2D.AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
	}
	
	void Destroy(){
		gameObject.SetActive(false);
	}
	
	void OnDisable(){
		CancelInvoke();
	}
	
	// Update is called once per frame
	void Update () {
//		transform.Translate(0, speed*Time.deltaTime, 0);
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("onTrigger");
	}
	
	void OnCollisionEnter2D(Collision2D other){
//		Debug.Log ("onCollision");
		if(other.gameObject == source) return;
		
//		CancelInvoke();
//		gameObject.SetActive(false);
		
	}
}
