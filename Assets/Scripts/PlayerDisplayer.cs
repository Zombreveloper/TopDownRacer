using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayer : MonoBehaviour
{
    //gets Data from this:
    public PlayerProfile profile_1;

    //displays Data here:
    public GameObject playerName;
    public GameObject inputFieldLeft;
    public GameObject inputFieldRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //profile_1.leftInput = inputFieldLeft.GetComponent<Text>().text;
        //Debug.Log(inputFieldLeft.GetComponent<Text>().text);
    }
}
