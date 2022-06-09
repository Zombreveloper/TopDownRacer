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

	[Header("Tiles without Rules")]
	[SerializeField] TileBase GrassTile;
	[SerializeField] TileBase StraightTile;
	[SerializeField] TileBase CurveTileUL;
	[SerializeField] TileBase CurveTileUR;
	[SerializeField] TileBase CurveTileDL;
	[SerializeField] TileBase CurveTileDR;


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





	//Coroutine that waits for a given time to set a tile
	private IEnumerator TilingDelay()
	{

		Debug.Log("Started Coroutine at timestamp : " + Time.time);
		for (int currentHeight = 0; currentHeight < 10; currentHeight++)
		{
			MakeLine();
			yield return new WaitForSeconds(1);
		}
		for (int currentHeight = 0; currentHeight < 2; currentHeight++)
		{
			MakeCurvedLine();
			yield return new WaitForSeconds(1);
		}
		for (int currentHeight = 0; currentHeight < 9; currentHeight++)
		{
			MakeLine();
			yield return new WaitForSeconds(1);
		}

		Debug.Log("Ended Coroutine at timestamp : " + Time.time);
	}

	// Update is called once per frame
	void Update()
	{
		PlaceTileByMouse();
	}

	void MakeLine()
    {
		currentTile = TileStraightVert;
		tilingPatterns.StraightLinePattern(); //calls for a straight line
		PlacePattern(tilingPatterns.GetPattern(), tilingPatterns.GetSprites()); //draws the pattern onto the map, needs Positions + Sprites!
		buildMarker.StepForward(1); //could be called elsewhere?
	}

	
	void MakeCurvedLine()
	{
		currentTile = TileCurveLD;
		tilingPatterns.CurvedTrackPattern(); //calls for a straight line
        PlacePattern(tilingPatterns.GetPattern(), tilingPatterns.GetSprites()); //draws the pattern onto the map, needs Positions + Sprites!
		buildMarker.StepForward(1);
		buildMarker.RotateLeft();
		buildMarker.StepForward(2);
	}

		
	

	private void PlacePattern(List<Vector3Int> pattern, TileBase[] spriteArray) //set by TilingPatterns class! places a Pattern of tiles on a given position on the grid
	{	
		Vector3Int markerPos = buildMarker.GetMarkerPos();
		Vector3 markerRot = buildMarker.GetMarkerRot();
		Vector3Int[] currentPattern = pattern.ToArray();
		Vector3Int[] gridCoordinates = new Vector3Int[currentPattern.Length];

		int i = 0;
		foreach (Vector3Int coordinate in currentPattern)
		{
			Vector3Int rotatedCoordinate = RotatePattern(coordinate); //Vllt umbenennen und umsiedeln. Wendet die Rotation des Marker auf das Pattern an
			Vector3Int gridCoordinate = markerPos + rotatedCoordinate; //transforms relative coordinate to coordinate on Grid
			gridCoordinates[i] = gridCoordinate;
			i++;
			//PlaceSingleTile(gridCoordinate);
		}
		currentTilemap.SetTiles(gridCoordinates, spriteArray);
	}

	Vector3Int RotatePattern(Vector3 coordinate) 
	{
		Vector3 markerRotation = buildMarker.GetMarkerRot(); //transforms MarkerRotation from Vector3Int to Vector3 in the process
		Quaternion rotation = Quaternion.Euler(markerRotation);
		Matrix4x4 rotMatrix = Matrix4x4.Rotate(rotation);
		Vector3 rotatedVector = rotMatrix.MultiplyPoint3x4(coordinate);
		Vector3Int rotatedIntVector = Vector3Int.RoundToInt(rotatedVector);
		return rotatedIntVector;
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
