using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplicationModel : MonoBehaviour
{
	static public string currentLevel = "";
}

public class LevelBuilderScript : MonoBehaviour {
	float tileWidth;

	// Take a json map file, create the level from the map
	public GameObject[] levelObjects;
	GameObject levelGameObject;
	GameObject player1;
	
	// Use this for initialization
	void Start () {
		levelGameObject = GameObject.FindGameObjectWithTag("Level");
		player1 = GameObject.FindGameObjectWithTag("Player");
		string path = ApplicationModel.currentLevel;
		if( path.Length == 0 ) path = "testmap";
		
		LoadLevelData(path);
	}
	
	void LoadLevelData(string path){
		TextAsset levelData = Resources.Load<TextAsset>(path);
		
		// Load the whole level
		JSONObject j = new JSONObject(levelData.ToString());
		
		// Get the tile width
		tileWidth = j.GetField("tilewidth").n;
		
		// Get layers
		List<JSONObject> layers = j.GetField("layers").list;
		
		foreach(JSONObject layer in layers){
			if(layer.GetField ("name").str.Equals("Map")){
				LoadMap (layer);
			} else if ( layer.GetField ("name").str.Equals("Objects") ){
				LoadObjects (layer.GetField("objects").list);
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
	
	void LoadMap(JSONObject map){
		int numTilesX = (int)map.GetField("width").n;
		
		// Place each object in data
		List<JSONObject> data = map.GetField("data").list;
		for(int i = 0; i < data.Count; i++){
			int dataInt = (int)data[i].n-1;
			Debug.Log ("dataInt" + data[i].n);
			if(dataInt < 0 || dataInt > levelObjects.Length) continue;
			
			int xTileCoord = (i % numTilesX);
//			int yTileCoord = (( data.Count - i - 1 ) / numTilesX);
			int yTileCoord = (i / numTilesX);
			GameObject o = (GameObject)Instantiate(levelObjects[dataInt]);
			Vector2 oPos = TileToScene (xTileCoord, yTileCoord);
			o.transform.position = new Vector3 (oPos.x, oPos.y);
			o.transform.parent = levelGameObject.transform;
		}
	}
	
	void LoadObjects(List<JSONObject> objects){
		// Only load the objects we're expecting
		// This means look for specific objects/groups and translate them
		
		// Get Player1_start
		JSONObject player1_start = GetJSONObjectWithName(objects, "Player1_start");
		if(player1_start != null){
			Debug.Log("player1_start" + player1_start);
			player1.rigidbody2D.position = CoordToScene(new Vector2(player1_start.GetField("x").n , player1_start.GetField ("y").n ));
			player1.rigidbody2D.rotation = player1_start.GetField("rotation").n;
		} else {
			Debug.Log ("player1_start not found");
		}
		
	}
	
	 JSONObject GetJSONObjectWithName(List<JSONObject> objects, string name){
		foreach (JSONObject o in objects){
			if(o.GetField("name").str.Equals(name)) return o;
		}
		return null;
	}
	
	Vector2 CoordToScene(Vector2 coord){
		// Coords start at top left corner
		Vector2 tileCoord = coord / tileWidth;
		return TileToScene (tileCoord.x, tileCoord.y);
	}
	
	Vector2 TileToScene(float xCoord, float yCoord){
		return new Vector2(xCoord - 15.5F, 11.5F - yCoord);
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
