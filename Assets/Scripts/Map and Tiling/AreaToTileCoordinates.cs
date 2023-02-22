using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* converts the Bounds of an Object to a List of cell coordinates of a given tilemap
 */
public class AreaToTileCoordinates : MonoBehaviour
{
    private Camera mainCam;
    private Tilemap currentTilemap;
    //private Camera mainCam;

    private BoxCollider2D area;

    List<Vector3Int> areaTileCoordinates;
    List<Vector3Int> allTileCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        currentTilemap = GameObject.Find("/Grid").GetComponentInChildren<Tilemap>();
        area = GetComponent<BoxCollider2D>();

        Bounds areaBounds = area.bounds;
        areaTileCoordinates = cellCoordinatesAroundCam();
    }

    // Update is called once per frame
    void Update()
    {
        Bounds areaBounds = area.bounds;
        //areaTileCoordinates = everyCellCoordinate();
        areaTileCoordinates = cellCoordinatesAroundCam();
        allTileCoordinates = everyCellCoordinate();
    }

    List<Vector3Int> everyCellCoordinate()
    {
        Bounds areaBounds = area.bounds;
        BoundsInt tilemapBounds = currentTilemap.cellBounds;
        List<Vector3Int> areaCoordinates = new List<Vector3Int>();

        for (int ix = tilemapBounds.xMin; ix < tilemapBounds.xMax; ix++)
        {
            for (int iy = tilemapBounds.yMin; iy < tilemapBounds.yMax; iy++)
            {
                Vector3Int cellVector = new Vector3Int(ix, iy);
                areaCoordinates.Add(cellVector);
            }
        }
        return areaCoordinates;
    }

    List<Vector3Int> cellCoordinatesAroundCam()
    {
        Vector3Int camCellPos = currentTilemap.WorldToCell(mainCam.transform.position);
        List<Vector3Int> areaCoordinates = new List<Vector3Int>();


        for (int ix = camCellPos.x - 10; ix < camCellPos.x + 10; ix++)
        {
            for (int iy = camCellPos.y - 10; iy < camCellPos.y + 10; iy++)
            {
                Vector3Int cellVector = new Vector3Int(ix, iy);
                areaCoordinates.Add(cellVector);
            }
        }
        return areaCoordinates;
    }

    public List<Vector3Int> getCellCoordinatesAroundCam()
    {
        return areaTileCoordinates;
    }

    public List<Vector3Int> getAllCellCoordinates()
    {
        return allTileCoordinates;
    }
     
}
