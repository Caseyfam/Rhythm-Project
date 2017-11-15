using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderColorLogic : MonoBehaviour {

    private Color playerColor;
    public PlayerMenuImageSetter playerMenuImageSetter;
    public int playerNumber;
    public StoredColors storedColors;

    public void CreatePlayerColor(int r, int g, int b, float divisor)
    {
        playerColor = new Color(r / divisor, g / divisor, b / divisor);
        playerMenuImageSetter.UpdatePlayerColor(playerColor);
        storedColors.SetColor(playerNumber, playerColor);
    }

    public Color GetPlayerColor()
    {
        return playerColor;
    }
	
}
