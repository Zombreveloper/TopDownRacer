using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    private AudioManager audioManager;
    private AudioSource engineSource;
    private TopDownCarController carController;
    public string[] crashSounds;
    public string motorSound;
    //try to control source remotely
    private AudioSource mySource;
    [SerializeField]
    private Sound sound;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        buildAudioSource();
        //mySource = audioManager.gameObject.AddComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<TopDownCarController>();

        engineSource = GetComponentInChildren<AudioSource>();
        //audioManager.instantiateSource(motorSound);
        //audioManager.Play(motorSound);
        //engineSource.Play();
        mySource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        playEngineSound();
    }

    private void OnDestroy()
    {
        if (mySource != null)
            Object.Destroy(mySource);
    }

    void buildAudioSource()
    {
        mySource = audioManager.gameObject.AddComponent<AudioSource>();
        mySource.clip = sound.clip;

        mySource.volume = sound.volume;

        mySource.pitch = sound.pitch;
        mySource.loop = sound.loop;
        mySource.outputAudioMixerGroup = sound.outputAudioMixerGroup;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ArenaMap" || collision.collider.tag == "underpass collider" || collision.collider.tag == "overpass collider" || collision.collider.transform.parent.tag == "Car")
        {
            CrashSound();
        }
    }

    void CrashSound()
    {
        var number = Random.Range(0,3);
        string soundName = crashSounds[number];
        audioManager.Play(soundName);
    }

    float minPitch = 1.0f;
    float maxPitch = 5f;
    void playEngineSound()
    {
        float carSpeed = carController.getRelativeCarVelocity();
        float RPM = carSpeed / carController.currentGear();
        float effectiveRPM = Mathf.Lerp(minPitch, maxPitch, RPM);
        //Debug.Log(carSpeed);
        //audioManager.setPitch(motorSound, effectiveRPM);
        setPitch(mySource, effectiveRPM);
    }

    public void setPitch(AudioSource source, float newPitch)
    {
        if (source == null) //does not play a sound that is not there. Catches spelling Errors.
        {
            Debug.Log("ERROR in AudioManager.Play(): Sound " + name + " was not found!!!");
            return;
        }
        source.pitch = newPitch;
    }

    public AudioSource getAudioSource()
    { return this.mySource; }
}
