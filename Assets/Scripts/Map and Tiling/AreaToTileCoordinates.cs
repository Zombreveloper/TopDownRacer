using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaToTileCoordinates : MonoBehaviour
{

    private Tilemap currentTilemap;
    //private Camera mainCam;

    private BoxCollider2D area;

    List<Vector3Int> areaTileCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        //mainCam = Camera.main;
        currentTilemap = GameObject.Find("/Grid").GetComponentInChildren<Tilemap>();

        area = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Bounds areaBounds = area.bounds;
        areaTileCoordinates = everyCellCoordinate();
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

    public List<Vector3Int> getCellCoordinates()
    {
        return areaTileCoordinates;
    }
}
