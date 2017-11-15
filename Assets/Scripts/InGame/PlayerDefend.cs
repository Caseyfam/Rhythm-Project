using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefend : MonoBehaviour {

    private bool playerDefending = false;
    private int tickCounter = 0;
    private GlobalVariables globalVars;

    public Sprite defend;
    public Sprite idle;

	// Use this for initialization
	void Awake ()
    {
        globalVars = GameObject.Find("Game Logic").GetComponent<GlobalVariables>();    
	}
	
    void OnTick()
    {
        if (!globalVars.GetPlayerTickPaused())
        {
            if (playerDefending)
            {
                tickCounter++;

                if (tickCounter == 2)
                {
                    SetPlayerDefending(false);
                    tickCounter = 0;
                }
            }
        }
    }

    public bool GetPlayerDefending()
    {
        return playerDefending;
    }

    public void SetPlayerDefending(bool playerDefending)
    {
        this.playerDefending = playerDefending;

        switch (GetComponent<PlayerVals>().GetFighterClass())
        {
            case "Wizard":
                if (playerDefending)
                {
                    GetComponent<Animator>().Play("WizDefend");
                }
                else
                {
                    GetComponent<Animator>().Play("WizIdle");
                }
                break;
            case "Knight":
                if (playerDefending)
                {
                    GetComponent<Animator>().Play("KnightDefend");
                }
                else
                {
                    GetComponent<Animator>().Play("KnightIdle");
                }
                break;
        }
    }
}
