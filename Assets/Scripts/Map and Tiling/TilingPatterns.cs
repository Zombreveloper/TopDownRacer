using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class TilingPatterns : MonoBehaviour
{
    //variables
    List<Vector3Int> savedCoordinatesList;
    TileBase[] spriteArray;

    [Header("Tiles to build with")]
    [SerializeField] TileBase GrassTile;
    [SerializeField] TileBase StraightTile;
    [SerializeField] TileBase CurveTileUL;
    [SerializeField] TileBase CurveTileUR;
    [SerializeField] TileBase CurveTileDL;
    [SerializeField] TileBase CurveTileDR;

    //for dynamic tilesets
    [SerializeField] TilePoolList_SO tileset;

    //referenced classes

    // Start is called before the first frame update
    void Awake()
    {
        if (tileset != null)
        {
            GrassTile = tileset.tiles[0];
            StraightTile = tileset.tiles[1];
            CurveTileUL = tileset.tiles[2];
            CurveTileUR = tileset.tiles[3];
            CurveTileDL = tileset.tiles[4];
            CurveTileDR = tileset.tiles[5];
        }
    }

    public void StraightLinePattern() //Builds List of coordinates for tiles in relation to each other
    {
        Vector3Int buildPos = new Vector3Int(0,0,0);
        List<Vector3Int> tileCoordinates = new List<Vector3Int>();

        for (int i = -1; i < 2; i++)
        {
            buildPos.Set(i, 0, 0);
            //buildPos = Quaternion.AngleAxis(-90, Vector3.up) * buildPos;
            tileCoordinates.Add(new Vector3Int(buildPos.x, buildPos.y, buildPos.z));
            //savedPattern = buildPos;
            savedCoordinatesList = tileCoordinates;
        }
        //Debug.Log("my TileCoordinates have: " + tileCoordinates.Count + "Elements");
        SetStraightTileSprites(tileCoordinates);
    }

    void SetStraightTileSprites(List<Vector3Int> tileCoordinates)
    {
        Vector3Int[] currentPatternArray = tileCoordinates.ToArray();
        TileBase[] currentTilesArray = new TileBase[currentPatternArray.Length];
        for (int index = 0; index < currentPatternArray.Length; index++)
        {
            currentTilesArray[index] = index == 1 ? StraightTile : GrassTile; //line asks: does "index" currently equal 1? Then add StraightTile, otherwise add GrassTile
        }
        spriteArray = currentTilesArray;
    }


    public void CurvedLeftTrackPattern()
    {
        Vector3Int buildPos = new Vector3Int(0, 0, 0);
        List<Vector3Int> tileCoordinates = new List<Vector3Int>();

        for (int i = -1; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                buildPos.Set(i, j, 0);
                tileCoordinates.Add(new Vector3Int(buildPos.x, buildPos.y, buildPos.z));
                //savedPattern = buildPos;
                savedCoordinatesList = tileCoordinates;
            }
        }

        SetCurveLeftTileSprites(tileCoordinates);
        //Debug.Log("my TileCoordinates have: " + tileCoordinates.Count + "Elements");
    }

    public void CurvedRightTrackPattern()
    {
        Vector3Int buildPos = new Vector3Int(0, 0, 0);
        List<Vector3Int> tileCoordinates = new List<Vector3Int>();

        for (int i = -1; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                buildPos.Set(i, j, 0);
                tileCoordinates.Add(new Vector3Int(buildPos.x, buildPos.y, buildPos.z));
                //savedPattern = buildPos;
                savedCoordinatesList = tileCoordinates;
            }
        }

        SetCurveRightTileSprites(tileCoordinates);
        //Debug.Log("my TileCoordinates have: " + tileCoordinates.Count + "Elements");
    }

    void SetCurveLeftTileSprites(List<Vector3Int> tileCoordinates)
    {
        Vector3Int[] currentPatternArray = tileCoordinates.ToArray();
        TileBase[] currentTilesArray = new TileBase[currentPatternArray.Length];
       
        //fills Array with specific tiles
        currentTilesArray[0] = CurveTileDL;
        currentTilesArray[1] = CurveTileUL;
        currentTilesArray[2] = GrassTile;
        currentTilesArray[3] = CurveTileDR;
        currentTilesArray[4] = CurveTileUR;
        currentTilesArray[5] = GrassTile;
        currentTilesArray[6] = GrassTile;
        currentTilesArray[7] = GrassTile;
        currentTilesArray[8] = GrassTile;


        spriteArray = currentTilesArray;
    }

    void SetCurveRightTileSprites(List<Vector3Int> tileCoordinates)
    {
        Vector3Int[] currentPatternArray = tileCoordinates.ToArray();
        TileBase[] currentTilesArray = new TileBase[currentPatternArray.Length];

        //fills Array with specific tiles
        currentTilesArray[0] = GrassTile;
        currentTilesArray[1] = GrassTile;
        currentTilesArray[2] = GrassTile;
        currentTilesArray[3] = CurveTileDR;
        currentTilesArray[4] = CurveTileUR;
        currentTilesArray[5] = GrassTile;
        currentTilesArray[6] = CurveTileDL;
        currentTilesArray[7] = CurveTileUL;
        currentTilesArray[8] = GrassTile;


        spriteArray = currentTilesArray;
    }


    public List<Vector3Int> GetPattern() //sends a pattern to whoever calls it
    {
        return this.savedCoordinatesList;
    }

    public TileBase[] GetSprites()
    {
        return this.spriteArray;
    }

    public TileBase GetSingleTile(int i)
    {
        return this.tileset.tiles[i];
    }
}
