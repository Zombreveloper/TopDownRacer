using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class TilingPatterns : MonoBehaviour
{
    //variables
    List<Vector3Int> savedCoordinatesList;
    TileBase[] SpriteArray;

    [Header("Tiles to build with")]
    [SerializeField] TileBase GrassTile;
    [SerializeField] TileBase StraightTile;
    [SerializeField] TileBase CurveTileUL;
    [SerializeField] TileBase CurveTileUR;
    [SerializeField] TileBase CurveTileDL;
    [SerializeField] TileBase CurveTileDR;

    //referenced classes

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SetTileSprites(tileCoordinates);
    }

    void SetTileSprites(List<Vector3Int> tileCoordinates)
    {
        Vector3Int[] currentPatternArray = tileCoordinates.ToArray();
        TileBase[] currentTilesArray = new TileBase[currentPatternArray.Length];
        for (int index = 0; index < currentPatternArray.Length; index++)
        {
            currentTilesArray[index] = index == 1 ? StraightTile : GrassTile;
        }
        SpriteArray = currentTilesArray;
    }


    public void CurvedTrackPattern()
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
        //Debug.Log("my TileCoordinates have: " + tileCoordinates.Count + "Elements");
    }



    public List<Vector3Int> GetPattern() //sends a pattern to whoever calls it
    {
        return this.savedCoordinatesList;
    }

    public TileBase[] GetSprites()
    {
        return this.SpriteArray;
    }
}
