using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSwing : MonoBehaviour {

    private float bpm;
    private Vector3 currentRot = new Vector3(0, 180, 0);
    private Vector3 target = new Vector3(0, 180, 50);

    void Awake()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentRot = Vector3.Lerp(currentRot, target, bpm / 4800);
        transform.eulerAngles = currentRot;
	}

    void OnTick()
    {
        target = new Vector3(0, 180, -target.z);
    }
    
}
