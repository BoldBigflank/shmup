using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour {
	public static float gameTime;
	public TextAsset songData;

	public GameObject[] enemies;
	
//	List<GameObject> enemyPool;
	
	// Use this for initialization
	void Start () {
		gameTime = 0.0F;
//		enemyPool = new List<GameObject>();
	
		// Load the song beats in
		JSONObject j = new JSONObject(songData.ToString());
		
		// Get segments
		List<JSONObject> segments = j.GetField("segments").list;
		
//		JSONObject response = j.GetField("response");
//		JSONObject track = response.GetField("track");
//		JSONObject analysis = track.GetField("analysis");
//		beats = analysis.GetField("beats").list;
		
		// We're gonna load all the objects now!
		foreach(JSONObject segment in segments){
			// Instantiate an object
			GameObject o = NewObjectPoolerScript.current.Spawn("Dot");
			Debug.Log (segment);
//			o.SendMessage("SetData", segment);
			
			o.SetActive(true);
			// Give its start time
			// 
		}
		GetComponent<AudioSource>().time = -10.0F;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime = GetComponent<AudioSource>().time;
		// Read the segments list up to one second ahead of time
	}
}
