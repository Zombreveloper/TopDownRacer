using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLayoutScripOnStart : MonoBehaviour
{
    public List<GameObject> Layouts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject Layout in Layouts)
        {
            Layout.SetActive(false);
        }

        int randomNumber = UnityEngine.Random.Range(0, 3);

        Layouts[randomNumber].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
