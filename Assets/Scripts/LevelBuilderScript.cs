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
	public GameObject[] enemyObjects;
	public GameObject exitObject;
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
			SetObjectTransform(player1, player1_start);
		} else {
			Debug.Log ("player1_start not found");
		}
		
		// Get the enemies
		List<JSONObject> enemies = GetJSONObjectsWithType(objects, "Enemy");
		foreach(JSONObject enemy in enemies){
			 GameObject o = (GameObject)Instantiate (enemyObjects[0]);
			 SetObjectTransform(o, enemy);
		}
		
		// Get the exit
		JSONObject exit = GetJSONObjectWithName(objects, "Exit");
		GameObject e = (GameObject)Instantiate (exitObject);
		
		SetObjectTransform (e, exit);
		
	}
	
	void SetObjectTransform(GameObject g, JSONObject j){
		// If object has a rigidbody
		Vector2 coordSize = new Vector2(j.GetField ("width").n, j.GetField ("height").n);
		Vector2 pos = CoordToScene(new Vector2(j.GetField("x").n , j.GetField ("y").n ), coordSize);
		
		// Tiled's rotation starts at (0,1) and moves clockwise
		// Unity's rotation starts at (1,0) and moves counter
		float rot = -270.0F - j.GetField("rotation").n;
		
		if(!coordSize.Equals (Vector2.zero)){
			g.transform.localScale = new Vector3(coordSize.x, coordSize.y, tileWidth) / tileWidth;
		}
		if(g.rigidbody2D != null){
			g.rigidbody2D.position = pos;
			g.rigidbody2D.rotation = rot;
		} else {
			g.transform.position = new Vector3(pos.x, pos.y);
			g.transform.rotation = Quaternion.Euler(0.0F, 0.0F, rot);
		}

		
	}
	
	 JSONObject GetJSONObjectWithName(List<JSONObject> objects, string name){
		foreach (JSONObject o in objects){
			if(o.GetField("name").str.Equals(name)) return o;
		}
		return null;
	}
	
	List<JSONObject> GetJSONObjectsWithType(List<JSONObject> objects, string type){
		List<JSONObject> results = new List<JSONObject>();
		foreach(JSONObject o in objects){
			if(o.GetField ("type").str.Equals(type)){
				results.Add(o);
			}
		}
		return results;
	}
	
	Vector2 CoordToScene(Vector2 coord, Vector2 size){
		// Coords start at top left corner
		Vector2 tileCoord = coord / tileWidth;
		return TileToScene (tileCoord.x, tileCoord.y, size/tileWidth);
	}
	
	Vector2 TileToScene(float xCoord, float yCoord){
		return TileToScene (xCoord, yCoord, Vector2.one);
	}
	
	Vector2 TileToScene(float xCoord, float yCoord, Vector2 size){
		return new Vector2(xCoord - 16.0F + size.x/2.0F, 12.0F - (yCoord + size.y/2) );
	}
	
	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
