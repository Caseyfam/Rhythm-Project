using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : GenericMonster {

    public Sprite[] sprites;
    private int tickCounter;

	// Use this for initialization
	void Start ()
    {
        SetHealth(3); // Debug set health

        // VERY IMPORTANT TO SYNC ATTACKS
        tickCounter = SetMonsterTickCounter();
        
	}

    void OnTick()
    {
        tickCounter++;
        switch (tickCounter)
        {
            case 4:
                WarnCorrectTiles();
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 6:
                //globalVars.SetPlayerTickPaused(true);
                break;
            case 7:
                GetComponent<SpriteRenderer>().sprite = sprites[2];

                switch (GetCurrentTile())
                {
                    case 1:
                        PhysicalAttack(playerTiles[9]);
                        break;
                    case 2:
                        PhysicalAttack(playerTiles[10]);
                        break;
                    case 3:
                        PhysicalAttack(playerTiles[11]);
                        break;
                    default:
                        break;
                }
                UnWarnCorrectTiles();
                break;
            case 8:
                //globalVars.SetPlayerTickPaused(false);
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                MoveToTileNotIncluding(GetCurrentTile());
                break;
            case 9:
                //globalVars.SkipPlayersTurn();
                tickCounter = 0;
                break;
            default:
                break;
        }
    }

    void WarnCorrectTiles()
    {
        switch (GetCurrentTile())
        {
            case 1:
                playerTiles[9].GetComponent<TileChangeListener>().Warn("Physical");
                break;
            case 2:
                playerTiles[10].GetComponent<TileChangeListener>().Warn("Physical");
                break;
            case 3:
                playerTiles[11].GetComponent<TileChangeListener>().Warn("Physical");
                break;
            default:
                break;
        }
    }

    void UnWarnCorrectTiles()
    {
        switch (GetCurrentTile())
        {
            case 1:
                playerTiles[9].GetComponent<TileChangeListener>().UnWarn();
                break;
            case 2:
                playerTiles[10].GetComponent<TileChangeListener>().UnWarn();
                break;
            case 3:
                playerTiles[11].GetComponent<TileChangeListener>().UnWarn();
                break;
            default:
                break;
        }
    }

    void MonsterDeath()
    {
        UnWarnCorrectTiles();
        monsterDeathVal.UpdateMonsterPoints("TestMonster");
        stats.AddToMonstersDefeated(1);
    }
}
