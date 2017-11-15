using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWizard : GenericMonster
{
    public Sprite[] idleSprites;
    public Sprite[] attackSprites;
    public Sprite[] flinchSprites;

    private int tickCounter;
    private float bpm;
    private bool movingTiles = false;

    private bool actionCompleted = true;
    private int randomAction;
    private int ticksSinceStart = 0;

    private bool spritingUp = true;
    private SpriteRenderer sr;

    private int currentSpriteIndex = 0;
    private bool attacking = false;

    private bool movingWiz = false;
    private int destinationTileWiz;
    private GameObject movingThingWiz;
    private float startTime, elapsedTime;
    private Vector3 startPos;
    private float trajectoryHeight = 0.5f;

    private bool dontHopAwake = false;
    private bool delayHop = false;

    // Use this for initialization
    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        SetHealth(5);
        tickCounter = SetMonsterTickCounter();
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
	}
	
    void Update()
    {
        elapsedTime = Time.time - startTime;
        if (movingWiz)
        {
            float currentTime = elapsedTime * (0.7f / (12 / bpm));

            Vector3 currentPos = Vector3.Lerp(startPos, rightTiles[destinationTileWiz].transform.position, currentTime);
            currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(currentTime) * Mathf.PI);
            movingThingWiz.transform.position = currentPos;

            if ((Vector3)movingThingWiz.transform.position == rightTiles[destinationTileWiz].transform.position)
            {
                movingWiz = false;
            }
        }
    }

    public void MoveToTileWiz(int tileToExclude)
    {
        int[] random = new int[2];
        switch (tileToExclude)
        {
            case 1:
                random[0] = 1;
                random[1] = 2;
                break;
            case 2:
                random[0] = 0;
                random[1] = 2;
                break;
            case 3:
                random[0] = 0;
                random[1] = 1;
                break;
            default:
                break;
        }
        destinationTileWiz = random[Random.Range(0, 2)];
        movingThingWiz = transform.parent.gameObject;
        movingWiz = true;
    }

    void OnEighth()
    {
        switch(currentSpriteIndex)
        {
            case 0:
            case 1:
            case 2:
                if (!attacking && !flinching)
                {
                    sr.sprite = idleSprites[currentSpriteIndex];
                }
                else if (attacking)
                {
                    sr.sprite = attackSprites[currentSpriteIndex];
                }
                else if (flinching)
                {
                    sr.sprite = flinchSprites[currentSpriteIndex];
                }
                break;
            case 3:
                if (!attacking && !flinching)
                {
                    sr.sprite = idleSprites[1];
                }
                else if (attacking)
                {
                    sr.sprite = attackSprites[1];
                }
                else if (flinching)
                {
                    sr.sprite = flinchSprites[1];
                }
                break;
            case 4:
                if (!attacking && !flinching)
                {
                    sr.sprite = idleSprites[0];
                }
                else if (attacking)
                {
                    sr.sprite = attackSprites[0];
                }
                else if (flinching)
                {
                    sr.sprite = flinchSprites[0];
                }
                break;
        }

        currentSpriteIndex++;

        if (currentSpriteIndex >= 4)
        {
            currentSpriteIndex = 0;
        }

    } // Sprite animation

    void OnTick()
    {
        flinching = false;
        if (tickCounter == 1 && dontHopAwake)
        {
            if (actionCompleted)
            {
                randomAction = Random.Range(0, 3);
                ticksSinceStart = 0;
                actionCompleted = false;
            }
            if (!actionCompleted)
            {
                switch (randomAction)
                {
                    case 0:
                    case 1:
                        // Move spaces
                        delayHop = true;
                        break;
                    case 2:
                        // Attack
                        attacking = true;
                        switch (ticksSinceStart)
                        {
                            case 0:
                                // Warn row
                                WarnRowMagical();
                                break;
                            case 2:
                                GameObject target = FindTarget(GetCurrentTile());
                                GameObject trail = (GameObject)Instantiate(Resources.Load("Wiz Particle Trail"));
                                trail.transform.position = transform.position;
                                if (target != null)
                                { 
                                    trail.GetComponent<WizardParticles>().LaunchParticle(target, GetComponent<SpriteRenderer>().color);
                                    target.GetComponent<PlayerHealth>().Damage(1);
                                }
                                else
                                {
                                    GameObject emptyTarget;
                                    switch (GetCurrentTile())
                                    {
                                        case 1:
                                            emptyTarget = playerTiles[0];
                                            break;
                                        case 2:
                                            emptyTarget = playerTiles[1];
                                            break;
                                        case 3:
                                            emptyTarget = playerTiles[2];
                                            break;
                                        default:
                                            emptyTarget = playerTiles[1];
                                            break;
                                    }
                                    trail.GetComponent<WizardParticles>().LaunchParticle(emptyTarget, GetComponent<SpriteRenderer>().color);
                                }
                                UnWarnAllTiles();
                                attacking = false;
                                actionCompleted = true;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

            }
        }
        if (tickCounter == 0 && delayHop)
        {
            startTime = Time.time;
            startPos = transform.parent.gameObject.transform.position;
            MoveToTileWiz(GetCurrentTile());

            delayHop = false;
            actionCompleted = true;
        }
        ticksSinceStart++;
        tickCounter++;

        if (tickCounter >= 2)
        {
            dontHopAwake = true;
            tickCounter = 0;
        }
    }

    GameObject FindTarget(int currentTile)
    {
        switch (currentTile)
        {
            case 1:
                if (playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if(playerTiles[3].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[3].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if(playerTiles[0].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[0].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else
                {
                    return null;
                }
            case 2:
                if (playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[4].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[4].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[1].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[1].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else
                {
                    return null;
                }
            case 3:
                if (playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[5].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[5].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else if (playerTiles[2].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                {
                    return playerTiles[2].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                else
                {
                    return null;
                }
            default:
                return null;
        }
    }
}
