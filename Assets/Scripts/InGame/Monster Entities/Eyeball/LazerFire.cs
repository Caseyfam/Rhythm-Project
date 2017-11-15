using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerFire : MonoBehaviour {

    private bool isMoving = true;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start ()
    {
        targetPosition = transform.position - new Vector3(50f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.2f);
        }
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
	}
}
