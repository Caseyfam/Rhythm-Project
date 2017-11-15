using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartWheelRotate : MonoBehaviour {

    float bpm;
    private bool isSpinning = false;
    private Quaternion target;


	// Use this for initialization
	void Awake ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        isSpinning = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (isSpinning)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * (bpm * 8));
        }
	}

    public void SetIsSpinning(bool flag)
    {
        isSpinning = flag;
    }
}
