using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*instantiates Tilemaps onto the correct positions and creates Lists with every (Tilemap-) coordinate 
 * where an obstacle is allowed to spawn
 */

public class ObstacleAreaGenerator : MonoBehaviour
{
	//variables

	//referenced classes
	//TrackBuildMarker buildMarker;
	private RandomObstacleSpawner obstacleSpawner;

	//used GameObjects
	[Header("Obstacle Patterns")]
	[SerializeField] private GameObject straightObstacleMap;
	[SerializeField] private GameObject leftObstacleMap;
	[SerializeField] private GameObject rightObstacleMap;
	[Header("Obstacle Objects (only for testing!)")]
	[SerializeField] private GameObject placeholderObs;

	// Start is called before the first frame update
	void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void makeRandomObstacles(Tilemap _streetMap, TrackBuildMarker _buildMarker, string direction) //TODO: function name only for experimental purposes
	{
		GameObject currentObstacleMap = selectObstacleMap(direction);

		GameObject mapClone = Instantiate(currentObstacleMap, _streetMap.GetCellCenterWorld(_buildMarker.GetMarkerPos()), pointerRotation(_buildMarker), GameObject.FindGameObjectWithTag("TilemapGrid").transform);
		//rotateMap(mapClone, _buildMarker);
		Tilemap _map = mapClone.GetComponentInChildren<Tilemap>();
		_map.CompressBounds();

		List<Vector3Int> possiblePlaces = makePlacementArray(_map);
		foreach (Vector3Int validLocation in possiblePlaces)
		{
			int randomNumber = Random.Range(0, 15);
			if(randomNumber == 1)
			Instantiate(placeholderObs, _map.GetCellCenterWorld(validLocation), Quaternion.identity, mapClone.transform);

		}

		//rudimentary obstacle placement here!
		//RandomObstacleSpawner obstacleSpawner = FindObjectOfType<RandomObstacleSpawner>();
		//obstacleSpawner.spawnObjects();
	}

	Quaternion pointerRotation(TrackBuildMarker buildMarker)
	{
		Vector3 markerRotation = buildMarker.GetMarkerRot();
		Quaternion targetRotation = Quaternion.Euler(markerRotation);
		return targetRotation;
	}

	void rotateMap(Tilemap map, TrackBuildMarker buildMarker)
    {
		Vector3 markerRotation = buildMarker.GetMarkerRot();
		Quaternion targetRotation = Quaternion.Euler(markerRotation);
		map.transform.rotation = targetRotation;
    }

	List<Vector3Int> makePlacementArray(Tilemap map)
	{
		List<Vector3Int> validTiles = new List<Vector3Int>();
		//Tilemap _map = map.GetComponent<Tilemap>(); //maybe for overloading when giving a GameObject

		BoundsInt mapBounds = map.cellBounds;

		for (int i = mapBounds.y; i < mapBounds.yMax; i++) //Y-Axis
		{
			for (int j = mapBounds.x; j < mapBounds.xMax; j++) //X-Axis
			{
				Vector3Int searchPos = new Vector3Int(j, i);
				if (map.HasTile(searchPos))
				{
					//Instantiate(placeholderObs, _map.GetCellCenterWorld(searchPos), Quaternion.identity, _map.transform);
					//Debug.Log("There is a Tile on " + searchPos);
					validTiles.Add(searchPos);
				}
			}
		}
		Debug.Log("There are currently " + validTiles.Count + " possible places to spawn obstacles");
		return validTiles;
	}

	GameObject selectObstacleMap(string direction)
    {
		if (direction == "straightTrack")
			return straightObstacleMap; //.GetComponent<Tilemap>();
		else if (direction == "leftCurve")
			return leftObstacleMap; //.GetComponent<Tilemap>();
		else
			return rightObstacleMap; //.GetComponent<Tilemap>();
    }


}
