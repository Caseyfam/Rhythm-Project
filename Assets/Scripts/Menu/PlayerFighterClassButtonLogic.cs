using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighterClassButtonLogic : MonoBehaviour {

    public int playerNumber;
    public PlayerMenuImageSetter playerImage;
    UnityEngine.UI.Text buttonText;
    StoredMenuValues storedMenuVals;

    // Use this for initialization
    void Start ()
    {
        buttonText = GetComponentInChildren<UnityEngine.UI.Text>();
        storedMenuVals = GameObject.Find("Menu Logic").GetComponent<StoredMenuValues>();
        storedMenuVals.SetFighterClass(playerNumber, buttonText.text);
	}

    public void ChangeClass()
    {
        switch (buttonText.text)
        {
            case "Wizard":
                buttonText.text = "Knight";
                playerImage.ChangeSpriteToClass("Knight");
                break;
            case "Knight":
                buttonText.text = "Wizard";
                playerImage.ChangeSpriteToClass("Wizard");
                break;
            default:
                Debug.Log("ASDaa");
                break;
        }
        storedMenuVals.SetFighterClass(playerNumber, buttonText.text);
    }
}
