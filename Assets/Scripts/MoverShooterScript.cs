using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoverShooterScript : MonoBehaviour {
	// Move into position during the delay
	// Grow to double until time_loudness_max
	// Disappear at end
	float start;
	float duration;
	float time_loudness_max;
	
	Vector3 baseScale;
	Vector3 grownScale;
	
	public Material shadowMaterial;
	public Material mainMaterial;
	
	void Start () {
		
	}
	
	public void SetData(JSONObject segment){
		start = segment.GetField ("start").n;
		duration = segment.GetField("duration").n;
		time_loudness_max = segment.GetField ("time_loudness_max").n;
		
		// Translate timbre to size
		int dominantTimbreIndex = 0;
		List<JSONObject> timbre = segment.GetField("timbre").list;
		for(int i = 1; i < timbre.Count; i++){
			if(timbre[i].n < timbre[i-1].n) dominantTimbreIndex = i;
		}
		baseScale = (15 - dominantTimbreIndex) * Vector3.one;
		baseScale.y = 1.0F;
		grownScale = (6 + 15 - dominantTimbreIndex) * Vector3.one;
		grownScale.y = 1.0F;
		
		// Translate pitch to Z value for now
//		int dominantPitchIndex = 0;
//		List<JSONObject> pitches = segment.GetField ("pitches").list;
//		for(int j = 1; j < pitches.Count; j++){
//			if(pitches[j].n < pitches[j-1].n) dominantPitchIndex = j;
//		}
		transform.position = new Vector3(Random.Range (-21, 21), 0.0F, Random.Range (-16, 16));
	}
	
	// Update is called once per frame
	void Update () {
		float gameTime = GameControllerScript.gameTime;
		float localGameTime = gameTime - start;
		if(localGameTime < 0.0F){
			transform.localScale = Vector3.zero;
			
		}
		if(localGameTime > -1.0F && localGameTime < 0.0F){
			
			renderer.material = shadowMaterial;
			transform.localScale = grownScale;
		}
		
		if( localGameTime > 0.0F ){
			renderer.material = mainMaterial;
//			gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			
			// Set the size
			if(localGameTime < time_loudness_max){
				transform.localScale = Vector3.Lerp(baseScale, grownScale, localGameTime / time_loudness_max );
			}
			else
				transform.localScale = Vector3.Lerp(grownScale, Vector3.zero, (localGameTime-time_loudness_max) / (duration-time_loudness_max));
			if(localGameTime > duration){
				transform.localScale = Vector3.zero;
				gameObject.SetActive(false);
			}
			
		}
	
	}
}
