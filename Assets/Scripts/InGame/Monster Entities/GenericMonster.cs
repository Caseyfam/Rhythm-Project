using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonster : MonoBehaviour
{

    int health;
    public GameObject[] rightTiles;
    public GameObject[] playerTiles;
    public GlobalVariables globalVars;
    public MonsterDeathValue monsterDeathVal;
    public PlaythroughStats stats;
    public Vector3 moveOffset = new Vector3(0f, 0.7f, 0f);

    public GameObject gameLogic;
    private int currentTile;

    private bool playerIsMoving = false;
    bool moving = false;
    private bool isInvincible = false;
    private bool environmentShift = true;
    private float increment = 0f;
    private int destinationTile = 1;

    private GameObject playerThatDamagedMonster;
    private GameObject movingThing;
    private Camera newCam;

    public bool flinching = false;

    void Awake()
    {
        LoadElements();
    }

    public void LoadElements()
    {
        gameLogic = GameObject.Find("Game Logic");
        globalVars = GameObject.Find("Game Logic").GetComponent<GlobalVariables>();
        newCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        rightTiles = new GameObject[3]; // Current hardcoded since I don't know how to add these to an array in the Inspector
        rightTiles[0] = GameObject.Find("Floor Tile M1");
        rightTiles[1] = GameObject.Find("Floor Tile M2");
        rightTiles[2] = GameObject.Find("Floor Tile M3");
        playerTiles = new GameObject[12];
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

        monsterDeathVal = GameObject.Find("Game Logic").GetComponent<MonsterDeathValue>();
        try
        {
            stats = GameObject.Find("Passed Items").GetComponent<PlaythroughStats>();
        }
        catch
        {

        }
    }

    void Start()
    {
        GameObject[] players = GameObject.Find("Game Logic").GetComponent<PlayerStorer>().GetPlayers();
        foreach (GameObject player in players)
        {
            try
            {
                if (player.GetComponent<ChangeSpaces>().GetIsMoving())
                {
                    playerIsMoving = true;
                }
            }
            catch
            {

            }
        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            float bpm = GetComponentInParent<Beat>().GetBPM();
            increment += Time.deltaTime / (bpm / 120);
            movingThing.transform.position = Vector3.Lerp(movingThing.transform.position, rightTiles[destinationTile - 1].transform.position + moveOffset, increment);
            if (movingThing.transform.position == rightTiles[destinationTile - 1].transform.position + moveOffset)
            {
                moving = false;
            }
        }
    }

    public void MoveToTile(int tileNumber)
    {
        destinationTile = tileNumber;
        increment = 0f;
        movingThing = this.gameObject;
        moving = true;
    }

    public void MoveToTile(int tileNumber, GameObject mover)
    {
        MoveToTile(tileNumber);
        movingThing = mover;
    }

    public void MoveToTileNotIncluding(int tileToExclude)
    {
        int[] random = new int[2];
        switch (tileToExclude)
        {
            case 1:
                random[0] = 2;
                random[1] = 3;
                break;
            case 2:
                random[0] = 1;
                random[1] = 3;
                break;
            case 3:
                random[0] = 1;
                random[1] = 2;
                break;
            default:
                break;
        }
        destinationTile = random[Random.Range(0, 2)];
        increment = 0f;
        movingThing = this.gameObject;
        moving = true;
    }

    public void MoveToTileNotIncluding(int tileToExclude, GameObject mover)
    {
        MoveToTileNotIncluding(tileToExclude);
        movingThing = mover;
    }

    public int SetMonsterTickCounter()
    {
        if (playerIsMoving)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetCurrentTile (int currentTile)
    {
        this.currentTile = currentTile;
    }

    public int GetCurrentTile()
    {
        return currentTile;
    }

    public void SetHealth(int health)
    {
        this.health = health;
        CheckIfDead();
    }

    public void SetInvincibility(bool flag)
    {
        isInvincible = flag;
    }

    public bool GetInvincibility()
    {
        return isInvincible;
    }

    public void DamageMonster(GameObject player, int damage)
    {
        if (!isInvincible)
        {
            flinching = true;
            playerThatDamagedMonster = player;
            BroadcastMessage("MonsterDamaged");
            try
            {
                GameObject.Find("Main Camera").GetComponent<CameraShake>().SetCameraShake(36 / GetComponentInParent<Beat>().GetBPM());
            }
            catch
            {

            }

            health = health - damage;
            CheckIfDead();

            try
            {
                GetComponent<DamageFlicker>().DoDamageFlicker();
            }
            catch
            {

            }
        }
    }

    public GameObject GetPlayerThatDamagedMonster()
    {
        try
        {
            return playerThatDamagedMonster;
        }
        catch
        {
            return null;
        }
    }

    public GameObject[] GetRightTiles()
    {
        return rightTiles;
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            BroadcastMessage("MonsterDeath");
            if (environmentShift)
            {
                CallEnvironmentShift();
            }

            Destroy(this.gameObject);
            try // If we're using the "tween effect with parent" hack
            {
                if (this.gameObject.transform.parent.name != "World")
                {
                    Destroy(this.gameObject.transform.parent.gameObject);
                }
            }
            catch
            {

            }
        }
    }

    public void PhysicalAttack(GameObject tileToHit)
    {
        GameObject playerToHit = tileToHit.GetComponent<TileChangeListener>().GetPlayerOnTile();
        tileToHit.GetComponent<TilePlayerStatus>().SetTileBeingAttackedState(true);
        if(playerToHit != null)
        {
            if (!playerToHit.GetComponent<PlayerDefend>().GetPlayerDefending())
            {
                playerToHit.GetComponent<PlayerHealth>().Damage(1); // Placeholder damage
                playerToHit.GetComponent<DamageFlicker>().DoDamageFlicker();
                try
                {
                    newCam.GetComponent<CameraShake>().SetCameraShake(36 / GetComponentInParent<Beat>().GetBPM());
                }
                catch
                {
                    Debug.Log("Can't shake that screen!");
                }
               
            }
        }
    }

    public void MagicalAttack(GameObject tileToHit)
    {
        GameObject playerToHit = tileToHit.GetComponent<TileChangeListener>().GetPlayerOnTile();
        tileToHit.GetComponent<TilePlayerStatus>().SetTileBeingAttackedState(true);
        if(playerToHit != null)
        {
            playerToHit.GetComponent<PlayerHealth>().Damage(1);
            playerToHit.GetComponent<DamageFlicker>().DoDamageFlicker();
            newCam.GetComponent<CameraShake>().SetCameraShake(36 / GetComponentInParent<Beat>().GetBPM());
        }
    }

    void OnDestroy()
    {
        Debug.Log(name + " destroyed");
    }

    void CallEnvironmentShift()
    {
        GameObject.Find("Environment Logic").GetComponent<EnvironmentShift>().ShiftEnvironment();
    }

    public void DamageRowPhysical()
    {
        switch (GetCurrentTile())
        {
            case 1:
                playerTiles[0].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[3].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[6].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[9].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                break;
            case 2:
                playerTiles[1].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[4].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[7].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[10].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                break;
            case 3:
                playerTiles[2].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[5].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[8].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                playerTiles[11].GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                break;
            default:
                break;
        }
    }

    public void DamageRowMagical()
    {
        switch (GetCurrentTile())
        {
            case 1:
                playerTiles[0].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[3].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[6].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[9].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                break;
            case 2:
                playerTiles[1].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[4].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[7].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[10].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                break;
            case 3:
                playerTiles[2].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[5].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[8].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                playerTiles[11].GetComponent<TileChangeListener>().DamagePlayerMagical(1);
                break;
            default:
                break;
        }
    }

    public void WarnRowPhysical()
    {
        switch (GetCurrentTile())
        {
            case 1:
                playerTiles[0].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[3].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[6].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[9].GetComponent<TileChangeListener>().Warn("Physical");
                break;
            case 2:
                playerTiles[1].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[4].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[7].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[10].GetComponent<TileChangeListener>().Warn("Physical");
                break;
            case 3:
                playerTiles[2].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[5].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[8].GetComponent<TileChangeListener>().Warn("Physical");
                playerTiles[11].GetComponent<TileChangeListener>().Warn("Physical");
                break;
            default:
                break;
        }
    }

    public void WarnRowMagical()
    {
        switch (GetCurrentTile())
        {
            case 1:
                playerTiles[0].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[3].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[6].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[9].GetComponent<TileChangeListener>().Warn("Magical");
                break;
            case 2:
                playerTiles[1].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[4].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[7].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[10].GetComponent<TileChangeListener>().Warn("Magical");
                break;
            case 3:
                playerTiles[2].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[5].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[8].GetComponent<TileChangeListener>().Warn("Magical");
                playerTiles[11].GetComponent<TileChangeListener>().Warn("Magical");
                break;
            default:
                break;
        }
    }

    public void UnWarnAllTiles()
    {
        for (int i = 0; i < 12; i++)
        {
            playerTiles[i].GetComponent<TileChangeListener>().UnWarn();
        }
    }

    public void SetEnvironmentShift(bool val)
    {
        environmentShift = val;
    }

    public bool GetMoving()
    {
        return moving;
    }

    void MonsterDamaged()
    {

    }
    
}
