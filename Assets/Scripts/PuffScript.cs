using UnityEngine;
using System.Collections;

public class PuffScript : MonoBehaviour {
	public float speed = 2.0F;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable(){
//		if(life > 0.0F) Invoke ("Destroy", life);
//		rigidbody2D.AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
//		transform.localScale = 1.0F;
	}
	
	void Destroy(){
		gameObject.SetActive(false);
	}
	
	void OnDisable(){
		CancelInvoke();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, speed * Time.deltaTime);
	}
}
