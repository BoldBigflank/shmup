using UnityEngine;
using System.Collections;

public class PlayerInputScript : MonoBehaviour {
	public float baseSpeed = 7.0F;
	float inputDeadSquare = 0.13F * 0.13F;
	bool isFiring;
	Vector2 newPos;
	
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
		GUI.Label(new Rect(0.0F, 0.0F, 100.0F, 100.0F), newPos.ToString());
		GUI.Label(new Rect(0.0F, 100.0F, 100.0F, 100.0F), Input.GetAxis ("Player1_AimY").ToString());
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		
		// Movement
		float xAxis = Input.GetAxis ("Player1_MoveX");
		float yAxis = Input.GetAxis ("Player1_MoveY");
		Vector2 direction = Vector2.zero;
		
		direction.x += xAxis * baseSpeed;
		direction.y += yAxis * baseSpeed;
		float playerAngle = Vector3.Angle(Vector3.right, direction);
		if( direction.y < 0.0F ) playerAngle = 360.0F - playerAngle;
		rigidbody2D.MoveRotation(playerAngle);
//		transform.rotation = Quaternion.AngleAxis(playerAngle, Vector3.forward);
	
		rigidbody2D.MovePosition( rigidbody2D.position + direction * Time.deltaTime );
		//if (c != CollisionFlags.None) {
	//		Debug.Log("Collided");
	//	}
		// Clamp the player to the bounds of the screen
//		Vector3 pos = transform.position;
//		pos.x = Mathf.Clamp(pos.x, -5.0F, 5.0F);
//		pos.y = Mathf.Clamp(pos.y, -5.0F, 5.0F);
//		transform.position = pos;
		
		// Aiming
		float playerAimX = Input.GetAxis("Player1_AimX");
		float playerAimY = Input.GetAxis("Player1_AimY");
		Vector3 aimAngleVector = new Vector3(playerAimX, playerAimY, 0.0F);
		float aimAngle = Vector3.Angle(Vector3.right, aimAngleVector);
		if( aimAngleVector.y < 0.0F ) aimAngle = 360.0F - aimAngle;
		if(aimAngleVector.magnitude > 0.0F)rigidbody2D.MoveRotation(aimAngle);
		
		// Firing
		isFiring = (Input.GetAxis ("Player1_Fire") > 0.1F);
		
	}
	
	public bool getIsFiring(){
		return isFiring;
	}
}
