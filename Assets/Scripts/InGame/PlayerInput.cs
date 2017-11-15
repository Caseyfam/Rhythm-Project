using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private int tickCounter = 0;
    private int preciseTickCounter = 0;
    private int currentTile;

    private bool performedMovement = false;
    private bool preciseTick = false;
    private bool actionPhase = true;

    private Vector3[] modifiedTileDestinations;
    private PlayerVals playerVals;
    private GlobalVariables globalVars;

    private string attackButton = "Attack";
    private string defendButton = "Defend";
    private string verticalAxis = "Vertical";
    private string horizontalAxis = "Horizontal";

    public void SetPlayerInputs(string attackButton, string defendButton, string verticalAxis, string horizontalAxis)
    {
        this.attackButton = attackButton;
        this.defendButton = defendButton;
        this.verticalAxis = verticalAxis;
        this.horizontalAxis = horizontalAxis;
    }

	void Start ()
    {
        playerVals = GetComponent<PlayerVals>();
        globalVars = GameObject.Find("Game Logic").GetComponent<GlobalVariables>();
	}
	
	void Update ()
    {
        currentTile = GetComponent<ChangeSpaces>().GetCurrentTile();

        if(!globalVars.GetPlayerTickPaused())
        {
            CheckMovementInputs();

            if (preciseTick) 
            {
                GetComponent<ChangeSpaces>().SetTileDestinations(modifiedTileDestinations); // Commiting new movement destinations

                if (!playerVals.GetTurnOver())
                {
                    if (Input.GetButtonDown(attackButton))
                    {
                        if (actionPhase)
                        {
                            //Debug.Log("Attack");
                            GetComponent<PlayerAttack>().Attack();
                        }

                        playerVals.SetTurnOver(true);
                    }
                    else if (Input.GetButtonDown(defendButton))
                    {
                        //Debug.Log("Defend");
                        GetComponent<PlayerDefend>().SetPlayerDefending(true);
                    
                        playerVals.SetTurnOver(true);
                    }
                }
            }
        }
    }

    void CheckMovementInputs()
    {
        if (!performedMovement)
        {
            if (Input.GetAxis(verticalAxis) < -0.5f) // DOWN
            {
                if (currentTile != 2 && currentTile != 5 && currentTile != 8 && currentTile != 11)
                {
                    //Debug.Log("Should go DOWN!");
                    try
                    {
                        modifiedTileDestinations[currentTile] = GetComponent<ChangeSpaces>().tiles[currentTile + 4].transform.position;
                    }
                    catch
                    {
                        if (currentTile == 9)
                        {
                            modifiedTileDestinations[9] = GetComponent<ChangeSpaces>().tiles[1].transform.position;
                        }
                        if (currentTile == 10)
                        {
                            modifiedTileDestinations[10] = GetComponent<ChangeSpaces>().tiles[2].transform.position;
                        }
                    }
                }
                else
                {
                    try
                    {
                        modifiedTileDestinations[currentTile] = GetComponent<ChangeSpaces>().tiles[currentTile + 3].transform.position;
                    }
                    catch
                    {
                        modifiedTileDestinations[currentTile] = GetComponent<ChangeSpaces>().tiles[2].transform.position;
                    }
                }
                performedMovement = true;
            }
            else if (Input.GetAxis(verticalAxis) > 0.5f) // UP
            {
                if (currentTile != 0 && currentTile != 3 && currentTile != 6 && currentTile != 9)
                {
                    //Debug.Log("Should go UP!");
                    try
                    {
                        modifiedTileDestinations[currentTile] = GetComponent<ChangeSpaces>().tiles[currentTile + 2].transform.position;
                    }
                    catch
                    {
                        if (currentTile == 10)
                        {
                            modifiedTileDestinations[10] = GetComponent<ChangeSpaces>().tiles[0].transform.position;
                        }
                        if (currentTile == 11)
                        {
                            modifiedTileDestinations[11] = GetComponent<ChangeSpaces>().tiles[1].transform.position;
                        }
                    }
                }
                else
                {
                    try
                    {
                        modifiedTileDestinations[currentTile] = GetComponent<ChangeSpaces>().tiles[currentTile + 3].transform.position;
                    }
                    catch
                    {
                        modifiedTileDestinations[currentTile] = GetComponent<ChangeSpaces>().tiles[0].transform.position;
                    }
                }
                performedMovement = true;
            }
            else // RELEASED JOYSTICK
            {
                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        modifiedTileDestinations[i] = GetComponent<ChangeSpaces>().tiles[i + 3].transform.position;
                    }
                    catch
                    {
                        try
                        {
                            modifiedTileDestinations[i] = GetComponent<ChangeSpaces>().tiles[i - 9].transform.position;
                        }
                        catch
                        {
                            // tiles not initialized yet
                        }
                    }

                }
                performedMovement = true;
            }
        }  
    }

    void OnPreciseTick(bool flag)
    {
        preciseTick = flag;

        preciseTickCounter++;
        if (preciseTickCounter == 2)
        {
            actionPhase = !actionPhase;
            preciseTickCounter = 0;
        }
    }

    void OnTick()
    {
        if (!globalVars.GetPlayerTickPaused())
        {
            tickCounter++;
            if (tickCounter == 3)
            {
                tickCounter = 1;
            }
            if (tickCounter == 1)
            {
                modifiedTileDestinations = GetComponent<ChangeSpaces>().GetTileDestinations();
            }

            //Debug.Log(tickCounter);
        }
        performedMovement = false;
    }

    public void SkipTurn()
    {
        tickCounter = 2;
    }

}
