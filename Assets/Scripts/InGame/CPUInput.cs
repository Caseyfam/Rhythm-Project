using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUInput : MonoBehaviour
{

    private int tickCounter = 0;
    private int currentTile;
    private int threatTile;

    private bool performedMovement = false;
    private bool performedAction = false;

    private Vector3[] modifiedTileDestinations;
    private PlayerVals playerVals;
    private GlobalVariables globalVars;
    private MonsterSpawner monsterSpawner;
    private GameObject[] playerTiles = new GameObject[12];
    private GameObject currentMonster;


    void Start()
    {
        playerVals = GetComponent<PlayerVals>();
        globalVars = GameObject.Find("Game Logic").GetComponent<GlobalVariables>();
        monsterSpawner = GameObject.Find("Game Logic").GetComponent<MonsterSpawner>();
        playerTiles[0] = GameObject.Find("Floor Tile 0");
        playerTiles[1] = GameObject.Find("Floor Tile 1");
        playerTiles[2] = GameObject.Find("Floor Tile 2");
        playerTiles[3] = GameObject.Find("Floor Tile 3");
        playerTiles[4] = GameObject.Find("Floor Tile 4");
        playerTiles[5] = GameObject.Find("Floor Tile 5");
        playerTiles[6] = GameObject.Find("Floor Tile 6");
        playerTiles[7] = GameObject.Find("Floor Tile 7");
        playerTiles[8] = GameObject.Find("Floor Tile 8");
        playerTiles[9] = GameObject.Find("Floor Tile 9");
        playerTiles[10] = GameObject.Find("Floor Tile 10");
        playerTiles[11] = GameObject.Find("Floor Tile 11");
    }

    void FixedUpdate()
    {
        currentTile = GetComponent<ChangeSpaces>().GetCurrentTile();

        if (!globalVars.GetPlayerTickPaused())
        {
            if (tickCounter == 1)
            {
                for (int i = 0; i < monsterSpawner.monsterTiles.Length; i++)
                {
                    if (monsterSpawner.monsterTiles[i].GetComponent<MonsterTileListener>().HasMonsterOnTile())
                    {
                        threatTile = i;
                        currentMonster = monsterSpawner.monsterTiles[i].GetComponent<MonsterTileListener>().GetMonsterOnTile();
                    }
                }
                if (!performedAction)
                {
                    if (playerTiles[currentTile].GetComponent<TileChangeListener>().GetTileWarning() == "Physical")
                    {
                        GetComponent<PlayerDefend>().SetPlayerDefending(true);
                        performedAction = true;
                    }
                    else if (CheckIfInSameRow(threatTile))
                    {
                        if (currentMonster != null)
                        {
                            if (!currentMonster.GetComponent<GenericMonster>().GetInvincibility())
                            {
                                switch (GetComponent<PlayerVals>().GetFighterClass())
                                {
                                    case "Wizard":
                                        GetComponent<PlayerAttack>().Attack();
                                        performedAction = true;
                                        break;
                                    case "Knight":
                                        if (currentTile == 9 || currentTile == 10 || currentTile == 11)
                                        {
                                            GetComponent<PlayerAttack>().Attack();
                                            performedAction = true;
                                        }
                                        break;
                                    default:
                                        Debug.Log("You haven't programmed when this class should attack in CPUInput");
                                        break;
                                }
                            }
                        }
                    }
                }
                

                bool isInDanger = false;
                for (int i = 0; i < playerTiles.Length; i++)
                {
                    if (playerTiles[i].GetComponent<TileChangeListener>().GetTileWarning() != null)
                    {
                        isInDanger = true;
                    }
                }
                if (isInDanger)
                {
                    AvoidDangerTiles();
                }
                else
                {

                }
                GetComponent<ChangeSpaces>().SetTileDestinations(modifiedTileDestinations);
            }
        }
    }
    void AvoidDangerTiles()
    {
        int nextTileUpper = 0;
        int nextTileMiddle = 0;
        int nextTileLower = 0;
        switch (currentTile)
        {
            case 0:
            case 1:
            case 2:
                nextTileUpper = 3;
                nextTileMiddle = 4;
                nextTileLower = 5;
                break;
            case 3:
            case 4:
            case 5:
                nextTileUpper = 6;
                nextTileMiddle = 7;
                nextTileLower = 8;
                break;
            case 6:
            case 7:
            case 8:
                nextTileUpper = 9;
                nextTileMiddle = 10;
                nextTileLower = 11;
                break;
            case 9:
            case 10:
            case 11:
                nextTileUpper = 0;
                nextTileMiddle = 1;
                nextTileLower = 2;
                break;
        }

        switch (currentTile)
        {
            case 0:
                AdjustAvoid("TOP", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 1:
                AdjustAvoid("MIDDLE", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 2:
                AdjustAvoid("BOTTOM", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 3:
                AdjustAvoid("TOP", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 4:
                AdjustAvoid("MIDDLE", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 5:
                AdjustAvoid("BOTTOM", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 6:
                AdjustAvoid("TOP", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 7:
                AdjustAvoid("MIDDLE", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 8:
                AdjustAvoid("BOTTOM", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 9:
                AdjustAvoid("TOP", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 10:
                AdjustAvoid("MIDDLE", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
            case 11:
                AdjustAvoid("BOTTOM", nextTileUpper, nextTileMiddle, nextTileLower);
                break;
        }
    }

    void AdjustAvoid(string position, int nextTileUpper, int nextTileMiddle, int nextTileLower)
    {
        switch (position)
        {    
            case "TOP":
                if (playerTiles[nextTileUpper].GetComponent<TileChangeListener>().GetTileWarning() != null)
                {
                    SpoofCPUInput("DOWN");
                }
                else if (playerTiles[nextTileMiddle].GetComponent<TileChangeListener>().GetTileWarning() != null)
                {
                    SpoofCPUInput("NONE");
                }
                break;
            case "MIDDLE":
                if (playerTiles[nextTileUpper].GetComponent<TileChangeListener>().GetTileWarning() != null)
                {
                    if (playerTiles[nextTileMiddle].GetComponent<TileChangeListener>().GetTileWarning() != null)
                    {
                        SpoofCPUInput("DOWN");
                    }
                    else
                    {
                        SpoofCPUInput("NONE");
                    }
                }
                else if (playerTiles[nextTileMiddle].GetComponent<TileChangeListener>().GetTileWarning() != null)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            SpoofCPUInput("UP");
                            break;
                        case 1:
                            SpoofCPUInput("DOWN");
                            break;
                    }
                }
                else if (playerTiles[nextTileLower].GetComponent<TileChangeListener>().GetTileWarning() != null)
                {
                    SpoofCPUInput("NONE");
                }
                break;
            case "BOTTOM":
                if (playerTiles[nextTileMiddle].GetComponent<TileChangeListener>().GetTileWarning() != null)
                {
                    SpoofCPUInput("NONE");
                }
                else if (playerTiles[nextTileLower].GetComponent<TileChangeListener>().GetTileWarning() != null)
                {
                    SpoofCPUInput("UP");
                }
                break;
        }
    } 

    bool CheckIfInSameRow(int tileNumber)
    {
        switch (tileNumber)
        {
            case 0:
                if (currentTile == 0 || currentTile == 3 || currentTile == 6 || currentTile == 9)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 1:
                if (currentTile == 1 || currentTile == 4 || currentTile == 7 || currentTile == 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (currentTile == 2 || currentTile == 5 || currentTile == 8 || currentTile == 11)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return true;
        }
    }

    void SpoofCPUInput(string input)
    {
        if (!performedMovement)
        {
            switch (input)
            {
                case "DOWN":
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
                    break;
                case "UP":
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
                    break;
                case "NONE":
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
                    break;
                default:
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
                    break;
            }
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

            performedMovement = false;
            performedAction = false;
        }
    }

    public void SkipTurn()
    {
        tickCounter = 2;
    }
}
