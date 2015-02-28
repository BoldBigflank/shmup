using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	Vector2 lastKnowntargetPosition;
	public LayerMask layerMask;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Cast a ray at each Player
		float distance = 30.0F;

		foreach(GameObject playerObject in GameObject.FindGameObjectsWithTag ("Player")){
//			int layerMask = LayerMask.NameToLayer("Foreground") | LayerMask.NameToLayer("Player");

			Debug.DrawRay(transform.position, transform.TransformDirection(playerObject.transform.position-transform.position), Color.red);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(playerObject.transform.position-transform.position), distance*2.0F, layerMask);
			if (hit.collider != null) {
				Debug.Log ("Found something " + hit.collider.tag);
				Debug.DrawLine(hit.point-Vector2.one, hit.point+Vector2.one, Color.white);
//				Debug.DrawLine(hit.point-Vector2.one, hit.point+Vector2.one, Color.white);
				// Found something
				if(hit.collider.gameObject.CompareTag("Player")){
					Debug.Log ("Found the player");
					lastKnowntargetPosition = hit.point;

				}
//				transform.position = hit.point;
			}
		}

		GoToLastKnownPosition();
	}

	void GoToLastKnownPosition(){
		// If we're close enough, return
//		transform.LookAt(lastKnowntargetPosition);

		// Rotate toward the point

		// Move forward

	}
	
	void OnCollisionEnter2D(Collision2D other){
		//		Debug.Log ("onCollision");
//		if(other.gameObject.CompareTag("Bullet"))
//			gameObject.SetActive(false);
		
	}
}
