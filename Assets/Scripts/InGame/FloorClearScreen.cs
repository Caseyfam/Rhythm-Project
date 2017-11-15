using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorClearScreen : MonoBehaviour {

    public GameObject floorClearedText, continueButton;

    private bool moveTextUp = false;
    private Vector2 target;

	// Use this for initialization
	void Start ()
    {
        target = new Vector2(floorClearedText.GetComponent<RectTransform>().anchoredPosition.x, Screen.height - 100f);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (moveTextUp)
        {
            floorClearedText.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(floorClearedText.GetComponent<RectTransform>().anchoredPosition, target, 1f);
            Debug.Log(target + "\n" + floorClearedText.GetComponent<RectTransform>().anchoredPosition);
        }	
	}

    public void ShowFloorClearScreen()
    {
        floorClearedText.SetActive(true);
        continueButton.SetActive(true);
        StartCoroutine(textMoveTimer(1.5f));
    }

    IEnumerator textMoveTimer (float time)
    {
        yield return new WaitForSeconds(time);
        moveTextUp = true;
    }

    public void LoadNewScene()
    {
        GameObject.Find("Game Logic").GetComponent<NewFloor>().LoadScene();
    }
}
