using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new Track Tile Pool", menuName = "Tiles/Race Track")]
public class TilePoolList_SO : ScriptableObject
{
    public List<TileBase> tiles;
}
