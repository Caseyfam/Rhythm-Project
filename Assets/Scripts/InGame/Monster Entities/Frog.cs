using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : GenericMonster {

    private int tickCounter;
    private int lickCounter = 1;
    private int stomachCounter = 0;


    private SpriteRenderer sr;
    private bool licking = false;
    private bool fullStomach = false;

    private bool playerShouldMove = false;

    public Sprite idle, idleFull, lick, jump, jumpFull, release;

    private GameObject targetPlayer;

    public FrogTongueStretch tongueScript;

	// Use this for initialization
	void Start ()
    {
        tickCounter = SetMonsterTickCounter();
        SetHealth(4);
        sr = GetComponent<SpriteRenderer>();
        moveOffset = new Vector3(0f, 0.4f, 0.4f);
	}

    void Update()
    {
        if (targetPlayer != null && playerShouldMove)
        {
            targetPlayer.transform.position = Vector3.MoveTowards(targetPlayer.transform.position, transform.position, 0.1f);
            if (targetPlayer.transform.position == transform.position)
            {
                targetPlayer.GetComponent<SpriteRenderer>().enabled = false;
                targetPlayer.GetComponentInChildren<PlayerHeartDisplay>().gameObject.transform.position = GameObject.Find("Offscreen Health Point").transform.position;
                fullStomach = true;
            }
        }
    }

    void CheckJumpSprite()
    {
        if (!GetMoving())
        {
            if (fullStomach)
            {
                sr.sprite = idleFull;
            }
            else
            {
                sr.sprite = idle;
            }
        }
    }

    void OnTock()
    {
        CheckJumpSprite();
    }

    void OnTick()
    {
        if (!fullStomach)
        {
            if (!licking && tickCounter == 1 && sr.sprite != release)
            {
                if (Random.Range(0, 4) == 0)
                {
                    // Lick attack
                    licking = true;
                }
                else
                {
                    // Jump spaces
                    if (fullStomach)
                    {
                        sr.sprite = jumpFull;
                    }
                    else
                    {
                        sr.sprite = jump;
                    }
                    MoveToTileNotIncluding(GetCurrentTile());
                }
            }
            else
            {
                if (licking)
                {
                    switch (lickCounter)
                    {
                        case 1:
                            WarnRowMagical();
                            break;
                        case 2:
                            break;
                        case 3:
                            // Do the lick
                            sr.sprite = lick;
                            try
                            {
                                targetPlayer = FindTarget().GetComponentInChildren<SpriteRenderer>().gameObject;
                                targetPlayer.GetComponent<ChangeSpaces>().SetTransformMoving(false);
                            }
                            catch
                            {

                            }

                            lickCounter = 0;
                            UnWarnAllTiles();
                            licking = false;
                            break;
                    }
                    lickCounter++;
                }

            }
        }
        else // If stomach is full
        {
            stomachCounter++;
            if (tickCounter == 1)
            {
                sr.sprite = jumpFull;
                MoveToTileNotIncluding(GetCurrentTile());
            }
            if (stomachCounter == 7)
            {
                if (targetPlayer != null)
                {
                    sr.sprite = release;
                    targetPlayer.GetComponent<ChangeSpaces>().SetTransformMoving(true);
                    targetPlayer.GetComponentInChildren<PlayerHeartDisplay>().ResetHeartPosition();
                    targetPlayer.GetComponent<SpriteRenderer>().enabled = true;
                    playerShouldMove = false;
                }
                stomachCounter = 0;
                targetPlayer = null;
                fullStomach = false;
            }
        }
        tickCounter++;
        if (tickCounter >= 2)
        {
            tickCounter = 0;
        }
        
    }

    GameObject FindTarget()
    {
        switch (GetCurrentTile())
        {
            case 1:
                if (playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(9);
                    return playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(6);
                    return playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[3].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(3);
                    return playerTiles[3].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[0].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(0);
                    return playerTiles[0].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else
                {
                    tongueScript.StretchTongue(13);
                    return null;
                }
            case 2:
                if (playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(10);
                    return playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(7);
                    return playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[4].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(4);
                    return playerTiles[4].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[1].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(1);
                    return playerTiles[1].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else
                {
                    tongueScript.StretchTongue(13);
                    return null;
                }
            case 3:
                if (playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(11);
                    return playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(8);
                    return playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[5].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(5);
                    return playerTiles[5].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[2].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    tongueScript.StretchTongue(2);
                    return playerTiles[2].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else
                {
                    tongueScript.StretchTongue(13);
                    return null;
                }
            default:
                tongueScript.StretchTongue(13);
                return null;
        }
    }

    void MonsterDamaged()
    {
        if (targetPlayer != null)
        {
            targetPlayer.GetComponent<PlayerHealth>().Damage(1);
        }
    }

    public void SetPlayerShouldMove(bool val)
    {
        playerShouldMove = val;
    }
}
