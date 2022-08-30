using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manager for Visual Effects when a Car leaves the Screen and gets destroyed
 */

public class OutOfScreenDestroyEffects : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        CarDestroyer.OnOutOfScreenDestroy += doEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void doEvent(GameObject destroyedCar)
    {
        Instantiate(explosion, destroyedCar.transform.position, Quaternion.identity);
        Debug.Log("Car destroy Event played");
    }
}
