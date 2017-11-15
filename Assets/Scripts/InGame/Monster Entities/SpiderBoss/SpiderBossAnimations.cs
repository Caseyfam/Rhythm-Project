using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossAnimations : MonoBehaviour {

    public Animation anim;
    private float bpm;

    void Awake()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
    }

    // Use this for initialization
    void Start ()
    {
        anim["SpiderBossIdle"].speed = bpm / 60f;
        // anim.Play()
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
