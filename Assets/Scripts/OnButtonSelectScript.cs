using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonSelectScript : MonoBehaviour
{
    public Button myButton;
    public GameObject showMe;
    public DynamicMenuNavigationScript masterScript;
    private Selectable currentSelected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentSelected = masterScript.GetSelectedSelectable();
        checkIfImSelected();
    }

    private void checkIfImSelected()
    {
        if (myButton == currentSelected)
        {
            showMe.SetActive(true);
        }
        else
        {
            showMe.SetActive(false);
        }
    }
}
