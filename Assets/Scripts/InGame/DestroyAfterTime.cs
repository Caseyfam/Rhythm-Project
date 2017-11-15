using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float destroyTime = 5f;
    public bool destroyOnStart = true;

	// Use this for initialization
	void Start ()
    {
	    if (destroyOnStart)
        {
            StartCoroutine(DestroyFromTime(destroyTime));
        }	
	}

    IEnumerator DestroyFromTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    public void TimedDestroy(float time)
    {
        StartCoroutine(DestroyFromTime(time));
    }
}
