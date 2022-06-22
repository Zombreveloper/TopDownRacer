/*
THis scipt is completely inspired by this:
https://youtu.be/JSXrXZeMkjU
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    public List<SpriteRenderer> defaultLayerSpriteRenderers = new List<SpriteRenderer>();

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

    void UpdateSortingAndCollisionLayers()
    {
        if (isDrivingOnOverpass)
        {
            SetSortingLayer("above track");
        }
        else
        {
            SetSortingLayer("track");
        }
    }

    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in defaultLayerSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    void OnTriggerEnter2d(Collider2D collider2d)
    {
        Debug.Log("trigger");
        if (collider2d.CompareTag("underpass"))
        {
            Debug.Log("underpass");
            isDrivingOnOverpass = false;
            UpdateSortingAndCollisionLayers();
        }
        else if (collider2d.CompareTag("overpass"))
        {
            Debug.Log("overpass");
            isDrivingOnOverpass = true;
            UpdateSortingAndCollisionLayers();
        }
    }
}
