using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	Vector2 lastKnownTargetPosition;
	public LayerMask layerMask;
    public float turnSpeed = 20.0F;
    public float moveSpeed = 8.0F;

    // My redo rotation/forward motion variables
    float currentAngle;
    float targetAngle;

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

            //Debug.DrawRay(transform.position, (playerObject.transform.position - transform.position), Color.red);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerObject.transform.position - transform.position) , distance * 2.0F, layerMask);
			if (hit.collider != null) {
                //Debug.DrawLine(hit.point-Vector2.one, hit.point+Vector2.one, Color.white);
				// Found something
				if(hit.collider.gameObject.CompareTag("Player")){
                    //Debug.Log ("Found the player");
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
        
        // Set the target angle
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentAngle = currentRotation.z; // Vector2.Angle(Vector2.right, currentRotation.z);
        targetAngle = SignedAngleBetween(Vector2.right, (lastKnownTargetPosition - (Vector2)transform.position));

        // Make targetAngle within 180 degrees of the current angle
        while (targetAngle < currentAngle - 180.0F) targetAngle += 360.0F;
        while (targetAngle > currentAngle + 180.0F) targetAngle -= 360.0F;

        // Turn as hard as you can until you're there
        float angleDelta = targetAngle - currentAngle;
        float direction = (angleDelta < 0.0F) ? -1.0F : 1.0F;
        float newAngle = currentAngle + Mathf.Min(Mathf.Abs(angleDelta), turnSpeed * Time.deltaTime) * direction;

        // Force the rotation
        rigidbody2D.rotation = newAngle;
        // Move if possible
        rigidbody2D.MovePosition(transform.position + (Vector3)(AngleToVector(newAngle) * moveSpeed * Time.deltaTime));
	}
	
	void OnCollisionEnter2D(Collision2D other){
		//		Debug.Log ("onCollision");
//		if(other.gameObject.CompareTag("Bullet"))
//			gameObject.SetActive(false);
		
	}

    float SignedAngleBetween(Vector2 a, Vector2 b)
    {
        // angle in [0,180]
        float angle = Vector2.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(Vector3.forward, Vector3.Cross(a, b)));

        // angle in [-179,180]
        float signed_angle = angle * sign;

        // angle in [0,360] (not used but included here for completeness)
        //float angle360 =  (signed_angle + 180) % 360;

        return signed_angle;
    }

    Vector2 AngleToVector(float angleDegrees)
    {
        float angle = Mathf.Deg2Rad * angleDegrees;
        Vector2 v = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return v.normalized;
    }
}
