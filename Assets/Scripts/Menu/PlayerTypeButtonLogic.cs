using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeButtonLogic : MonoBehaviour {

    public int playerNumber;
    UnityEngine.UI.Text buttonText;
    StoredMenuValues storedMenuVals;

    void Start()
    {
        buttonText = GetComponentInChildren<UnityEngine.UI.Text>();
        storedMenuVals = GameObject.Find("Menu Logic").GetComponent<StoredMenuValues>();
        storedMenuVals.SetPlayerType(playerNumber, buttonText.text);
    }

	public void ChangeType()
    {
        switch (buttonText.text)
        {
            case "CPU":
                buttonText.text = "Player";
                break;
            case "Player":
                buttonText.text = "Off";
                break;
            case "Off":
                buttonText.text = "CPU";
                break;
            default:
                break;
        }
        storedMenuVals.SetPlayerType(playerNumber, buttonText.text);
    }
}
