using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manager for Visual Effects when a Cars Health drops to 0 and gets destroyed
 * only here to call methods of other classes and combine them
 */

public class OnOutOfHealthDestroy : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject explosion;

    Vector3 explosionScreenPos;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        ArenaCarDestroyer.OnOutOfHealthDestroy += playEffects; //Subscription to the event
    }

    private void OnDestroy()
    {
        ArenaCarDestroyer.OnOutOfHealthDestroy -= playEffects; //Desubscription to Event
    }

    private void playEffects(GameObject destroyedCar)
    {
        Instantiate(explosion, destroyedCar.transform.position, Quaternion.identity);
        mainCamera.GetComponent<CameraManager>().allowCamShake();
        Debug.Log("Car destroy Event played");
    }
}
