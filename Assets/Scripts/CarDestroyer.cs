using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    GameObject thisCar;

    


    // Start is called before the first frame update
    void Start()
    {
        Transform parentsTransform = this.transform.parent;
        thisCar = parentsTransform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(thisCar);
        Debug.Log("Car was destroyed");
    }
}
