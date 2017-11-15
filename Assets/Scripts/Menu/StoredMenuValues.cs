using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredMenuValues : MonoBehaviour {

    private string[] playerType = new string[4];
    private string[] fighterClass = new string[4];   

    public void SetPlayerType(int playerNum, string type)
    {
        playerType[playerNum - 1] = type;
    }

    public string GetPlayerType(int playerNum)
    {
        try
        {
            return playerType[playerNum - 1];
        }
        catch
        {
            return "Player";
        }
    }

    public void SetFighterClass(int playerNum, string desiredClass)
    {
        fighterClass[playerNum - 1] = desiredClass;
    }

    public string GetFighterClass (int playerNum)
    {
        try
        {
            return fighterClass[playerNum - 1];
        }
        catch
        {
            return "Wizard"; 
        }
    }
}
