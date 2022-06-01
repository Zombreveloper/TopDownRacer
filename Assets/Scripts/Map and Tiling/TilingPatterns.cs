using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class TilingPatterns : MonoBehaviour
{
    //variables
    //Vector3Int savedPattern;
    List<Vector3Int> savedCoordinates;

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
            tileCoordinates.Add(new Vector3Int(buildPos.x, buildPos.y, buildPos.z));
            //savedPattern = buildPos;
            savedCoordinates = tileCoordinates;
        }
        Debug.Log("my TileCoordinates have: " + tileCoordinates.Count + "Elements");
    }

    public void CurvedTrackPattern()
    {
        Vector3Int buildPos = new Vector3Int(0, 0, 0);
        List<Vector3Int> tileCoordinates = new List<Vector3Int>();

        for (int i = -1; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                buildPos.Set(i, j, 0);
                tileCoordinates.Add(new Vector3Int(buildPos.x, buildPos.y, buildPos.z));
                //savedPattern = buildPos;
                savedCoordinates = tileCoordinates;
            }
        }
        Debug.Log("my TileCoordinates have: " + tileCoordinates.Count + "Elements");
    }



    public List<Vector3Int> GetPattern() //sends a pattern to whoever calls it
    {
        //return this.savedPattern;
        return this.savedCoordinates;
    }

}
