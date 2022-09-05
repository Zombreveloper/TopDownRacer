using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*Fills up unset Tiles in an area with a default Tile and deletes every tile that is outside the Area*/
public class TilemapFiller : MonoBehaviour
{
    public TileBase defaultTile;
    private Tilemap currentTilemap;
    private Camera mainCam;

    private Vector3 areaSize = new Vector3(400, 400);

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        currentTilemap = GameObject.Find("/Grid").GetComponentInChildren<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 upperLeft;
        Vector3 lowerRight;

        Vector3 camPos = mainCam.transform.position;
        //Debug.Log(camPos);
        createArea(camPos, out upperLeft, out lowerRight);
        boxFillOnTilemap(upperLeft, lowerRight);
        //Debug.Log(upperLeft);
    }

    void boxFillOnTilemapBackup(Vector3 upLeft, Vector3 downRight)
    {
        Vector3Int upLeftCell = currentTilemap.WorldToCell(upLeft);
        Vector3Int downRightCell = currentTilemap.WorldToCell(downRight);

        for (int ix =upLeftCell.x; ix < downRightCell.x; ix++)
        {
            for (int iy = downRightCell.y; iy < upLeftCell.y; iy++)
            {
                Vector3Int currentCell = new Vector3Int(ix, iy);
                if (!currentTilemap.HasTile(currentCell))
                {
                    currentTilemap.SetTile(currentCell, defaultTile);
                }
            }
        }
    }
    void boxFillOnTilemap(Vector3 upLeft, Vector3 downRight)
    {
        List<Vector3Int> areaCoordinates = cellCoordinatesInArea(upLeft, downRight);
        foreach (Vector3Int currentCell in areaCoordinates)
        {
            if (!currentTilemap.HasTile(currentCell))
            {
                currentTilemap.SetTile(currentCell, defaultTile);
            }
        }       
    }

    List<Vector3Int> cellCoordinatesInArea(Vector3 _upLeft, Vector3 _downRight)
    {
        Vector3Int upLeftCell = currentTilemap.WorldToCell(_upLeft);
        Vector3Int downRightCell = currentTilemap.WorldToCell(_downRight);
        List<Vector3Int> areaCoordinates = new List<Vector3Int>();

        for (int ix = upLeftCell.x; ix < downRightCell.x; ix++)
        {
            for (int iy = downRightCell.y; iy < upLeftCell.y; iy++)
            {
                Vector3Int currentCell = new Vector3Int(ix, iy);
                areaCoordinates.Add(currentCell);
            }
        }
        return areaCoordinates;
    }

    void createArea(Vector3 center, out Vector3 upperLeftCorner, out Vector3 lowerRightCorner)
    {
        float leftBorder = center.x - areaSize.x;
        float rightBorder = center.x + areaSize.x;
        float upperBorder = center.y + areaSize.y;
        float lowerBorder = center.y - areaSize.y;

        upperLeftCorner = new Vector3(leftBorder, upperBorder);
        lowerRightCorner = new Vector3(rightBorder, lowerBorder);
    }
}
