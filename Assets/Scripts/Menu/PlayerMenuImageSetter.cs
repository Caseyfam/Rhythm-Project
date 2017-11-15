using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuImageSetter : MonoBehaviour {

    public Sprite[] playerSprites;

    public void UpdatePlayerColor(Color newColor)
    {
        GetComponent<UnityEngine.UI.Image>().color = newColor;
    }

    public void ChangeSpriteToClass(string fighterClass)
    {
        switch (fighterClass)
        {
            case "Wizard":
                GetComponent<UnityEngine.UI.Image>().sprite = playerSprites[0];
                break;
            case "Knight":
                GetComponent<UnityEngine.UI.Image>().sprite = playerSprites[1];
                break;
            default:
                GetComponent<UnityEngine.UI.Image>().sprite = playerSprites[0];
                break;
        }
    }
}
