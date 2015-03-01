using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	Vector2 lastKnownTargetPosition;
	public LayerMask layerMask;
    public float RotationSpeed;

    Quaternion lookRotation;
    Vector3 direction;

	// Use this for initialization
	void Start () {
        lastKnownTargetPosition = (Vector2)transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// Cast a ray at each Player
		float distance = 30.0F;

		foreach(GameObject playerObject in GameObject.FindGameObjectsWithTag ("Player")){
//			int layerMask = LayerMask.NameToLayer("Foreground") | LayerMask.NameToLayer("Player");

            Debug.DrawRay(transform.position, (playerObject.transform.position - transform.position), Color.red);
			// RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(playerObject.transform.position-transform.position), distance*2.0F, layerMask);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerObject.transform.position - transform.position) , distance * 2.0F, layerMask);
			if (hit.collider != null) {
				Debug.Log ("Found something " + hit.collider.tag);
				Debug.DrawLine(hit.point-Vector2.one, hit.point+Vector2.one, Color.white);
//				Debug.DrawLine(hit.point-Vector2.one, hit.point+Vector2.one, Color.white);
				// Found something
				if(hit.collider.gameObject.CompareTag("Player")){
					Debug.Log ("Found the player");
					lastKnownTargetPosition = hit.point;

                    break;
				}
			}
		}
	}

    void FixedUpdate()
    {
        GoToLastKnownPosition();
    }

	void GoToLastKnownPosition(){
		// If we're close enough, return
        if (((Vector2)transform.position - lastKnownTargetPosition).magnitude < 1.0F) { rigidbody2D.velocity = Vector2.zero; return; };
//		transform.LookAt(lastKnownTargetPosition);
        direction = (lastKnownTargetPosition - (Vector2)transform.position).normalized;

        lookRotation = Quaternion.LookRotation(direction);

		// Rotate toward the point
        Quaternion q = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        rigidbody2D.MoveRotation(q.eulerAngles.z);
		// Move forward
        // rigidbody2D.AddRelativeForce(Vector3.left, ForceMode2D.Impulse);
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(direction * 8.0F, ForceMode2D.Impulse);
	}
	
	void OnCollisionEnter2D(Collision2D other){
		//		Debug.Log ("onCollision");
//		if(other.gameObject.CompareTag("Bullet"))
//			gameObject.SetActive(false);
		
	}
}
