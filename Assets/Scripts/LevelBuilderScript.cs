using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplicationModel : MonoBehaviour
{
	static public string currentLevel = "";
}

public class LevelBuilderScript : MonoBehaviour {
	// Take a json map file, create the level from the map
	public GameObject[] levelObjects;
	GameObject levelGameObject;
	
	// Use this for initialization
	void Start () {
		levelGameObject = GameObject.FindGameObjectWithTag("Level");
		string path = ApplicationModel.currentLevel;
		if( path.Length == 0 ) path = "testmap";
		
		LoadLevelData(path);
	}
	
	void LoadLevelData(string path){
		TextAsset levelData = Resources.Load<TextAsset>(path);
		
		// Load the whole level
		JSONObject j = new JSONObject(levelData.ToString());
		
		// Get layers
		List<JSONObject> layers = j.GetField("layers").list;
		
		foreach(JSONObject layer in layers){
			Debug.Log(layer.GetField ("name").ToString ());
			if(layer.GetField ("name").str.Equals("Map")){
				Debug.Log ("Found Map");
				int numTilesX = (int)layer.GetField("width").n;
				
				// Place each object in data
				List<JSONObject> data = layer.GetField("data").list;
				for(int i = 0; i < data.Count; i++){
					int dataInt = (int)data[i].n-1;
					Debug.Log ("dataInt" + data[i].n);
					if(dataInt < 0 || dataInt > levelObjects.Length) continue;
					
					int xTileCoord = (i % numTilesX);
					int yTileCoord = (( data.Count - i ) / numTilesX);
					Debug.Log ("Creating " + dataInt + " at (" + xTileCoord.ToString () + ", " + yTileCoord.ToString () + ")");
					GameObject o = (GameObject)Instantiate(levelObjects[dataInt]);
					o.transform.position = TileToScene (xTileCoord, yTileCoord, 0);
					o.transform.parent = levelGameObject.transform;
				}
			} else if ( layer.GetField ("name").str.Equals("Treasure") ){
				
			} else {
				Debug.Log ("No Match" + layer.GetField("name").ToString());
			}
		}
		// Find the {"name":"Map"} layer
		// Use width/height and data to place objects in Level GameObject
		
		//		JSONObject response = j.GetField("response");
		//		JSONObject track = response.GetField("track");
		//		JSONObject analysis = track.GetField("analysis");
		//		beats = analysis.GetField("beats").list;
		
		
	}
	
	Vector2 TileToScene(int xCoord, int yCoord, int z){
		return new Vector3(xCoord - 15.5F, yCoord - 11.5F, z);
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
