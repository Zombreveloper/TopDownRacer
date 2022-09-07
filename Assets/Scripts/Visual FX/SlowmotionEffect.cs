using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowmotionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public float targetValue = 0.2f;
    public float effectDuration = 2;

    public static SlowmotionEffect instance;

    /*public SlowmotionEffect(float slowestTime, float duration)
    {
        targetValue = slowestTime;
        effectDuration = duration;
    }*/
    void Start()
    {
        StartCoroutine(LerpFunction(targetValue, effectDuration));
    }
    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = 1f; //timeScale to start
        while (time < duration)
        {
            Time.timeScale = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
    }
}
