using UnityEngine;
using System.Collections;

public class ScreenBorderScript : MonoBehaviour {
	public GameObject borderCollider;
	
	float mapX = 100.0F;
	float mapY = 100.0F;
	
	float minX;
	float maxX;
	float minY;
	float maxY;
	
	void Start() {
		float vertExtent = Camera.main.camera.orthographicSize;
		float horzExtent = vertExtent * Screen.width / Screen.height;
		
		Debug.Log (vertExtent + " " + horzExtent);
		// Calculations assume map is position at the origin
		minX = horzExtent - mapX / 2.0F;
		maxX = mapX / 2.0F - horzExtent;
		minY = vertExtent - mapY / 2.0F;
		maxY = mapY / 2.0F - vertExtent;
	}
	
	void LateUpdate() {
		var v3 = transform.position;
		v3.x = Mathf.Clamp(v3.x, minX, maxX);
		v3.y = Mathf.Clamp(v3.y, minY, maxY);
		transform.position = v3;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
