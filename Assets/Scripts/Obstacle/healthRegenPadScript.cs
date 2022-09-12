using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthRegenPadScript : MonoBehaviour
{
    public int waitTime = 30;
    public Collider2D myCollider;

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
            other.transform.parent.gameObject.GetComponent<CarCollisionManager>().HealthRegenPadBehavior();
            StartCoroutine(DisabledPadForTime());
        }
    }

    private IEnumerator DisabledPadForTime()
    {
        //whatever makes the pad look like its disabled...
        GetComponent<SpriteRenderer>().color = new Color32(200, 200, 200, 100);
        myCollider.enabled = false;

        //wait for "waitTime" Seconds
        yield return new WaitForSeconds(waitTime);

        ReEnablePad();
    }

    private void ReEnablePad()
    {
        StopCoroutine(DisabledPadForTime());

        //make the pad look enabeld again...
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        myCollider.enabled = true;
    }
}
