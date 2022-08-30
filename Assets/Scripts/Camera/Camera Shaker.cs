using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private float shakeTimeRemaining, shakePower, shakeFadeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.K))
        {
            startShake(.5f, 1f);
        }
    }

    private void LateUpdate()
    {
        
    }

    public void startShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;
    }
}
