using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlayerInput : MonoBehaviour {

    Rigidbody playerRigidBody;
    private int playerSpeed = 10;
    GameObject shell;
    private Vector3 originalScale;

    private string horizontal, vertical;
    private bool inputsSet = false;

	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        shell = GetComponentInChildren<SpriteRenderer>().gameObject;
        originalScale = shell.transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {   
        if (inputsSet)
        {
            if (Input.GetAxis(vertical) > 0.4f)
            {
                playerRigidBody.MovePosition(transform.position + transform.forward * Time.deltaTime * playerSpeed);
            }
            else if (Input.GetAxis(vertical) < -0.4f)
            {
                playerRigidBody.MovePosition(transform.position - transform.forward * Time.deltaTime * playerSpeed);
            }

            if (Input.GetAxis(horizontal) > 0.4f)
            {
                playerRigidBody.MovePosition(transform.position + transform.right * Time.deltaTime * playerSpeed);
                shell.transform.localScale = originalScale;
            }
            else if (Input.GetAxis(horizontal) < -0.4f)
            {
                playerRigidBody.MovePosition(transform.position - transform.right * Time.deltaTime * playerSpeed);
                shell.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
	}

    public void SetInput(int playerNumber)
    {
        if (playerNumber == 1)
        {
            horizontal = "Horizontal";
            vertical = "Vertical";
        }
        else
        {
            horizontal = "Horizontal" + playerNumber;
            vertical = "Vertical" + playerNumber;
        }
        inputsSet = true;
    }
}
