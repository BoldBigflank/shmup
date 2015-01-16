using UnityEngine;
using System.Collections;

public class PlayerInputScript : MonoBehaviour {
	float baseSpeed = 7.0F;
	float inputDeadSquare = 0.13F * 0.13F;
	bool isFiring;
	
	void Start () {
	
	}
	
	void OnCollisionEnter (Collision collision){
		Debug.Log("Player Collision Enter");
	
	}
	
	void OnTriggerEnter(Collider other){
		Debug.Log ("Player Triggered " + other.tag);
		if(other.CompareTag("Enemy")){
			// Send a the player's FSM Hit event
			
		}
	}
	
	void OnGUI(){
		GUI.Label(new Rect(0.0F, 0.0F, 100.0F, 100.0F), Input.GetAxis ("Player1_Fire").ToString());
		GUI.Label(new Rect(0.0F, 100.0F, 100.0F, 100.0F), Input.GetAxis ("Player1_AimY").ToString());
	}
		
	// Update is called once per frame
	void Update () {
		
		// Movement
		float xAxis = Input.GetAxis ("Player1_MoveX");
		float yAxis = Input.GetAxis ("Player1_MoveY");
		Vector3 direction = Vector3.zero;
		if(xAxis * xAxis + yAxis * yAxis > inputDeadSquare){
			direction.x += xAxis * baseSpeed * Time.deltaTime;
			direction.y += yAxis * baseSpeed * Time.deltaTime;
			float playerAngle = Vector3.Angle(Vector3.right, direction);
			if( direction.y < 0.0F ) playerAngle = 360.0F - playerAngle;
			transform.rotation = Quaternion.AngleAxis(playerAngle, Vector3.forward);
		}
		GetComponent<CharacterController>().Move(direction);
		
		// Aiming
		float playerAimX = Input.GetAxis("Player1_AimX");
		float playerAimY = Input.GetAxis("Player1_AimY");
		Vector3 aimAngleVector = new Vector3(playerAimX, playerAimY, 0.0F);
		float aimAngle = Vector3.Angle(Vector3.right, aimAngleVector);
		if( aimAngleVector.y < 0.0F ) aimAngle = 360.0F - aimAngle;
		if(aimAngleVector.magnitude > 0.0F)transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
		
		// Firing
		isFiring = (Input.GetAxis ("Player1_Fire") > 0.1F);
		
	}
	
	public bool getIsFiring(){
		return isFiring;
	}
}
