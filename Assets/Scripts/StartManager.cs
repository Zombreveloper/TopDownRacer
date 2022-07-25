using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartManager : MonoBehaviour
{
    ListOfActiveCars activeCars;
    //LassesTestInputHandler Inputhandler;
    public TMP_Text mytext;
    public int i;
    //private List<GameObject> myCarsList;

    void Awake()
    {
        activeCars = FindObjectOfType<ParticipantsManager>().GetComponent<ListOfActiveCars>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mytext.text = "0";
        i = 0;
        StartCoroutine(Countdown1());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Countdown1()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); //wait 2 seconds
            //do thing
            //Debug.Log("Countdown2");
            Countdown();
        }
    }

    void Countdown()
    {
        i++;

        if (i <= 3)
        {
            mytext.text = i.ToString();
            //Debug.Log("the number is " + i);
            //sound "möp"
        }
        else if (i == 4)
        {
            mytext.text = "GO!";
            //sound "meep"
            StartCars();
        }
        else
        {
            mytext.text = "";
            StopCoroutine(Countdown1());
        }
    }

    void StartCars()
    {
        foreach (GameObject car in activeCars.carsList)
        {
            //car.LassesTestInputHandler.SetCarTorque(1);

            LassesTestInputHandler Inputhandler = car.GetComponent<LassesTestInputHandler>();
            Inputhandler.GameStartet(true);
        }
    }
}
