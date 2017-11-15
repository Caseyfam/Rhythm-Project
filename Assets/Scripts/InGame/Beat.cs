using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour {

    public float bpm = 120f;
    private AudioSource audioSource;
    private double nextTick = 0.0;
    private double timePerTick;
    bool ticked = false;

    private bool preciseTick = false;

    private bool paused;

	// Use this for initialization
	void Start ()
    {
        if (!paused)
        {
            double startTick = AudioSettings.dspTime;
            nextTick = startTick + (60.0 / bpm);
            timePerTick = 60.0f / bpm;
            StartCoroutine(WaitForEigth(timePerTick / 8));
        }
    }

    void LateUpdate()
    {
        if (!paused)
        {
            if (!ticked && nextTick >= AudioSettings.dspTime)
            {
                ticked = true;
                BroadcastMessage("OnTick"); //Onbeat
                StartCoroutine(WaitForTock(timePerTick / 2));
            }
        }
    }

    IEnumerator WaitForTock(double time)
    {
        yield return new WaitForSeconds((float)time);
        BroadcastMessage("OnTock"); // Offbeat
    }

    void OnTick()
    {
        if (!paused)
        {
            //Debug.Log(AudioSettings.dspTime + " " + (60.0f / bpm));
            StartCoroutine(WaitForInactive(0.4f * timePerTick));
            // 0.5f
        }
    }

    void OnTock()
    {
        if (!paused)
        {
            //StartCoroutine(WaitForInactive(timePerTick));
            StartCoroutine(WaitForActive(.4f * timePerTick));
            // 0.7f
        }
    }

    IEnumerator WaitForActive(double time)
    {
        yield return new WaitForSeconds((float)time);
        preciseTick = true;
        BroadcastMessage("OnPreciseTick", true);
    }
    IEnumerator WaitForInactive(double time)
    {
        yield return new WaitForSeconds((float)time);
        preciseTick = false;
        BroadcastMessage("OnPreciseTick", false);
    }

    IEnumerator WaitForEigth(double time)
    {
        yield return new WaitForSeconds((float)time);
        BroadcastMessage("OnEighth");
        StartCoroutine(WaitForEigth(time));
    }

    void OnEighth()
    {

    }

    void FixedUpdate()
    {
        if (!paused)
        {
            timePerTick = 60.0f / bpm;
            double dspTime = AudioSettings.dspTime;

            while (dspTime >= nextTick)
            {
                ticked = false;
                nextTick += timePerTick;
            }
        }
    }

    public float GetBPM()
    {
        return bpm;
    }

    public double GetTickSpeed()
    {
        return 60.0 / bpm;
    }

    public bool GetBPMPaused()
    {
        return paused;
    }

    public void SetBPMPaused(bool val)
    {
        paused = val;
        if (val)
        {
            GameObject.Find("Audio Source").GetComponent<AudioSource>().Pause();
        }
        else
        {
            GameObject.Find("Audio Source").GetComponent<AudioSource>().UnPause();
        }
    }
}
