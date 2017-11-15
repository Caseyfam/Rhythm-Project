using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesTracks : MonoBehaviour {

    private float bpm;
    private float originalX;

    public float distanceUntilReset = 9.95f;

    void Awake()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        originalX = transform.position.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
		transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0.1f, 0f, 0f), bpm / 3000f);
        if (transform.position.x <= originalX - distanceUntilReset)
        {
            transform.position = new Vector3(originalX, transform.position.y, transform.position.z);
        }
	}

}
