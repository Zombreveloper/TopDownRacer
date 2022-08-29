using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOilTrailRendererHandler : MonoBehaviour
{
    TopDownCarController carController;
    TrailRenderer trailRenderer;

    void Awake()
    {
        carController = GetComponentInParent<TopDownCarController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.getSlipperyStatus() == true)
        {
            Invoke("setEmitter", 0.3f);
        }
        else trailRenderer.emitting = false;
    }


    private void setEmitter() => trailRenderer.emitting = true;
}
