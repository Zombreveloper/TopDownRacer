using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* inspired by https://www.youtube.com/watch?v=8PXPyyVu_6I */
public class CameraShaker : MonoBehaviour
{
    //[SerializeField]
    private float shakeTime = .5f, shakePower = 1; 
    float shakeFadeTime;

    Vector3 shakeOutput = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //CarDestroyer.OnOutOfScreenDestroy += startShake; //Subscription for Event
    }

    // Update is called once per frame
    void Update()
    {
     /* if (Input.GetKeyDown(KeyCode.K))
        {
            startShake(.5f, 1f);
        }*/
    }

    private void LateUpdate()
    {
    }

    private void startShake(GameObject car)
    {
        //shakeTime = duration;
        //shakePower = power;
        StartCoroutine(doShake());
    }

    public void startShake(float duration, float power)
    {
        shakeTime = duration;
        shakePower = power;

        shakeFadeTime = power / duration;
        StartCoroutine(doShake());
    }

    public IEnumerator doShake()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Event Camera shake played");
        float shakeTimeRemaining = shakeTime;
        while (shakeTimeRemaining > 0)
        {
            //Debug.Log("if-statement is met");
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            shakeOutput = new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            yield return null;
        }
        shakeOutput = Vector3.zero;
        yield break;
    }
    //TODO maybe let the coroutine give out the created Vector and the Camera Manager adds the values in LateUpdate

    public Vector3 getShakeValue()
    {
        return shakeOutput;
    }
}
