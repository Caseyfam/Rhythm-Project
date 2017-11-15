using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Animator>().Play("Slash1");
        GetComponent<Animator>().speed = GameObject.Find("World").GetComponent<Beat>().GetBPM() / 60f;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
