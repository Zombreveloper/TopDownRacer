using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void FadeOutEvent(float waitTime, object go)
    {
        // Get the GameObject fromt the recieved Object.
        GameObject parent = go as GameObject;

        // Get all ChildGameObjects and try to get their PBR Shader
        List<RPBShader> shaders = parent
            .GetComponentsInChildren(typeof(RPBShader)).ToList();

        // Call our FadeOut Coroutine for each Component found.
        foreach (var shader in shaders)
        {
            // If the shader is null skip that element of the List.
            if (shader == null)
            {
                continue;
            }
            StartCoroutine(FadeOut(waitTime, shader));
        }
    }

    private IEnumerator FadeOut(float waitTime, RPBShader shader)
    {
        // Decrease 1 at a time, 
        // with a delay equal to the time, 
        // until the Animation finished / 100.
        float delay = waitTime / 100;

        while (shader.alpha > 0)
        {
            shader.alpha--;
            yield return new WaitForSeconds(delay);
        }
    }*/
    void startFading()
    {
        Color start = this.gameObject.GetComponent<SpriteRenderer>().color;
        Color end = new Vector4(1, 1, 1, 0);
        float duration = .5f; //in best case should me dynamic to always match the end of the animation but whatever
        StartCoroutine(FadeOverTime(start, end, duration));
    }

    IEnumerator FadeOverTime(Color start, Color end, float duration)
    {
        SpriteRenderer myRenderer = this.gameObject.GetComponent<SpriteRenderer>();


        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            myRenderer.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        myRenderer.color = end; //without this, the value will end at something like 0.9992367
    }

}
