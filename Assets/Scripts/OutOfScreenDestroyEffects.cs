using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manager for Visual Effects when a Car leaves the Screen and gets destroyed
 */

public class OutOfScreenDestroyEffects : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject explosion;
    private List<GameObject> explosions;

    Vector3 explosionScreenPos;

    // Start is called before the first frame update
    void Start()
    {
        explosions = new List<GameObject>();
        mainCamera = GameObject.FindObjectOfType<Camera>();

        CarDestroyer.OnOutOfScreenDestroy += doEvent;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (GameObject _explosion in explosions)
        {
            Debug.Log(explosionScreenPos);
            _explosion.transform.position = explosionScreenPos + mainCamera.transform.position;
        }
    }

    private void doEvent(GameObject destroyedCar)
    {
        explosions.Add(Instantiate(explosion, destroyedCar.transform.position, Quaternion.identity));
        explosionScreenPos = destroyedCar.transform.position - mainCamera.transform.position;
        Debug.Log("Car destroy Event played");
    }
}
