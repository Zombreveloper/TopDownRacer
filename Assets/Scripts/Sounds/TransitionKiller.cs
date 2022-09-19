using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        //Debug.Log(SceneManager.GetActiveScene().name);
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        if (SceneChangeAudioKeeper.Instance.gameObject)
        {
            if (SceneManager.GetActiveScene().name != "Menu Test" && SceneManager.GetActiveScene().name != "Player Count Test")
            {
                //Debug.Log(SceneManager.GetActiveScene());
                Destroy(SceneChangeAudioKeeper.Instance.gameObject); //.GetComponent<AudioSource>());
            }
        }

    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}
