using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCavesMovement : MonoBehaviour {

    private float bpm;

	// Use this for initialization
	void Awake ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0.1f, 0f, 0f), bpm / 3000f);	
	}
}
