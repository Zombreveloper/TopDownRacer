using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartManager : MonoBehaviour
{
    private AudioManager audioManager;
    public string[] countdownVoice;

    ListOfActiveCars activeCars;
    //LassesTestInputHandler Inputhandler;
    public TMP_Text mytext;
    public int i;
    private int voiceIndex;
    //private List<GameObject> myCarsList;

    [Header("Toggle to let cars start instantly")]
    [SerializeField] bool instantStart = true;


    void Awake()
    {
        activeCars = FindObjectOfType<ParticipantsManager>().GetComponent<ListOfActiveCars>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        StartCars();
        mytext.text = "3";
        i = 3;
        voiceIndex = 0;
        if (instantStart)
        {
            Canvas canvas = this.gameObject.GetComponentInChildren<Canvas>();
            canvas.enabled = false;
            StartCoroutine(NoCountdown());

        }

        else
        {
            StartCoroutine(Countdown1());
            audioManager.Play(countdownVoice[voiceIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator NoCountdown()
    {
        yield return 0;
        StartCars();

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
        i--;
        voiceIndex++;

        if (i >= 1)
        {
            mytext.text = i.ToString();
            //Debug.Log("the number is " + i);
            //sound "möp"
            audioManager.Play(countdownVoice[voiceIndex]);
        }
        else if (i == 0)
        {
            mytext.text = "GO!";
            //sound "meep"
            StartCars();
            audioManager.Play(countdownVoice[voiceIndex]);
        }
        else
        {
            mytext.text = "";
            StopCoroutine(Countdown1());
        }
    }

    void StartCars()
    {
        List<GameObject> activeCarsList = activeCars.getCarsList();
        foreach (GameObject car in activeCarsList)
        {
            //car.LassesTestInputHandler.SetCarTorque(1);

            LassesTestInputHandler Inputhandler = car.GetComponent<LassesTestInputHandler>();
            Inputhandler.GameStartet(true);
        }
    }
}
