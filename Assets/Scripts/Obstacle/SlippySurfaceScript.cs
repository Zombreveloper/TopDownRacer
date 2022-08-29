using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlippySurfaceScript : MonoBehaviour
{
    [SerializeField] float effectDurationSeconds = 5f;
    [Tooltip("Set between 0 and 1. Glitches out otherwise.")]
    [SerializeField] float targetDriftFactor = 0.98f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.tag == "Car")
        {
            other.transform.parent.gameObject.GetComponent<CarCollisionManager>().slippySurfaceBehavior(targetDriftFactor, effectDurationSeconds);
        }
    }
}
