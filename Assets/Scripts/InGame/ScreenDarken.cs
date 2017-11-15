using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDarken : MonoBehaviour {

    public UnityEngine.UI.Image screenDarken;
    public float darknessPerTick = 0.01f;
    private bool isDarkening = false;
    private bool isUnDarkening = false;
    private bool showFloorClear = false;

    public GameObject levelNameWiggle;

	// Update is called once per frame
	void Update ()
    {
		if (isDarkening)
        {
            screenDarken.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(screenDarken.color.a, 1f, darknessPerTick));
        }
        if (isUnDarkening)
        {
            screenDarken.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(screenDarken.color.a, 0f, darknessPerTick));
            if (screenDarken.color == new Color(0f, 0f, 0f, 0.4f))
            {
                levelNameWiggle.SetActive(false);
                StartCoroutine(WaitToUnpausedBPM(1f));
                try
                {
                    GameObject.Find("Main Camera").GetComponent<CameraFOV>().EntranceZoom();
                }
                catch
                {
                    Debug.Log("Camera does not have CameraFOV script");
                }
                // Also want to unfreeze BPM here
            }
            if (screenDarken.color == new Color(0f, 0f, 0f, 0f))
            {
                isUnDarkening = false;
            }
        }
	}

    public void SetIsUnDarkening(bool val)
    {
        if (val)
        {
            screenDarken.color = new Color(0f, 0f, 0f, 1f);
            GameObject.Find("World").GetComponent<Beat>().SetBPMPaused(true);
            levelNameWiggle.SetActive(true);

            StartCoroutine(WaitToStart(2f, val));
        }
    }

    IEnumerator WaitToUnpausedBPM(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Find("World").GetComponent<Beat>().SetBPMPaused(false);
    }

    IEnumerator WaitToStart(float time, bool val)
    {
        yield return new WaitForSeconds(time);
        isUnDarkening = val;
    }

    public void SetIsDarkening(bool val)
    {
        isDarkening = val;
    }

    public bool GetIsDarkening()
    {
        return isDarkening;
    }

    public void SetShowFloorClear(bool val)
    {
        showFloorClear = val;
        if (val)
        {
            GetComponent<FloorClearScreen>().ShowFloorClearScreen();
        }
    }
}
