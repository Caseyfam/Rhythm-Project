using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctor : GenericMonster {
    
    public Sprite idle1, idle2, caneOut, armsUp, throwSprite;
    private int tickCounter;

    SpriteRenderer sr;

    private bool canPerformNewAction = true;
    private int ticksSinceStart;
    private int randomAction;
    private int randomRow;

    private int randomTile;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        LoadElements();
    }

	// Use this for initialization
	void Start ()
    {
        SetHealth(15);
        tickCounter = SetMonsterTickCounter();
    }

    void ChangeIdleSprite()
    {
        if (sr.sprite.Equals(idle1))
        {
            sr.sprite = idle2;
        }
        else if (sr.sprite.Equals(idle2))
        {
            sr.sprite = idle1;
        }

        if (sr.sprite.Equals(caneOut))
        {
            sr.sprite = idle1;
        }

        if (sr.sprite.Equals(throwSprite))
        {
            sr.sprite = idle1;
        }

    }

    private void ThrowPotion(string resourceToLoad, string attackType, int tile)
    {
        GameObject potion = (GameObject)Instantiate(Resources.Load(resourceToLoad));
        playerTiles[tile].GetComponent<TileChangeListener>().Warn(attackType);
        potion.transform.parent = GameObject.Find("World").transform;
        potion.transform.position = transform.position;
        potion.GetComponent<PlaguePotionLob>().FirePotion(playerTiles[tile]);
    }

    void OnTick()
    {
        tickCounter++;
        if (tickCounter == 2)
        {
            tickCounter = 0;
        }

        
        if (tickCounter == 1 && canPerformNewAction)
        {
            // Assign random action
            randomAction = Random.Range(0, 4); // Currently, 3 actions exist
            ticksSinceStart = 0;
            canPerformNewAction = false;
        }

        switch (randomAction)
        {
            case 0:
                // Idle phase
                ChangeIdleSprite();
                Debug.Log("Idle");
                canPerformNewAction = true;
                break;
            case 1: // Potion Lob
                switch (ticksSinceStart)
                {
                    case 0:
                        sr.sprite = caneOut;
                        randomTile = Random.Range(0, 12);
                        break;
                    case 1:
                        playerTiles[randomTile].GetComponent<TileChangeListener>().Warn("Physical");
                        break;
                    case 3:
                        playerTiles[randomTile].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                        break;
                    case 4:
                        ChangeIdleSprite();
                        playerTiles[randomTile].GetComponent<TileChangeListener>().UnWarn();
                        canPerformNewAction = true;
                        break;
                    default:
                        break;
                }
                ticksSinceStart++;
                break;
            case 2: // Poison lob
                switch (ticksSinceStart)
                {
                    case 0:
                        randomTile = Random.Range(0, 12);
                        playerTiles[randomTile].GetComponent<TileChangeListener>().Warn("Magical");
                        break;
                    case 2:
                        sr.sprite = throwSprite;
                        ThrowPotion("PlaguePotion", "Magical", randomTile);
                        randomTile = Random.Range(0, 12);
                        playerTiles[randomTile].GetComponent<TileChangeListener>().Warn("Magical");
                        break;
                    case 3:
                        ChangeIdleSprite();
                        break;
                    case 4:
                        sr.sprite = throwSprite;
                        ThrowPotion("PlaguePotion", "Magical", randomTile);
                        randomTile = Random.Range(0, 12);
                        playerTiles[randomTile].GetComponent<TileChangeListener>().Warn("Magical");
                        break;
                    case 5:
                        ChangeIdleSprite();
                        break;
                    case 6:
                        sr.sprite = throwSprite;
                        ThrowPotion("PlaguePotion", "Magical", randomTile);
                        break;
                    case 7:
                        ChangeIdleSprite();
                        canPerformNewAction = true;
                        break;
                    default:
                        break;
                }
                ticksSinceStart++;
                break;
            case 3: // Row of explosion potions
                switch (ticksSinceStart)
                {
                    case 0:
                        randomRow = Random.Range(0, 3);
                        switch (randomRow)
                        {
                            case 0:
                                playerTiles[0].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[3].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[6].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[9].GetComponent<TileChangeListener>().Warn("Physical");
                                break;
                            case 1:
                                playerTiles[1].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[4].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[7].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[10].GetComponent<TileChangeListener>().Warn("Physical");
                                break;
                            case 2:
                                playerTiles[2].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[5].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[8].GetComponent<TileChangeListener>().Warn("Physical");
                                playerTiles[11].GetComponent<TileChangeListener>().Warn("Physical");
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2: // Create and lob the explosion potions
                        sr.sprite = throwSprite;
                        switch (randomRow)
                        {
                            case 0:
                                ThrowPotion("ExplosionPotion", "Physical", 0);
                                ThrowPotion("ExplosionPotion", "Physical", 3);
                                ThrowPotion("ExplosionPotion", "Physical", 6);
                                ThrowPotion("ExplosionPotion", "Physical", 9);
                                break;
                            case 1:
                                ThrowPotion("ExplosionPotion", "Physical", 1);
                                ThrowPotion("ExplosionPotion", "Physical", 4);
                                ThrowPotion("ExplosionPotion", "Physical", 7);
                                ThrowPotion("ExplosionPotion", "Physical", 10);
                                break;
                            case 2:
                                ThrowPotion("ExplosionPotion", "Physical", 2);
                                ThrowPotion("ExplosionPotion", "Physical", 5);
                                ThrowPotion("ExplosionPotion", "Physical", 8);
                                ThrowPotion("ExplosionPotion", "Physical", 11);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        ChangeIdleSprite();
                        canPerformNewAction = true;
                        break;
                    default:
                        break;
                }
                ticksSinceStart++;
                break;
            // Bat storm?
            default:
                break;
        }

        
    }

    void MonsterDeath()
    {
        for (int i = 0; i < 12; i++)
        {
            playerTiles[i].GetComponent<TileChangeListener>().UnWarn();
        }
    }
}
