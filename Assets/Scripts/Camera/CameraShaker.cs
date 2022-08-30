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

    public void startShake(float duration, float power)
    {
        shakeTimeRemaining = duration;
        shakePower = power;
        StartCoroutine(doShake());
    }

    private IEnumerator doShake()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);
            yield return null;
        }
        yield break;
    }
}
