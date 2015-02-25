﻿using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {
	public float fireTime = 0.05F;
	float fireCooldown;
	public string projectile;

	[SerializeField]
	PlayerInputScript playerInputScript;
	
	// Use this for initialization
	void Start () {
//		playerInputScript = gameObject.GetComponent<PlayerInputScript>();
		fireCooldown = fireTime;
	}
	
	void Update(){
		if(!playerInputScript) return;
		if(fireCooldown > 0.0F) fireCooldown -=Time.deltaTime;
		if(fireCooldown <= 0.0F && playerInputScript.getIsFiring()){
			Fire ();
		}
	}
	
	void Fire () {
		GameObject dot = NewObjectPoolerScript.current.Spawn(projectile);
		if(!dot) return;
		dot.transform.position = transform.position;
		dot.transform.rotation = transform.rotation;
		dot.transform.Rotate(Vector3.back, 90.0F);
		dot.GetComponent<DotScript>().source = gameObject.transform.parent.gameObject;
		
		dot.SetActive(true);
        Physics2D.IgnoreCollision(dot.collider2D, gameObject.transform.parent.gameObject.collider2D);
		fireCooldown = fireTime;
	}
}
