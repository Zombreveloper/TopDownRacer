using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* basic Idea from this Tutorial: https://www.youtube.com/watch?v=4OQjnKUENoE
 * I will need to include a Grid in which the obstacles will spawn ~ Stickan
 */

public class RandomObstacleSpawner : MonoBehaviour
{
    public int amountToSpawn;
    public List<GameObject> spawnPool;
    public GameObject streetMap;

    private ObstacleAreaGenerator obstacleAreaGen;

    //variables for delayed start to spawn obstacles
    [SerializeField] int waitBuildingObstacles = 1; //amount of checkpoints that must be passed before obstacles are set
    int delayIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        obstacleAreaGen = FindObjectOfType<ObstacleAreaGenerator>();
        //spawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeRandomObstacles(Tilemap _streetMap, TrackBuildMarker _buildMarker, string direction) //TODO: function name only for experimental purposes
    {
        if (delayIndex < waitBuildingObstacles)
        {
            delayIndex++;
            return;
        }

        Tilemap map = obstacleAreaGen.setObstacleMap(_streetMap, _buildMarker, direction);

        List<Vector3Int> possiblePlaces = obstacleAreaGen.makePlacementArray(map);
        int listIndex = 0;
        foreach (Vector3Int validLocation in possiblePlaces)
        {
            listIndex++;
            Quaternion gradualRotation = rotateWithCurve(direction, possiblePlaces, listIndex);
            int randomNumber = Random.Range(0, 15);
            if (randomNumber == 0)
            {
                GameObject randomObstacle = chooseRandomObject();
                //GameObject randomObstacle = placeholderObs;
                Instantiate(randomObstacle, map.GetCellCenterWorld(validLocation), _buildMarker.getRotationQuaternion() * gradualRotation, map.transform.parent.transform);
                //Instantiate(randomObstacle, map.GetCellCenterWorld(validLocation), gradualRotation, map.transform.parent.transform); //for testing, only the additional Rotation gets set 
            }
        }

        obstacleAreaGen.checkDestroyObstacleMap();
    }

    Quaternion rotateWithCurve(string _direction, List<Vector3Int> _possiblePlaces, int _listIndex) //for every spot in a curve the iterator goes through the rotation gets a little wider
    {
        float endRotation = 0;
        if (_direction == "leftCurve" || _direction == "rightCurve")
        {
            if (_direction == "leftCurve")
            {
                endRotation = 90;
            }
            else if (_direction == "rightCurve")
            {
                endRotation = -90;
            }
            float fListIndex = (float)_listIndex;
            float fListLength = (float)_possiblePlaces.Count; //cast integer to float. otherwise division will only result in whole numbers

            float interpolator = fListIndex / fListLength;
            float lipoResult = Mathf.LerpAngle(0f, endRotation, interpolator);
            Debug.Log("index is " + _listIndex + " and the list has " + _possiblePlaces.Count + " entries");
            Debug.Log(lipoResult);
            Quaternion additionalRotation = Quaternion.Euler(0, 0, lipoResult);
            return additionalRotation;
        }
        else
        {
            Quaternion additionalRotation = Quaternion.Euler(0, 0, 0);
            return additionalRotation;
        }
        
    }

    public GameObject chooseRandomObject()
    {
        GameObject toSpawn;

        int randomItem = Random.Range(0, spawnPool.Count);
        toSpawn = spawnPool[randomItem];

        /*for (int i = 0; i < spawnPool.Count; i++)
        {
            Debug.Log("List Index " + i + " carries the GameObject " + spawnPool[i]);
        }*/

        return toSpawn;
    }
}
