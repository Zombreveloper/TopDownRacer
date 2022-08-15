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

    // Start is called before the first frame update
    void Start()
    {
        spawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObjects()
    {
        int randomItem = 0;
        GameObject toSpawn;
        TilemapCollider2D c = streetMap.GetComponent<TilemapCollider2D>();

        float borderX, borderY;
        Vector2 pos;

        for(int i = 0; i < amountToSpawn; i++)
        {
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];

            //bounds are defined by the size of the TilemapColliders. which unfortunately include grass
            borderX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            borderY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(borderX, borderY);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }
}
