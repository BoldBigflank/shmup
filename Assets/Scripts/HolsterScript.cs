using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HolsterScript : MonoBehaviour {
	int activeGunIndex = 0;

	[SerializeField]
	List<ShooterScript> availableGuns;

	void Start () {
		foreach(ShooterScript s in availableGuns){
			s.enabled = false;
		}
		if(availableGuns.Count > 0){
			activeGunIndex = 0;
			availableGuns[activeGunIndex].enabled = true;
		}
	}

	// Receive input from the PlayerInputScript
	void ReceivedInput(string button){
		if(button=="Y") nextWeapon ();
	}

	void nextWeapon(){
		availableGuns[activeGunIndex].enabled = false;
		activeGunIndex = (activeGunIndex+1) % availableGuns.Count;
		availableGuns[activeGunIndex].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.C) == true){
			Debug.Log("Key Down" + activeGunIndex);
			nextWeapon();
		}
	}
}
