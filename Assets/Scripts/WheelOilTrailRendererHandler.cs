using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOilTrailRendererHandler : MonoBehaviour
{
    TopDownCarController carController;
    TrailRenderer trailRenderer;
    CarLayerHandler layerHandler;

    bool thisFrameUnderpass;
    bool prevFrameWasUnderpass = false;

    void Start()
    {
        carController = GetComponentInParent<TopDownCarController>();
        layerHandler = GetComponentInParent<CarLayerHandler>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.getSlipperyStatus() && prevFrameWasUnderpass == true)
        {
            trailRenderer.emitting = true;
        }
        else if (carController.getSlipperyStatus() && layerHandler.IsDrivingOnOverpass())
        {
            Invoke("setEmitter", 0.3f);
        }
        else trailRenderer.emitting = false;

        setOverUnderpass();
        
    }

    private void setOverUnderpass()
    {
        if (layerHandler.IsDrivingOnOverpass() == false)
        {
            thisFrameUnderpass = true;
        }
        else if (layerHandler.IsDrivingOnOverpass() && thisFrameUnderpass)
        {
            thisFrameUnderpass = false;
            prevFrameWasUnderpass = true;
        }
        else prevFrameWasUnderpass = false;
    }

    private void setEmitter() => trailRenderer.emitting = true;
}
