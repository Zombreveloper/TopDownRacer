/*
THis scipt is completely inspired by this:
https://youtu.be/JSXrXZeMkjU
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    public SpriteRenderer carOutlineSpriteRenderer;
    public SpriteRenderer carNormalSpriteRenderer;

    List<SpriteRenderer> defaultLayerSpriteRenderers = new List<SpriteRenderer>();

    List<Collider2D> underpassColliderList = new List<Collider2D>();
    List<Collider2D> overpassColliderList = new List<Collider2D>();

    Collider2D carCollider;

    bool isDrivingOnOverpass = true;

    void Awake()
    {
      foreach(SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
      {
        if (spriteRenderer.sortingLayerName == "Default")
        {
            defaultLayerSpriteRenderers.Add(spriteRenderer);
        }
      }

      foreach(GameObject overpassColliderGameObject in GameObject.FindGameObjectsWithTag("overpass collider"))
      {
        overpassColliderList.Add(overpassColliderGameObject.GetComponent<Collider2D>());
      }

      foreach(GameObject underpassColliderGameObject in GameObject.FindGameObjectsWithTag("underpass collider"))
      {
        underpassColliderList.Add(underpassColliderGameObject.GetComponent<Collider2D>());
      }

      carCollider = GetComponentInChildren<Collider2D>();

      //Default drive on underpass
      carCollider.gameObject.layer = LayerMask.NameToLayer("ObjectOnOverpass"); //in Projectsettings->Physics2D->Layer Collision Matrix->disable collision between Over- and Underpass Layer
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSortingAndCollisionLayers();
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

            carOutlineSpriteRenderer.enabled = false;
            carNormalSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        }
        else
        {
            SetSortingLayer("track");

            carOutlineSpriteRenderer.enabled = true;
            carNormalSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }

        SetCollisionWithOverpass();
    }

    void SetCollisionWithOverpass()
    {
        foreach (Collider2D collider2D in overpassColliderList)
        {
            Physics2D.IgnoreCollision(carCollider, collider2D, !isDrivingOnOverpass);
        }

        foreach (Collider2D collider2D in underpassColliderList)
        {
            if (isDrivingOnOverpass)
            {
                Physics2D.IgnoreCollision(carCollider, collider2D, true);
            }
            else
            {
                Physics2D.IgnoreCollision(carCollider, collider2D, false);
            }
        }
    }

    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in defaultLayerSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        Debug.Log("Car Layer Handler entered a trigger");
        if (collider2d.CompareTag("underpass"))
        {
            //Debug.Log("underpass");
            isDrivingOnOverpass = false;
            carCollider.gameObject.layer = LayerMask.NameToLayer("ObjectOnUnderpass"); //in Projectsettings->Physics2D->Layer Collision Matrix->disable collision between Over- and Underpass Layer
            UpdateSortingAndCollisionLayers();
        }
        else if (collider2d.CompareTag("overpass"))
        {
            //Debug.Log("overpass");
            isDrivingOnOverpass = true;
            carCollider.gameObject.layer = LayerMask.NameToLayer("ObjectOnOverpass"); //in Projectsettings->Physics2D->Layer Collision Matrix->disable collision between Over- and Underpass Layer
            UpdateSortingAndCollisionLayers();
        }
    }
}
