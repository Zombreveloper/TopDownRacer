using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowmotionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    float targetValue;
    float effectDuration;
    public bool coroutineRunning = false;

    public static SlowmotionEffect instance;

    /*public SlowmotionEffect(float slowestTime, float duration)
    {
        targetValue = slowestTime;
        effectDuration = duration;
    }*/
    void Start()
    {
        //StartCoroutine(LerpFunction(targetValue, effectDuration));
    }
    public IEnumerator reverseSlowmotion(float startValue, float duration)
    {
        coroutineRunning = true;
        float time = 0;
        float endValue = 1f; //timeScale to start
        while (time < duration)
        {
            Time.timeScale = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
        coroutineRunning = false;
    }

    public IEnumerator slowmotion(float endValue, float duration)
    {
        coroutineRunning = true;
        float time = 0;
        float startValue = 1f; //timeScale to start
        while (time < duration)
        {
            Time.timeScale = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
        coroutineRunning = false;
    }



    private void OnDestroy() //failsafe if Object gets destroyed before end of coroutine
    {
        Time.timeScale = 1f;
        coroutineRunning = false;
    }
}
