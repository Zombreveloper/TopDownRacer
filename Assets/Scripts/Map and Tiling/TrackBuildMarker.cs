using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TrackBuildMarker : MonoBehaviour
{

    private Vector3Int pointerPos; //represents pointers position
    private Vector3Int pointerRot; //represents pointers rotation

    public GameObject visibleMarker;
    private GameObject instMarker; //instanced version of the visibleMarker
    Tilemap tilemap;

    //checkbox for printing Pos and Rot Logs
    [Header("Toggle to enable poition an rotation Logs")]
    [SerializeField] bool printsEnabled = true;

    [Header("Toggle to enable marker Visualization")]
    [SerializeField] bool MVisualize = true;

    private void Awake()
    {
        tilemap = GameObject.Find("/Grid/Streetmap").GetComponent<Tilemap>(); //GetComponent<Tilemap>();
        pointerPos.Set(0, 0, 0);
        pointerRot.Set(0, 0, 0); //rotation, 0 means upwards
        CheckBools();
        

    }

    private void CheckBools()
    {
        if (printsEnabled)
        {
            Debug.Log("Pointer wakes at Position: " + pointerPos);
        }
        if (MVisualize)
        {
            instMarker = Instantiate(visibleMarker, pointerPos, Quaternion.identity);
        }
    }

    // Start is called before the first frame update but only AFTER the coroutine in MapBuilder!!!
    void Start()
    {
        //Debug.Log("Pointer starts at Position: " + pointerPos);
        //Instantiate(visibleMarker, pointerPos, Quaternion.identity);
    }

    private void Update()
    {
        if (instMarker != null)
        {
            updateMarker();
        }
    }

    //will show me a visible version of the marker for debugging
    void updateMarker() //this is crap. But i need this for testing
     {

         Vector3 gridInWorldPoint = tilemap.GetCellCenterWorld(pointerPos);
        instMarker.transform.position = gridInWorldPoint; //tilemap.WorldToCell(pointerPos);
        Quaternion newRotation = Quaternion.Euler(pointerRot); // +(new Vector3 (0,-90,0)));
        instMarker.transform.rotation = newRotation ;

     }

    //Public functions

    public Quaternion getRotationQuaternion()
    {
        Vector3 markerRotation = GetMarkerRot();
        Quaternion targetRotation = Quaternion.Euler(markerRotation);
        return targetRotation;
    }

    public void StepForward(int steps)
    {
        pointerPos = pointerPos + RotatedStep(steps);
        if (printsEnabled)
        {
            Debug.Log("Pointer is now on Position: " + pointerPos);
        }
        
    }
   

    public void RotateLeft()
    {
        pointerRot.z = pointerRot.z + 90;
        if (printsEnabled)
        {
            Debug.Log("Pointer has now rotation of: " + pointerRot);
        }
        
    }

    public void RotateRight()
    {
        pointerRot.z = pointerRot.z - 90;
        if (printsEnabled)
        {
            Debug.Log("Pointer has now rotation of: " + pointerRot);
        }
    }

    Vector3Int RotatedStep(int stepsForward)
    {
        
       Vector3 stepDirection = new Vector3(0, stepsForward, 0);
       
            Vector3 markerRotation = pointerRot; //transforms Vector3Int pointerRot to Vector3
            Quaternion rotation = Quaternion.Euler(markerRotation);
            Matrix4x4 rotMatrix = Matrix4x4.Rotate(rotation);
            //lines above build our rotation Matrix

            Vector3 rotatedVector = rotMatrix.MultiplyPoint3x4(stepDirection); //applies rotation to current position
            Vector3Int rotatedIntVector = Vector3Int.RoundToInt(rotatedVector); //transforms result back to Vector3Int
            return rotatedIntVector;
        
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


    //Trash dump
    public void MoveUp(int steps)
    {
        pointerPos.y = pointerPos.y + steps;
        Debug.Log("Pointer is now on Position: " + pointerPos);
    }

    public void MoveLefd(int steps)
    {
        pointerPos.x = pointerPos.x - steps;
        Debug.Log("Pointer is now on Position: " + pointerPos);
    }

}
