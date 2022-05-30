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

    public void StraightLinePattern(int posY, Vector3Int buildPos) //Builds List of coordinates for tiles in relation to each other
    {
        List<Vector3Int> tileCoordinates = new List<Vector3Int>();
        for (int i = -1; i < 2; i++)
        {
            buildPos.Set(i, posY, 0);
            tileCoordinates.Add(new Vector3Int(buildPos.x, buildPos.y, buildPos.z));
            //savedPattern = buildPos;
            savedCoordinates = tileCoordinates;
        }
        Debug.Log("my TileCoordinates have: " + tileCoordinates.Count + "Elements");
    }

    public List<Vector3Int> GetPattern() //sends a pattern to whoever calls it
    {
        //return this.savedPattern;
        return this.savedCoordinates;
    }

}
