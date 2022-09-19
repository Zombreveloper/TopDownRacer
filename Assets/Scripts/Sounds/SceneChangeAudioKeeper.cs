using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeAudioKeeper : MonoBehaviour
{
	//Hier wird eine Instanz der momentanen Szene erstellt, die nur das 
	//Objekt enthält, an dem das Script hängt
	private static SceneChangeAudioKeeper instance = null;
	public static SceneChangeAudioKeeper Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

		// Die Musik soll mal nur nach dem Splashscreen weiterspielen
		//string currentPlanet = PlayerPrefs.GetString("fromPLanet", "crash");
		//Debug.Log("Die jetzige Scene ist: " + currentPlanet);


		DontDestroyOnLoad(this.gameObject);
	}
}
