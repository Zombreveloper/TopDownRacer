using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfActiveCars : MonoBehaviour
{
    //list with all cars in the race
    public List<GameObject> carsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateList() //removes empty keys
    {
        carsList.RemoveAll(GameObject => GameObject == null);
    }

    public List<GameObject> getCarsList()
    {
        return this.carsList;
    }

    public GameObject getCarFromList(int i)
    {
        return this.carsList[i];
    }
}
