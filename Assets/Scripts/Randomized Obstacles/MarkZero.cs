using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* only there to make the center of the grid visible
 * 
 */

public class MarkZero : MonoBehaviour
{
    Vector3Int gridCenter = Vector3Int.zero;
    public GameObject visibleMarker;


    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        Vector3 gridInWorldPoint = tilemap.GetCellCenterWorld(gridCenter);
        Instantiate(visibleMarker, gridInWorldPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
