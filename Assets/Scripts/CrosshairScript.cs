using UnityEngine;
using System.Collections;

public class CrosshairScript : MonoBehaviour {
	GameObject player;
	
	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Position it in front of the Player
		// Cast a ray
		float distance = 3.0F;
		int layerMask = LayerMask.NameToLayer("Foreground");
        
		RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.transform.TransformDirection(Vector2.right), distance*2, layerMask);
		if (hit.collider != null) {
			transform.position = hit.point;
		} else {
			transform.localPosition = distance * Vector2.right;
		}
	}
}
