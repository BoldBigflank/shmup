using UnityEngine;
using System.Collections;

public class TriShooterScript : MonoBehaviour {
	public float fireTime = 0.08F;
	float fireCooldown;
	
	PlayerInputScript playerInputScript;
	
	// Use this for initialization
	void Start () {
		playerInputScript = gameObject.GetComponent<PlayerInputScript>();
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
		GameObject dot = NewObjectPoolerScript.current.Spawn("Dot");
		if(!dot) return;
		dot.transform.position = transform.position;
		dot.transform.rotation = transform.rotation;
		dot.transform.Rotate(Vector3.back, 90.0F);
		dot.GetComponent<DotScript>().source = gameObject;
		dot.GetComponent<DotScript>().life = 0.15F;
		dot.SetActive(true);
		
		GameObject leftDot = NewObjectPoolerScript.current.Spawn("Dot");
		if(!leftDot) return;
		leftDot.transform.position = transform.position;
		leftDot.transform.rotation = transform.rotation;
		leftDot.transform.Rotate(Vector3.back, 70.0F);
		leftDot.GetComponent<DotScript>().source = gameObject;
		leftDot.GetComponent<DotScript>().life = 0.15F;
		leftDot.SetActive(true);
		
		GameObject rightDot = NewObjectPoolerScript.current.Spawn("Dot");
		if(!rightDot) return;
		rightDot.transform.position = transform.position;
		rightDot.transform.rotation = transform.rotation;
		rightDot.transform.Rotate(Vector3.back, 110.0F);
		rightDot.GetComponent<DotScript>().source = gameObject;
		rightDot.GetComponent<DotScript>().life = 0.15F;
		rightDot.SetActive(true);
		
		fireCooldown = fireTime;
	}
}
