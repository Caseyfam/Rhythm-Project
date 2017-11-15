using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug3DTextDisplay : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        switch (tag)
        {
            case "Monster Health":
                GetComponent<TextMesh>().text = GetComponentInParent<TestMonster>().GetHealth().ToString();
                break;
            case "Player Health":
                GetComponent<TextMesh>().text = GetComponentInParent<PlayerHealth>().GetHealth().ToString();
                break;
            default:
                break;
        }
	}
}
