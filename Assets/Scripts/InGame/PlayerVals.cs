using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVals : MonoBehaviour {

    private bool turnOver;
    private string fighterClass;

    private int tickCounter = 0;
    private bool onSafeSpace = true;
    public int playerNumber = 1;
    private GlobalVariables globalVars;
    private StoredMenuValues menuVals;
    private SpriteRenderer sr;
    private Color playerColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        try
        {
            menuVals = GameObject.Find("Menu Logic").GetComponent<StoredMenuValues>();
            SetFighterClass(menuVals.GetFighterClass(playerNumber));
        }
        catch
        {
            // Setting classes for debug if you didn't come from a menu
            // These filenames are awful but will work for now, just be careful
            // of sprite names.

            switch (sr.sprite.name)
            {
                case "Wizard":
                    SetFighterClass("Wizard");
                    break;
                case "Knight_0":
                    SetFighterClass("Knight");
                    break;
                default:
                    SetFighterClass("Wizard");
                    break;
            }
        }

        globalVars = GameObject.Find("Game Logic").GetComponent<GlobalVariables>();
    }

    public void SetFighterClass(string fighterClass)
    {
        this.fighterClass = fighterClass;
    }

    public string GetFighterClass()
    {
        try
        {
            return fighterClass;
        }
        catch // Used for debugging, sets class to Wizard if loading from TestScene
        {
            return "Wizard";
        }
    }

	public bool GetTurnOver()
    {
        return turnOver;
    }

    public void SetTurnOver(bool val)
    {
        turnOver = val;
    }

    void OnTick()
    {
        if (!globalVars.GetPlayerTickPaused() && onSafeSpace)
        {
            tickCounter++;

            if (tickCounter == 2)
            {
                tickCounter = 0;
            }
            if (tickCounter == 0)
            {
                SetTurnOver(false);
            }
        }
    }

    public void SkipTurn()
    {
        tickCounter = 0;
    }

    public void SetSafeSpace(bool flag)
    {
        onSafeSpace = flag;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void SetColor(Color thisColor)
    {
        playerColor = thisColor;
    }

    public Color GetColor()
    {
        return playerColor;
    }
}
