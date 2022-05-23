using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Based on the Video: Edit, Save And Load Unity Tilemaps at Runtime! https://www.youtube.com/watch?v=snUe2oa_iM0
*/

public class MapBuilder : MonoBehaviour
{
	[SerializeField] Tilemap currentTilemap;
	[SerializeField] TileBase currentTile;
	
	[SerializeField] Camera cam; //Only necessary for pointing with mouse.
	
	[SerializeField] Grid currentGrid;
	
	public GameObject straightTrackPrefab;
	
	//Vectors for positions on a grid to build on
	private Vector3Int buildPos;
	private Vector3 prefabSpawnPoint;


	private Tile _straightTrack;

	//private IEnumerator TilingDelay;
	
	
    // Start is called before the first frame update
    void Start()
    {

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
		int currentHeight;
		currentHeight = 1;

		Debug.Log("Started Coroutine at timestamp : " + Time.time);
		for (currentHeight = -1; currentHeight < 10; currentHeight++)
		{
			StraightLinePattern(currentHeight);
			yield return new WaitForSeconds(1);
		}

		Debug.Log("Ended Coroutine at timestamp : " + Time.time);
	}

	void StraightLinePattern(int posY)
	{
		for (int i = -1; i < 2; i++)
		{
			buildPos.Set(i, posY, 0);
			PlaceSingleTile(buildPos);
		}	
	}

	
	void PlaceSingleTile(Vector3Int pos) //places a tile on a given position on the grid
	{
		currentTilemap.SetTile(pos, currentTile);
	}

	
	void PlaceTileByMouse()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int pos = currentTilemap.WorldToCell(mousePosition);


		if (Input.GetMouseButton(0))
		{
			PlaceSingleTile(pos);
		}
	} //Click anywhere to place a given tile set by the currentTile


	void PlacePrefab(Vector3Int pos)
	{
		prefabSpawnPoint = currentGrid.CellToWorld(pos);
		GameObject newStraightTile = Instantiate(straightTrackPrefab, prefabSpawnPoint, Quaternion.identity) as GameObject;
		newStraightTile.transform.parent = currentTilemap.transform;
		//currentTilemap.SetTile(buildPos, straightTrackPrefab);
		//_straightTrack = Instantiate(straightTrackPrefab);
		//currentTilemap.SetTile(buildPos, _straightTrack);
	}//MOST LIKELY NOT EVER USED!!  Instanciates a Prefab on given positions on the grid
}
