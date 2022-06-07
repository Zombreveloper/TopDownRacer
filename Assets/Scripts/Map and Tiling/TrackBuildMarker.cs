using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrackBuildMarker : MonoBehaviour
{

    private Vector3Int pointerPos; //represents pointers position
    private Vector3Int pointerRot; //represents pointers rotation

    private void Awake()
    {
        pointerPos.Set(0, 0, 0);
        pointerRot.Set(0, 0, 0); //rotation, 0 means upwards
        Debug.Log("Pointer wakes at Position: " + pointerPos);
    }

    // Start is called before the first frame update but only AFTER the coroutine in MapBuilder!!!
    void Start()
    {
        //Debug.Log("Pointer starts at Position: " + pointerPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Public functions
    public void MoveUp(int steps)
    {
        pointerPos.y = pointerPos.y + steps;
        Debug.Log("Pointer is now on Position: " + pointerPos);
    }

    public void MoveLeft(int steps)
    {
        pointerPos.x = pointerPos.x - steps;
        Debug.Log("Pointer is now on Position: " + pointerPos);
    }

    public void RotateLeft()
    {
        pointerRot.z = pointerRot.z + 90;
        Debug.Log("Pointer has now rotation of: " + pointerRot);
    }

    //getters and setters
    public Vector3Int GetMarkerPos()
    {
        return this.pointerPos;
    }

    public Vector3Int GetMarkerRot()
    {
        return this.pointerRot;
    }


}
