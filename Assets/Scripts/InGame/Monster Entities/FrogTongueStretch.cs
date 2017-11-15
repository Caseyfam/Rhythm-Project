using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueStretch : MonoBehaviour {

    private bool isStretching = false;

    private Vector3 originalScale = new Vector3(0f, 1f, 1f);
    private Vector3 targetScale;

    public Frog frogScript;
    public GameObject frogBody;
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition = frogBody.transform.localPosition;
		if (isStretching)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, 0.2f);
            if (transform.localScale == targetScale)
            {
                frogScript.SetPlayerShouldMove(true);
                isStretching = false;
            }
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originalScale, 0.2f);
        }
	}

    void OnTick()
    {
        transform.localPosition = frogBody.transform.localPosition;
    }

    void OnTock()
    {
        transform.localPosition = frogBody.transform.localPosition;
    }

    public void StretchTongue(int tileNumber)
    {
        switch (tileNumber)
        {
            case 0:
            case 1:
            case 2:
                targetScale = new Vector3(10f, 1f, 1f);
                // Set amount to MoveTowards localScale here
                break;
            case 3:
            case 4:
            case 5:
                targetScale = new Vector3(8f, 1f, 1f);
                break;
            case 6:
            case 7:
            case 8:
                targetScale = new Vector3(6f, 1f, 1f);
                break;
            case 9:
            case 10:
            case 11:
                targetScale = new Vector3(4f, 1f, 1f);
                break;
            default:
                targetScale = new Vector3(10f, 1f, 1f);
                break;
        }
        isStretching = true;
    }
}
