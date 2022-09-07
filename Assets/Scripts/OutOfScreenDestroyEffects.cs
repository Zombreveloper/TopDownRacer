using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manager for Visual Effects when a Car leaves the Screen and gets destroyed
 * only here to call methods of other classes and combine them
 */

public class OutOfScreenDestroyEffects : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject explosion;

    Vector3 explosionScreenPos;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        CarDestroyer.OnOutOfScreenDestroy += playEffects; //Subscription to the event
    }

    private void OnDestroy()
    {
        CarDestroyer.OnOutOfScreenDestroy -= playEffects; //Desubscription to Event
    }

    private void playEffects(GameObject destroyedCar)
    {
        Instantiate(explosion, destroyedCar.transform.position, Quaternion.identity);
        mainCamera.GetComponent<CameraManager>().allowCamShake(destroyedCar);
        Debug.Log("Car destroy Event played");
    }
}
