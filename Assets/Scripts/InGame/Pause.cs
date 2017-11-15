using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    AudioSource mainAudio;
	
    void Awake()
    {
        mainAudio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            if (mainAudio.isPlaying)
            {
                mainAudio.Pause();
            }
        }
        if (Time.timeScale == 0)
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                Time.timeScale = 1;
                if (!mainAudio.isPlaying)
                {
                    mainAudio.UnPause();
                }
            }
        }
        //mainAudio.pitch = mainAudio.pitch + 0.0001f;
	}
}
