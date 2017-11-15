using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredColors : MonoBehaviour {

    private Color player1Color, player2Color, player3Color, player4Color;

	void Awake ()
    {
        player1Color = Color.white;
        player2Color = Color.white;
        player3Color = Color.white;
        player4Color = Color.white;
	}

    public Color GetColor(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return player1Color;
            case 2:
                return player2Color;
            case 3:
                return player3Color;
            case 4:
                return player4Color;
            default:
                return player1Color;
        }
    }
    
    public void SetColor(int playerNumber, Color color)
    {
        switch (playerNumber)
        {
            case 1:
                player1Color = color;
                break;
            case 2:
                player2Color = color;
                break;
            case 3:
                player3Color = color;
                break;
            case 4:
                player4Color = color;
                break;
            default:
                break;
        }
    }
}
