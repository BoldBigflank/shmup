using UnityEngine;
using System.Collections;

public class BorderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other){
		Debug.Log (other.tag + " entered Border");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
