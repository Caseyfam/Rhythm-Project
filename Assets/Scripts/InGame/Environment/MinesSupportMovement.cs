using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesSupportMovement : MonoBehaviour
{
    public GameObject supportToTell;
    public bool isTeller = false;
    public GameObject target;

    private Vector3 originalPos;
    private Vector3 targetPos;

	// Use this for initialization
	void Awake ()
    {
        originalPos = transform.position;
        try
        {
            targetPos = target.transform.position;
        }
        catch
        {

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isTeller)
        {
            if(transform.position.x <= targetPos.x)
            {
                ApproachedTarget();
            }
        }
	}

    public void ApproachedTarget()
    {
        try
        {
            supportToTell.GetComponent<MinesSupportMovement>().ApproachedTarget();
        }
        catch
        {

        }
        transform.position = originalPos;
    }
}
