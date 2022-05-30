using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrackBuildMarker : MonoBehaviour
{

    private Vector3Int pointerPos;

    private void Awake()
    {
        pointerPos.Set(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Pointer is now on Position: " + pointerPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveUp()
    {
        pointerPos.y = pointerPos.y + 1;
        Debug.Log("Pointer is now on Position: " + pointerPos);
    }

    public Vector3Int GetMarkerPos()
    {
        return this.pointerPos;
    }


}
