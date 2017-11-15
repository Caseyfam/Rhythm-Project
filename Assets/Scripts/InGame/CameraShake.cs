using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    Camera newCam;
    private bool isShaking = false;
    private Vector3 startPosition;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        newCam = GetComponent<Camera>();
        startPosition = newCam.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (isShaking)
        {
            newCam.transform.position = Vector3.SmoothDamp(newCam.transform.position, startPosition + new Vector3(Random.Range(0, 0.1f), Random.Range(0, 0.1f), Random.Range(0, 0.1f)), ref velocity, 0.02f);
        }

	}

    public void SetCameraShake(float time)
    {
        isShaking = true;
        StartCoroutine(ShakeTime(time));
    }

    IEnumerator ShakeTime(float time)
    {
        yield return new WaitForSeconds(time);
        isShaking = false;
        newCam.transform.position = startPosition;
    }
}
