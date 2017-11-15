using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

    private bool playerTickPaused = false;
    private ChangeSpaces changeSpaces;
    private PlayerStorer playerStorer;

    void Start()
    {
        playerStorer = GetComponent<PlayerStorer>();
    }

	public bool GetPlayerTickPaused()
    {
        return playerTickPaused;
    }

    public void SetPlayerTickPaused(bool val)
    {
        playerTickPaused = val;
    }

    public void SkipPlayersTurn()
    {
        for (int i = 0; i < 4; i++)
        {
            try
            {
                playerStorer.GetPlayers()[i].GetComponent<ChangeSpaces>().SkipTurn();

                // If these are stopped, player can attack when about to be hit
                // Important to allow non-getting-hit players to attack in the future

                // Tried some debug stuff, it actually puts the non-getting-hit players
                // on the off-beat, so that's... not good...

                playerStorer.GetPlayers()[i].GetComponent<PlayerVals>().SkipTurn();
                try
                {
                    playerStorer.GetPlayers()[i].GetComponent<PlayerInput>().SkipTurn();
                }
                catch
                {
                    try
                    {
                        playerStorer.GetPlayers()[i].GetComponent<CPUInput>().SkipTurn();
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {
                // Player is ded probably
            }
        }
    }
}
