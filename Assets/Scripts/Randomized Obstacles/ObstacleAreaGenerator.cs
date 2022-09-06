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
	private Queue<GameObject> obstacleMapPool = new Queue<GameObject>();
	[SerializeField] int maxObstacleMaps = 4;
	[SerializeField] int waitBuildingObstacles = 1; //amount of checkpoints that must be passed before obstacles are set
	int delayIndex = -1;

	//referenced classes
	private RandomObstacleSpawner obstacleSpawner;

	//used GameObjects
	[Header("Obstacle Patterns")]
	[SerializeField] private GameObject straightObstacleMap;
	[SerializeField] private GameObject leftObstacleMap;
	[SerializeField] private GameObject rightObstacleMap;
	[Header("Obstacle Objects (only for testing!)")]
	[SerializeField] private GameObject placeholderObs;

	// Start is called before the first frame update
	void Awake()
    {
		obstacleSpawner = FindObjectOfType<RandomObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void makeRandomObstacles(Tilemap _streetMap, TrackBuildMarker _buildMarker, string direction) //TODO: function name only for experimental purposes
	{
		if (delayIndex < waitBuildingObstacles)
        {
			delayIndex++;
			return;
        }

		Tilemap map = setObstacleMap(_streetMap, _buildMarker, direction);
		
		List<Vector3Int> possiblePlaces = makePlacementArray(map);
		int listIndex = 0;
		foreach (Vector3Int validLocation in possiblePlaces)
		{
			//TODO: jede Location soll bei einer Kurve jetzt zusätzlich zur Map Rotation eine zusätzliche interpolierte Rotation von 0 zu +-90 Grad bekommen
			listIndex++;
			Quaternion gradualRotation = rotateWithCurve(direction, possiblePlaces, listIndex, _buildMarker);
			int randomNumber = Random.Range(0, 15);
			if(randomNumber == 0)
            {
				GameObject randomObstacle = obstacleSpawner.chooseRandomObject();
				//GameObject randomObstacle = placeholderObs;
				Instantiate(randomObstacle, map.GetCellCenterWorld(validLocation), pointerRotation(_buildMarker) * gradualRotation, map.transform.parent.transform);
				//Instantiate(randomObstacle, map.GetCellCenterWorld(validLocation), gradualRotation, map.transform.parent.transform); //for testing, only the additional Rotation gets set 
			}		
		}

		checkDestroyObstacles();		
	}

	Quaternion rotateWithCurve(string _direction, List<Vector3Int> _possiblePlaces, int _listIndex, TrackBuildMarker buildMarker) //for every spot in a curve the iterator goes through the rotation gets a little wider
    {
		float endRotation = 0;
		if (_direction == "leftCurve")
		{
			endRotation = 90;
		}
		else if (_direction == "rightCurve")
        {
			endRotation = -90;
		}
		float fListIndex = (float)_listIndex;
		float fListLength = (float)_possiblePlaces.Count; //cast integer to float. otherwise division will only result in whole numbers

		float interpolator = fListIndex / fListLength;
		float startRotation = buildMarker.GetMarkerRot().z;
		float lipoResult = Mathf.LerpAngle(0f, endRotation,interpolator);
		Debug.Log("index is " + _listIndex + " and the list has " + _possiblePlaces.Count + " entries");
		Debug.Log(lipoResult);
		Quaternion additionalRotation = Quaternion.Euler(0, 0, lipoResult);
		return additionalRotation;
    }

	Tilemap setObstacleMap(Tilemap _street, TrackBuildMarker _buildMarker, string _direction)
    {
		GameObject currentObstacleMap = selectObstacleMap(_direction);

		GameObject mapClone = Instantiate(currentObstacleMap, _street.GetCellCenterWorld(_buildMarker.GetMarkerPos()), pointerRotation(_buildMarker), GameObject.FindGameObjectWithTag("TilemapGrid").transform);
		obstacleMapPool.Enqueue(mapClone);
		//rotateMap(mapClone, _buildMarker);
		Tilemap _map = mapClone.GetComponentInChildren<Tilemap>();
		_map.CompressBounds();

		return _map;
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
		//Tilemap _map = map.GetComponent<Tilemap>(); //maybe for overloading when given a GameObject instead of tilemap

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

	void checkDestroyObstacles()
    {
		if (obstacleMapPool.Count > maxObstacleMaps)
		{
			Destroy(obstacleMapPool.Dequeue());
		}
	}


}
