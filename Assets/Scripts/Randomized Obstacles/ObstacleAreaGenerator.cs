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

	//used GameObjects
	[Header("Obstacle Patterns")]
	[SerializeField] private GameObject straightObstacleMap;
	[SerializeField] private GameObject leftObstacleMap;
	[SerializeField] private GameObject rightObstacleMap;
	[Header("Obstacle Objects (only for testing!)")]
	[SerializeField] private GameObject placeholderObs;


	public Tilemap setObstacleMap(Tilemap _street, TrackBuildMarker _buildMarker, string _direction)
    {
		GameObject currentObstacleMap = selectObstacleMap(_direction);

		GameObject mapClone = Instantiate(currentObstacleMap, _street.GetCellCenterWorld(_buildMarker.GetMarkerPos()), _buildMarker.getRotationQuaternion(), GameObject.FindGameObjectWithTag("TilemapGrid").transform);
		obstacleMapPool.Enqueue(mapClone);
		//rotateMap(mapClone, _buildMarker);
		Tilemap _map = mapClone.GetComponentInChildren<Tilemap>();
		_map.CompressBounds();

		return _map;
	}


	void rotateMap(Tilemap map, TrackBuildMarker buildMarker)
    {
		Vector3 markerRotation = buildMarker.GetMarkerRot();
		Quaternion targetRotation = Quaternion.Euler(markerRotation);
		map.transform.rotation = targetRotation;
    }

	public List<Vector3Int> makePlacementArray(Tilemap map)
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

	public void checkDestroyObstacleMap()
    {
		if (obstacleMapPool.Count > maxObstacleMaps)
		{
			Destroy(obstacleMapPool.Dequeue());
		}
	}


}
