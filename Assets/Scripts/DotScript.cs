using UnityEngine;
using System.Collections;

public class DotScript : MonoBehaviour {
	public float speed = 15.0F;
	public GameObject source;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable(){
		Invoke ("Destroy", 2.0F);
	}
	
	void Destroy(){
		gameObject.SetActive(false);
	}
	
	void OnDisable(){
		CancelInvoke();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, speed*Time.deltaTime, 0);
	}
	
	void onCollision(Collider other){
		if(other.gameObject == source) return;
		
	}
}
