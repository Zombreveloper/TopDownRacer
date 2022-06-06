using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Based on the Video: Edit, Save And Load Unity Tilemaps at Runtime! https://www.youtube.com/watch?v=snUe2oa_iM0
*/

public class MapBuilder : MonoBehaviour
{
	[Header("Selected Tiles")]
	[SerializeField] Tilemap currentTilemap;
	[SerializeField] TileBase TileStraightVert; //Ruletiles for Straight Track in Vertival Direction
	[SerializeField] TileBase TileCurveLD; //Ruletiles for curved Track Left to Down
	TileBase currentTile;

	[Header("Scene Camera")]
	[SerializeField] Camera cam; //Only necessary for pointing with mouse.

	[Header("Prefab Stuff (may not matter)")]
	[SerializeField] Grid currentGrid;	
	public GameObject straightTrackPrefab;
	
	//Vectors for positions on a grid to build on
	private Vector3Int buildPos;
	private Vector3 prefabSpawnPoint;

	//referenced classes
	private TrackBuildMarker buildMarker;
	private TilingPatterns tilingPatterns;
	
	

    // Start is called before the first frame update
    void Start()
    {
		buildMarker = FindObjectOfType<TrackBuildMarker>();
		tilingPatterns = FindObjectOfType<TilingPatterns>();
		StartCoroutine(TilingDelay());

	}




	// Update is called once per frame
	void Update()
    {
		PlaceTileByMouse();
	}

	//Coroutine that waits for a given time to set a tile
	private IEnumerator TilingDelay()
	{
		//int currentHeight;
		//Vector3Int currentBuildPos = buildMarker.GetMarkerPos();
		//currentHeight = 0;

		Debug.Log("Started Coroutine at timestamp : " + Time.time);
		for (int currentHeight = 0; currentHeight < 10; currentHeight++)
		{
			MakeStraightLine();
			yield return new WaitForSeconds(1);
		}
		for (int currentHeight = 0; currentHeight < 1; currentHeight++)
		{
			MakeCurvedLine();
			yield return new WaitForSeconds(1);
		}
		MakeStraightLine();

		Debug.Log("Ended Coroutine at timestamp : " + Time.time);
	}

	void MakeStraightLine()
    {
		currentTile = TileStraightVert;
		tilingPatterns.StraightLinePattern(); //calls for a straight line
		PlacePattern(tilingPatterns.GetPattern()); //draws the pattern onto the map
		buildMarker.MoveUp(1); //could be called elsewhere?
	}

	void MakeCurvedLine()
	{
		currentTile = TileCurveLD;
		tilingPatterns.CurvedTrackPattern(); //calls for a straight line
		PlacePattern(tilingPatterns.GetPattern()); //draws the pattern onto the map
		buildMarker.MoveLeft(3); //could be called elsewhere?
	}

		
	

	private void PlacePattern(List<Vector3Int> pattern) //set by TilingPatterns class! places a Pattern of tiles on a given position on the grid
	{
		Vector3Int markerPos = getMarkerPos();
		Vector3 markerRot = getMarkerRot();


		foreach (Vector3Int coordinate in pattern)
		{
			Vector3Int gridCoordinate = markerPos + coordinate; //transforms relative coordinate to coordinate on Grid
			PlaceSingleTile(gridCoordinate);
		}
	}
	private void PlaceSingleTile(Vector3Int pos) //places a single current tile on a given position on the grid
	{
		currentTilemap.SetTile(pos, currentTile);
	}

	
	void PlaceTileByMouse() //Click anywhere to place a given tile set by currentTile
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int pos = currentTilemap.WorldToCell(mousePosition);


		if (Input.GetMouseButton(1))
		{
			PlaceSingleTile(pos);
		}
	}

	//getters and setters
	Vector3Int getMarkerPos()
	{
		return buildMarker.GetMarkerPos();
	}

	Vector3 getMarkerRot()
	{
		return buildMarker.GetMarkerRot();
	}

	//here starts the trash dump! These methods will never be used but if compiler finds problems here
	//i did something wrong higher up!
	void PlacePrefab(Vector3Int pos)
	{
		prefabSpawnPoint = currentGrid.CellToWorld(pos);
		GameObject newStraightTile = Instantiate(straightTrackPrefab, prefabSpawnPoint, Quaternion.identity) as GameObject;
		newStraightTile.transform.parent = currentTilemap.transform;
	}//MOST LIKELY NOT EVER USED!! (but functional)  Instanciates a Prefab on given positions on the grid

	void StraightLinePattern(int posY) //this is just here to fall back onto if something brakes!
	{
		for (int i = -1; i < 2; i++)
		{
			buildPos.Set(i, posY, 0);
			PlaceSingleTile(buildPos);
		}	
	}
}
