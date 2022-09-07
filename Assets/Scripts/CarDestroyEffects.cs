using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manager for Visual Effects when a Car leaves the Screen and gets destroyed
 * only here to call methods of other classes and combine them
 */

public class CarDestroyEffects : MonoBehaviour
{
    private Camera mainCamera;
    private TopDownCarController[] carControllers;
    public GameObject explosion;

    Vector3 explosionScreenPos;

    [Header("Effects at Car Destroy")]
    [SerializeField] float shakeDurationDestroy = 1.5f; 
    [SerializeField] float shakePowerDestroy = 2f;
    [SerializeField] float lengthOfTimestop = 0.15f;

    [Header("Effects at hitting waypoint")]
    [SerializeField] float shakeDurationWaypoint = .5f; 
    [SerializeField] float shakePowerWaypoint = 1f;
    [SerializeField] bool allowUnevenSpins = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        carControllers = GameObject.FindObjectsOfType<TopDownCarController>();

        //Events
        CarDestroyer.OnCarDestroy += playDestroyEffects; //Subscription to the event
        WayPointScript.OnCarGotWaypoint += playWayPointEffects;
    }

    private void OnDestroy()
    {
        CarDestroyer.OnCarDestroy -= playDestroyEffects; //Desubscription to Event
        WayPointScript.OnCarGotWaypoint -= playWayPointEffects;
    }

    private void playDestroyEffectsBackup(GameObject destroyedCar) //Destroy Effects without Hitstop
    {
        Instantiate(explosion, destroyedCar.transform.position, Quaternion.identity);
        mainCamera.GetComponent<CameraManager>().allowCamShake(shakeDurationDestroy, shakePowerDestroy);
        Debug.Log("Car destroy Event played");
    }

    private void playWayPointEffects(GameObject collidedCar)
    {
        mainCamera.GetComponent<CameraManager>().allowCamShake(shakeDurationWaypoint, shakePowerWaypoint);
        if (carControllers != null)
        {
            foreach (TopDownCarController controller in carControllers)
            {
                if (controller.gameObject != collidedCar)
                controller.autoRotateCar(1, allowUnevenSpins);
            }
        }
        
    }

    private void playDestroyEffects(GameObject destroyedCar)
    {
        StartCoroutine(delayedDestroyEffects(destroyedCar));
    }

    IEnumerator delayedDestroyEffects(GameObject destroyedCar)
    {
        Instantiate(explosion, destroyedCar.transform.position, Quaternion.identity);
        //Hitstop start
        Time.timeScale = 0;
        float RealTimeOfTimestopStart = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < RealTimeOfTimestopStart + lengthOfTimestop)
        {
            yield return null;
        }
        Time.timeScale = 1f;
        //Hitstop end

        mainCamera.GetComponent<CameraManager>().allowCamShake(shakeDurationDestroy, shakePowerDestroy);
        Debug.Log("Car destroy Event played");
    }
}
