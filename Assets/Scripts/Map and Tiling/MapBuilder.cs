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

	//these are somewhere in the code but not really relevant anymore
	TileBase currentTile;


	[Header("Scene Camera")]
	[SerializeField] Camera cam; //Only necessary for pointing with mouse.

	[Header("Prefab Stuff (may not matter)")]
	[SerializeField] Grid currentGrid;	
	private GameObject straightTrackPrefab;
	
	//Vectors for positions on a grid to build on
	private Vector3Int buildPos;
	private Vector3 prefabSpawnPoint;

	//referenced classes
	private TrackBuildMarker buildMarker;
	private TilingPatterns tilingPatterns;
	private CheckpointPlacer checkpointPlacer;
	private ObstacleAreaGenerator obstacleArea;

	//Random Obstacle Spawning
	private RandomObstacleSpawner obstacleSpawner;


	// Start is called before the first frame update
	void Start()
    {
		//references to GameObjects
		buildMarker = FindObjectOfType<TrackBuildMarker>();
		tilingPatterns = FindObjectOfType<TilingPatterns>();
		checkpointPlacer = FindObjectOfType<CheckpointPlacer>();
		obstacleArea = GetComponentInChildren<ObstacleAreaGenerator>();
		obstacleSpawner = GameObject.Find("/MapManager/ObstacleManager").GetComponent<RandomObstacleSpawner>();

		//build initial Track pieces
		//StartCoroutine(TilingDelay());
		MakeStartArea();
		MakeLine();
		MakeLine();

	}	


	//Coroutine that waits for a given time to set a tile
	private IEnumerator TilingDelay()
	{

		Debug.Log("Started Coroutine at timestamp : " + Time.time);

		int infinityKeeper = 0;
		while (infinityKeeper == 0)
		{
			int randomNumber = Random.Range(0, 12); //Sets random int between 0 and 11
			//Debug.Log("The randomized Number is: " + randomNumber);
			if (randomNumber < 8)
			{
				MakeLine();
				yield return new WaitForSeconds(1);
			}
			else if (randomNumber >= 8 && randomNumber < 10)
			{
				MakeCurveRight();
				yield return new WaitForSeconds(1);
			}
			else
			{
				MakeCurveLeft();
				yield return new WaitForSeconds(1);
			}
		}

		//Method to build a Track myself
		/*for (int currentHeight = 0; currentHeight < 10; currentHeight++)
		{
			MakeLine();
			yield return new WaitForSeconds(1);
		}
		for (int currentHeight = 0; currentHeight < 2; currentHeight++)
		{
			MakeCurveLeft();
			yield return new WaitForSeconds(1);
		}
		for (int currentHeight = 0; currentHeight < 9; currentHeight++)
		{
			MakeLine();
			yield return new WaitForSeconds(1);
		}
		*/
		Debug.Log("Ended Coroutine at timestamp : " + Time.time);
	}

	public void BuildRandomPiece()
    {
		int randomNumber = Random.Range(0, 12); //Sets random int between 0 and 11
		//Debug.Log("The randomized Number is: " + randomNumber);
		if (randomNumber < 8)
		{
			MakeLine();
		}
		else if (randomNumber >= 8 && randomNumber < 10)
		{
			MakeCurveRight();
		}
		else
		{
			MakeCurveLeft();
		}
	}


	// Update is called once per frame
	void Update()
	{
		//PlaceTileByMouse();
	}

	void MakeStartArea()
	{
		//tilingPatterns.StraightLinePattern(); //calls for a straight line
		currentTilemap.SetTile(new Vector3Int(0, -1, 0), tilingPatterns.GetSingleTile(1));
		currentTilemap.SetTile(new Vector3Int(0, 0, 0), tilingPatterns.GetSingleTile(1));
		currentTilemap.SetTile(new Vector3Int(0, 11, 0), tilingPatterns.GetSingleTile(1));



	}

	void MakeLine()
    {
		string flag = "straightTrack";
		tilingPatterns.StraightLinePattern(); //calls for a straight line
		PlacePattern(tilingPatterns.GetPattern(), tilingPatterns.GetSprites(), flag); //draws the pattern onto the map, needs Positions + Sprites!
		buildMarker.StepForward(1); //could be called elsewhere?
	}

	
	void MakeCurveLeft()
	{
		string flag = "leftCurve";
		tilingPatterns.CurvedLeftTrackPattern(); //calls for a left curve
        PlacePattern(tilingPatterns.GetPattern(), tilingPatterns.GetSprites(), flag); //draws the pattern onto the map, needs Positions + Sprites!
		buildMarker.StepForward(1);
		buildMarker.RotateLeft();
		buildMarker.StepForward(2);
	}

	void MakeCurveRight()
	{
		string flag = "rightCurve";
		tilingPatterns.CurvedRightTrackPattern(); //calls for a right curve
		PlacePattern(tilingPatterns.GetPattern(), tilingPatterns.GetSprites(), flag ); //draws the pattern onto the map, needs Positions + Sprites!
		buildMarker.StepForward(1);
		buildMarker.RotateRight();
		buildMarker.StepForward(2);
	}

	void MirrorTilesInPattern(Vector3Int[] coordinates)
	{
		foreach (Vector3Int position in coordinates)
		{
			Quaternion newRotation = Quaternion.Euler(buildMarker.GetMarkerRot()); //der Quaternion aus der PointerRotation
			currentTilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, newRotation, new Vector3(-1, 1, 1))); //Überschreibt Rotation der Transformationsmatrix
		}
	}


	private void PlacePattern(List<Vector3Int> pattern, TileBase[] spriteArray, string direction) //set by TilingPatterns class! places a Pattern of tiles on a given position on the grid
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
		checkpointPlacer.PlaceCheckpoint(markerPos, markerRot);
		currentTilemap.SetTiles(gridCoordinates, spriteArray);
		RotateAllTilesInPattern(gridCoordinates);


		if (direction == "rightCurve")
        {
			MirrorTilesInPattern(gridCoordinates);

        }

		obstacleSpawner.makeRandomObstacles(currentTilemap, buildMarker, direction);
    }

    //Don´t ever lose this! function to rotate a tile!
    void RotateAllTilesInPattern(Vector3Int[] coordinates)
	{
		foreach (Vector3Int position in coordinates)
		{
			Quaternion newRotation = Quaternion.Euler(buildMarker.GetMarkerRot()); //der Quaternion aus der PointerRotation
			currentTilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, newRotation, Vector3.one)); //Überschreibt Rotation der Transformationsmatrix
		}
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

	
	void PlaceTileByMouse() //Click anywhere to place a given tile set by currentTile
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int pos = currentTilemap.WorldToCell(mousePosition);
		Debug.Log("I am called in MapBuilder");


		if (Input.GetMouseButton(1))
		{
			PlaceSingleTile(pos);
		}
	}


	//getters and setters 
	// (none currently)


	//here starts the trash dump! These methods will never be used but if compiler finds problems here
	//i did something wrong above!
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

	private void PlaceSingleTile(Vector3Int pos) //places a single current tile on a given position on the grid
	{
		currentTilemap.SetTile(pos, currentTile);
	}
}
