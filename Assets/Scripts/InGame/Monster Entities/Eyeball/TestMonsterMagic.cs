using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterMagic : GenericMonster {

    private int tickCounter;
    public Sprite[] sprites;

	// Use this for initialization
	void Start ()
    {
        SetHealth(2);
        tickCounter = SetMonsterTickCounter();
	}

    void OnTick()
    {
        tickCounter++;

        // I believe monsters should act on EVEN beats

        switch (tickCounter)
        {
            case 2:
                WarnCorrectTiles();
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 4:
                //globalVars.SetPlayerTickPaused(true);
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                switch (GetCurrentTile())
                {
                    case 1:
                        MagicalAttack(playerTiles[0]);
                        MagicalAttack(playerTiles[3]);
                        MagicalAttack(playerTiles[6]);
                        MagicalAttack(playerTiles[9]);
                        break;
                    case 2:
                        MagicalAttack(playerTiles[1]);
                        MagicalAttack(playerTiles[4]);
                        MagicalAttack(playerTiles[7]);
                        MagicalAttack(playerTiles[10]);
                        break;
                    case 3:
                        MagicalAttack(playerTiles[2]);
                        MagicalAttack(playerTiles[5]);
                        MagicalAttack(playerTiles[8]);
                        MagicalAttack(playerTiles[11]);
                        break;
                    default:
                        break;
                }

                GameObject localLazer = Instantiate((GameObject)Resources.Load("Lazer"), transform);
                localLazer.transform.parent = GameObject.Find("World").transform;
                localLazer.transform.position = transform.position;
                localLazer.transform.localScale = new Vector3(1f, 1f, 1f);
                UnWarnCorrectTiles();
                break;
            case 6:
                //globalVars.SetPlayerTickPaused(false);
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                MoveToTileNotIncluding(GetCurrentTile());
                break;
            case 7:
                //globalVars.SkipPlayersTurn();
                tickCounter = 0;
                break;
        }
    }
	
    void WarnCorrectTiles()
    {
        switch (GetCurrentTile())
        {
            case 1:
                StartCoroutine(WarnWait(0.1f, 9));
                break;
            case 2:
                StartCoroutine(WarnWait(0.1f, 10));
                break;
            case 3:
                StartCoroutine(WarnWait(0.1f, 11));
                break;
            default:
                break;
        }
    }

    IEnumerator WarnWait(float time, int tile)
    {
        yield return new WaitForSeconds(time);
        try
        {
            playerTiles[tile].GetComponent<TileChangeListener>().Warn("Magic");
            if (tile > 0)
            {
                StartCoroutine(WarnWait(0.1f, tile - 3));
            }
        }
        catch
        {

        }
    }

    void UnWarnCorrectTiles()
    {
        switch (GetCurrentTile())
        {
            case 1:
                playerTiles[0].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[3].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[6].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[9].GetComponent<TileChangeListener>().UnWarn();
                break;
            case 2:
                playerTiles[1].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[4].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[7].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[10].GetComponent<TileChangeListener>().UnWarn();
                break;
            case 3:
                playerTiles[2].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[5].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[8].GetComponent<TileChangeListener>().UnWarn();
                playerTiles[11].GetComponent<TileChangeListener>().UnWarn();
                break;
            default:
                break;
        }
    }

    void MonsterDeath()
    {
        UnWarnCorrectTiles();
        monsterDeathVal.UpdateMonsterPoints("TestMonsterMagic");
        stats.AddToMonstersDefeated(1);
    }
}
