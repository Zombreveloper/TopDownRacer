using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
This Script is designed, to make it easy to navigate throug a Menu of UI elemnts that get dynamicly SetActive or inactive.
It is used to navigate using ARROW UP AND DOWN.
It also loops on the vertical axis.
*/

public class DynamicMenuNavigationScript : MonoBehaviour
{
    public List<Selectable> ListOfUiElements = new List<Selectable>(); //has to be filleed in Inspector in Unity with UI Elements from screen-Top to screen-Bottom
    private int index;
    private Selectable currentUIElement;
    private bool downwards = true;
    public int startIndex; //defines wich object of the ListOfUiElements is the first selected ui Element

    // Start is called before the first frame update
    void Start()
    {
        index = startIndex;
        SelectUiElement();
        //Debug.Log("The current game object is: " + currentUIElement.gameObject.transform.parent.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            bool active = true;
            SelectNorthUiElement(active);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            bool active = true;
            SelectSouthUiElement(active);
        }
        else if (Input.GetKeyDown(KeyCode.Return)) //specific for Top Down Racing Games PlayerCounterMenu
        {
            if (currentUIElement != ListOfUiElements[0])
            {
                GetComponent<PlayerCounter>().startGame();
            }
            else if (currentUIElement == ListOfUiElements[0])
            {
                GetComponent<PlayerCounter>().backToMainMenu();
            }
        }
    }

    public void SelectSouthUiElement(bool _active) //cycles through Selectebles from screen-top to screen-bottom
    {
        StartCoroutine(waitForOneFrameSouth(_active)); //specific for PlayerCounterMenu, onValueChanged in TMP_InputField gets called before PlayerCounterMenu updates its ui, wich kind of generates an error
        /*index++; //next element in list

        if (index >= ListOfUiElements.Count)
        {
            index = 0; //loop from bottom to top
        }

        downwards = true;

        if (_active) //if element has to be SetActive(true)
        {
            SelectActiveUiElement();
        }
        else if (!_active) //if element has to be SetActive(false) or SetActive doesnt matter
        {
            SelectUiElement();
        }*/
    }

    private IEnumerator waitForOneFrameSouth(bool _active)
    {
        //returning 0 will make it wait 1 frame
        yield return 0;

        //code goes here
        index++; //next element in list

        if (index >= ListOfUiElements.Count)
        {
            index = 0; //loop from bottom to top
        }

        downwards = true;

        if (_active) //if element has to be SetActive(true)
        {
            SelectActiveUiElement();
        }
        else if (!_active) //if element has to be SetActive(false) or SetActive doesnt matter
        {
            SelectUiElement();
        }
    }

    public void SelectNorthUiElement(bool _active)  //cycles through Selectebles from screen-bottom to screen-top
    {
        StartCoroutine(waitForOneFrameNorth(_active)); //specific for PlayerCounterMenu, onValueChanged in TMP_InputField gets called before PlayerCounterMenu updates its ui, wich kind of generates an error
        /*index--;

        if (index < 0)
        {
            index = ListOfUiElements.Count - 1; //loop from top to bottom
        }

        downwards = false;

        if (_active) //if element has to be SetActive(true)
        {
            SelectActiveUiElement();
        }
        else if (!_active) //if element has to be SetActive(false) or SetActive doesnt matter
        {
            SelectUiElement();
        }*/
    }

    private IEnumerator waitForOneFrameNorth(bool _active)
    {
        //returning 0 will make it wait 1 frame
        yield return 0;

        //code goes here
        index--;

        if (index < 0)
        {
            index = ListOfUiElements.Count - 1; //loop from top to bottom
        }

        downwards = false;

        if (_active) //if element has to be SetActive(true)
        {
            SelectActiveUiElement();
        }
        else if (!_active) //if element has to be SetActive(false) or SetActive doesnt matter
        {
            SelectUiElement();
        }
    }

    private void SelectActiveUiElement() //selects next UI Element that is SetActive(true)
    {
        currentUIElement = ListOfUiElements[index];

        if (currentUIElement.gameObject.transform.parent.gameObject.activeSelf)
        {
            currentUIElement.Select();
        }
        else if (!currentUIElement.gameObject.transform.parent.gameObject.activeSelf)
        {
            if (downwards == true)
            {
                bool active = true;
                SelectSouthUiElement(active);
            }
            else if (downwards == false)
            {
                bool active = true;
                SelectNorthUiElement(active);
            }
        }
    }

    private void SelectUiElement() //selects next UI Element
    {
        currentUIElement = ListOfUiElements[index];
        currentUIElement.Select();
    }
}
