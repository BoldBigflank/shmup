using UnityEngine;
using System.Collections;

public class DotScript : MonoBehaviour {
	public float speed = 15.0F;
	public GameObject source;
	public float life = 2.0F;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable(){
		if(life > 0.0F) Invoke ("Destroy", life);
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
		if(other.gameObject == source) return;
		
		if(other.gameObject.CompareTag("Enemy")){
			Debug.Log ("Bullet hit ");
			other.gameObject.SetActive(false);
		}
		
		CancelInvoke();
		gameObject.SetActive(false);
		
	}
}
