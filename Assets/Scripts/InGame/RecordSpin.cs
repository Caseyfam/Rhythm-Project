using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordSpin : MonoBehaviour
{
    private float bpm;
    private Quaternion target;
    void Awake()
    {
        bpm = GetComponentInParent<Beat>().GetBPM();
    }
	
	void OnTick()
    {
        target = transform.rotation * Quaternion.Euler(0, 0, 90);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, bpm / 22);
    }
}
