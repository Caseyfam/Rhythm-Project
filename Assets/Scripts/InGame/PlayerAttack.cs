using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private GameObject[] tiles = new GameObject[3];
    private GameObject[] playerTiles = new GameObject[12];
    private GameObject monsterTarget;
    private GenericMonster currentMonsterVals;
    private ChangeSpaces changeSpaces;
    private PlayerVals playerVals;
    private SpriteRenderer sr;

    private Sprite defaultSprite;
    public Sprite[] sprites;

    private Color playerColor;

    private float bpm;

	// Use this for initialization
	void Start ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 12; i++)
        {
            playerTiles[i] = GameObject.Find("Floor Tile " + i);
        }
        tiles[0] = GameObject.Find("Floor Tile M1");
        tiles[1] = GameObject.Find("Floor Tile M2");
        tiles[2] = GameObject.Find("Floor Tile M3");
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        changeSpaces = GetComponent<ChangeSpaces>();
        playerVals = GetComponent<PlayerVals>();

        try
        {
            playerColor = GameObject.Find("Menu Logic").GetComponent<StoredColors>().GetColor(playerVals.GetPlayerNumber());
        }
        catch
        {
            playerColor = Color.white;
        }
    }

    public void Attack()
    {
        switch (playerVals.GetFighterClass())
        {
            case "Wizard":
                GetComponent<Animator>().Play("WizAttack");
                GameObject playerTarget = null;
                playerTarget = FindPlayerTarget();
                if (playerTarget != null)
                {
                    // Attacking player
                    GameObject trail = (GameObject)Instantiate(Resources.Load("Wiz Particle Trail"));
                    trail.transform.position = transform.position;
                    trail.GetComponent<WizardParticles>().LaunchParticle(playerTarget, playerColor);

                    // Really bare bones attacking here
                    // We don't want to directly change health (what if they do defense up or something?)

                    // This is just for shitty "first concept" purposes
                    // Also what if you tie it into a PlayerStats script or something...
                    playerTarget.GetComponent<PlayerHealth>().Damage(1);
                }
                else
                {
                    monsterTarget = FindWizardsTarget();
                    // If no player, attack monster

                    if (monsterTarget != null)
                    {
                        currentMonsterVals = monsterTarget.GetComponent<GenericMonster>();

                        GameObject trail = (GameObject)Instantiate(Resources.Load("Wiz Particle Trail"));
                        trail.transform.position = transform.position;
                        trail.GetComponent<WizardParticles>().LaunchParticle(monsterTarget, playerColor);

                        // Really bare bones attacking here
                        // We don't want to directly change health (what if they do defense up or something?)

                        // This is just for shitty "first concept" purposes
                        // Also what if you tie it into a PlayerStats script or something...
                        try
                        {
                            currentMonsterVals.DamageMonster(transform.gameObject, 1);
                        }
                        catch
                        {
                            Debug.Log("PlayerAttack.cs didn't like that");
                        }
                        
                    }
                }
                break;
            case "Knight":
                GetComponent<Animator>().Play("KnightAttack");
                GetComponent<Animator>().speed = bpm / 44f;
                GameObject playerTargetKnight = null;
                playerTargetKnight = FindPlayerTarget();
                if (playerTargetKnight != null)
                {
                    GameObject slash = (GameObject)Instantiate(Resources.Load("Slash"));
                    slash.transform.position = playerTargetKnight.transform.position - new Vector3(0f, -0.1f, 0.3f);
                    slash.transform.parent = GameObject.Find("World").transform;
                    slash.transform.parent = GameObject.Find("World").transform;
                    playerTargetKnight.GetComponent<PlayerHealth>().Damage(1);
                }
                else
                {
                    // Find Knight monster target
                    monsterTarget = FindKnightTarget();

                    if (monsterTarget != null)
                    {
                        GameObject slash = (GameObject)Instantiate(Resources.Load("Slash"));
                        slash.transform.position = monsterTarget.transform.position - new Vector3(0f, -0.1f, 0.3f);
                        slash.transform.parent = GameObject.Find("World").transform;

                        currentMonsterVals = monsterTarget.GetComponent<GenericMonster>();

                        currentMonsterVals.DamageMonster(transform.gameObject, 1);
                    }
                }
                break;
        }
    }

    private GameObject FindPlayerTarget()
    {
        switch (playerVals.GetFighterClass())
        {
            case "Wizard":
                switch (GetComponent<ChangeSpaces>().GetCurrentTile())
                {
                    case 0:
                        if (playerTiles[3].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[3].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 1:
                        if (playerTiles[4].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[4].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 2:
                        if (playerTiles[5].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[5].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 3:
                        if (playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[6].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 4:
                        if (playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[7].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 5:
                        if (playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[8].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        else if (playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 6:
                        if (playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[9].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 7:
                        if (playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[10].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    case 8:
                        if (playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                        {
                            return playerTiles[11].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        }
                        break;
                    default:
                        return null;
                }
                break;
            case "Knight":
                GameObject returnTile = null;
                try
                {
                    returnTile = playerTiles[GetComponent<ChangeSpaces>().GetCurrentTile() + 3].GetComponent<TileChangeListener>().GetPlayerOnTile();
                }
                catch
                {
                    returnTile = null;
                }
                return returnTile;
            default:
                return null;
        }
        
        return null;
    }
    
    private GameObject FindWizardsTarget() // Find monster target for Wizard if he's on the rightmost 3 tiles
    {
        GameObject target;

        switch (changeSpaces.GetCurrentTile()) // Attack only if in the same row
        {
            case 0:
            case 3:
            case 6:
            case 9:
                target = tiles[0].GetComponent<MonsterTileListener>().GetMonsterOnTile();
                break;
            case 1:
            case 4:
            case 7:
            case 10:
                target = tiles[1].GetComponent<MonsterTileListener>().GetMonsterOnTile();
                break;
            case 2:
            case 5:
            case 8:
            case 11:
                target = tiles[2].GetComponent<MonsterTileListener>().GetMonsterOnTile();
                break;
            default:
                target = null;
                break;
        }
        return target;
    }

    private GameObject FindKnightTarget()
    {
        GameObject target;

        switch (changeSpaces.GetCurrentTile())
        {
            case 9:
                target = tiles[0].GetComponent<MonsterTileListener>().GetMonsterOnTile();
                break;
            case 10:
                target = tiles[1].GetComponent<MonsterTileListener>().GetMonsterOnTile();
                break;
            case 11:
                target = tiles[2].GetComponent<MonsterTileListener>().GetMonsterOnTile();
                break;
            default:
                target = null;
                break;
        }
        return target;
    }

    void OnTick()
    {
       

        /*
        if ((changeSpaces.GetCurrentTile() == 9 || changeSpaces.GetCurrentTile() == 10 || changeSpaces.GetCurrentTile() == 11))
            // If Wizard is on rightmost 3 tiles
        {
            canAttackMonsterWizard = true;
        }
        else
        {
            canAttackMonsterWizard = false;
        }
        */
    }

    void OnPreciseTick(bool flag)
    {
        if (!flag)
        {
            try
            {
                switch (playerVals.GetFighterClass())
                {
                    case "Wizard":
                        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WizAttack"))
                        {
                            GetComponent<Animator>().Play("WizIdle");
                        }
                        break;
                    case "Knight":
                        try
                        {
                            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("KnightAttack"))
                            {
                                GetComponent<Animator>().Play("KnightIdle");
                            }
                        }
                        catch
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
        }
    }
    
}
