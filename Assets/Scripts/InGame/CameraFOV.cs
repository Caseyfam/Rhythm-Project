using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour {

    private float desiredFOV;
    private Camera thisCam;

    private bool isZooming = false;

    void Awake()
    {
        thisCam = GetComponent<Camera>();
        desiredFOV = thisCam.fieldOfView;
        thisCam.fieldOfView = 0.01f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (isZooming)
        {
            thisCam.fieldOfView = Mathf.Lerp(thisCam.fieldOfView, desiredFOV + 1f, 0.01f);

            if (thisCam.fieldOfView >= desiredFOV)
            {
                isZooming = false;
                Destroy(gameObject.GetComponent<CameraFOV>());
            }
        }
	}

    public void EntranceZoom()
    {
        isZooming = true;
    }
}
