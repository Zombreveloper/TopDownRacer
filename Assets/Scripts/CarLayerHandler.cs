/*
THis scipt is completely inspired by this:
https://youtu.be/JSXrXZeMkjU
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> defaultLayerSpriteRenderers = new List<SpriteRenderer>();

    bool isDrivingOnOverpass = false;

    void Awake()
    {
      foreach(SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
      {
        if (spriteRenderer.sortingLayerName == "Default")
        {
          defaultLayerSpriteRenderers.Add(spriteRenderer);
        }
      }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2d(Collider2D collider2d)
    {
      if (collider2d.CompareTag("UnderpassTrigger"))
      {
        isDrivingOnOverpass = false;
      }
      else if (collider2d.CompareTag("OverpasssTrigger"))
      {
        isDrivingOnOverpass = true;
      }
    }

    //bei min 15 weitermachen
}
