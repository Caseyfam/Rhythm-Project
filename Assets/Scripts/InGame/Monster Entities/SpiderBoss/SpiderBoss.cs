using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : GenericMonster {

    public const int MAX_ACTIONS = 4;
    private Animation anim;
    private int tickCounter;
    private float bpm;
    private float animationSpeed;
    private int[] miteTiles = new int[4];

    private int randomAction = 0;
    private int ticksSinceStart = 0;
    private int tileToHit = 0;

    private bool inClaws = false;

    private bool canPerformNewAction = true;
    GameObject ceilingSpider = null;
    GameObject grabbedPlayer = null;
    GameObject healthDisplay = null;
    GameObject spiderMinion = null;
    Vector3 originalHealthPosition = Vector3.zero;

    void Awake()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        animationSpeed = bpm / 60f;
        LoadElements();
    }

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();
        SetHealth(20);
        //SetHealth(1);
        SetMonsterTickCounter();
    }

    void Update()
    {
        if (inClaws)
        {
            if (grabbedPlayer != null)
            {
                try
                {
                    grabbedPlayer.transform.position = Vector3.MoveTowards(grabbedPlayer.transform.position, ceilingSpider.transform.Find("SpiderCeilingEntity/PlayerGrabPoint").transform.position, 1f);
                }
                catch
                {

                }
            }
        }
    }
	
	void OnTick()
    {
        tickCounter++; 
        if (tickCounter == 2)
        {
            tickCounter = 0;
        }

        if (canPerformNewAction && tickCounter == 1)
        {
            randomAction = Random.Range(0, MAX_ACTIONS);
            //randomAction = 3;

            ticksSinceStart = 0;
            canPerformNewAction = false;
        }


        switch (randomAction)
        {
            case 0: // IDLE
                anim.Play("SpiderBossIdle");
                anim["SpiderBossIdle"].speed = animationSpeed;
                canPerformNewAction = true;
                break;
            case 1: // SPIDER SWIPE ATTACK
                if (ticksSinceStart == 0)
                {
                    playerTiles[7].GetComponent<TileChangeListener>().Warn("Physical");
                    playerTiles[10].GetComponent<TileChangeListener>().Warn("Physical");
                }
                ticksSinceStart++;
                if (ticksSinceStart == 2)
                {
                    anim.Play("SpiderBossSwipe");
                    anim["SpiderBossSwipe"].speed = animationSpeed;
                }
                if (ticksSinceStart == 3)
                {
                    PhysicalAttack(playerTiles[7]);
                    PhysicalAttack(playerTiles[10]);
                    playerTiles[7].GetComponent<TileChangeListener>().UnWarn();
                    playerTiles[10].GetComponent<TileChangeListener>().UnWarn();
                    canPerformNewAction = true;
                }
                break;
            case 2: // SPIDER GRAB ATTACK

                if (ticksSinceStart == 0)
                {
                    anim.Play("SpiderBossJumpOffscreen");
                    anim["SpiderBossJumpOffscreen"].speed = animationSpeed;
                    SetInvincibility(true);
                }
                ticksSinceStart++;
                if (ticksSinceStart == 2)
                {
                    tileToHit = Random.Range(0, 11);
                    //tileToHit = 7;
                    playerTiles[tileToHit].GetComponent<TileChangeListener>().Warn("Magic");
                }
                if (ticksSinceStart == 4)
                {
                    // DO THE SPIDAH GRABBBB
                    
                    ceilingSpider = (GameObject)Instantiate(Resources.Load("SpiderCeilingEntityShell"));
                    
                    ceilingSpider.transform.position = playerTiles[tileToHit].transform.position + new Vector3(-1.245f, 0f, -1.737f);
                    ceilingSpider.transform.parent = GameObject.Find("World").transform;
                    
                    
                    Animation entityAnim = ceilingSpider.GetComponentInChildren<Animation>();
                    entityAnim["SpiderCeilingGrab"].speed = animationSpeed;
                }
                if (ticksSinceStart == 5)
                {
                    if (playerTiles[tileToHit].GetComponent<TileChangeListener>().GetPlayerOnTile() != null)
                    {
                        grabbedPlayer = playerTiles[tileToHit].GetComponent<TileChangeListener>().GetPlayerOnTile();
                        healthDisplay = grabbedPlayer.GetComponentInChildren<PlayerHeartDisplay>().gameObject;
                        originalHealthPosition = healthDisplay.transform.localPosition;
                        inClaws = true;
                        grabbedPlayer.GetComponent<ChangeSpaces>().SetTransformMoving(false);
                        grabbedPlayer.GetComponent<SpriteStretch>().SetIsStretching(false);
                    }
                    else
                    {
                        grabbedPlayer = null;
                    }
                    playerTiles[tileToHit].GetComponent<TileChangeListener>().UnWarn();
                }
                if (ticksSinceStart == 6)
                {
                    DecideMites();
                    MiteAttack();
                }
                if (ticksSinceStart == 8) // Damage
                {
                    if (grabbedPlayer != null)
                    {
                        healthDisplay.transform.position = GameObject.Find("Offscreen Health Point").transform.position;
                    }
                    CeilingAttack();
                }
                if (ticksSinceStart == 9)
                {
                    MiteDamage();
                }
                if (ticksSinceStart == 11)
                {
                    DecideMites();
                    MiteAttack();
                }
                if (ticksSinceStart == 12) // Damage
                {
                    CeilingAttack();
                }
                if (ticksSinceStart == 14)
                {
                    MiteDamage();
                }
                if (ticksSinceStart == 15)
                {
                    DecideMites();
                    MiteAttack();
                }
                if (ticksSinceStart == 16) // Damage
                {
                    CeilingAttack(); 
                }
                if (ticksSinceStart == 17)
                {
                    MiteDamage();
                }
                if (ticksSinceStart == 19)
                {
                    inClaws = false;
                    if (grabbedPlayer != null)
                    {
                        grabbedPlayer.GetComponent<ChangeSpaces>().SetTransformMoving(true);
                        grabbedPlayer.GetComponent<SpriteStretch>().SetIsStretching(true);
                        healthDisplay.transform.localPosition = originalHealthPosition;
                    }
                    anim.Play("SpiderBossJumpOnscreen");
                    anim["SpiderBossJumpOnscreen"].speed = animationSpeed;
                    canPerformNewAction = true;
                    SetInvincibility(false);
                }
                break;
            case 3: // Spawn spider minion
                if (ticksSinceStart == 0)
                {
                    spiderMinion = (GameObject)Instantiate(Resources.Load("SpiderMinion"));

                    spiderMinion.transform.position = playerTiles[10].transform.position + new Vector3(0f, 6f, 0f);
                    spiderMinion.transform.parent = GameObject.Find("World").transform;

                    spiderMinion.GetComponent<SpiderBossMinion>().CreateSpiderMinion(playerTiles);
                }
                if (spiderMinion == null)
                {
                    canPerformNewAction = true;
                }
                ticksSinceStart++;
               
                break;
            default:
                Debug.Log("Wut");
                break;
        }
    }

    private void CeilingAttack()
    {
        if (grabbedPlayer != null)
        {
            grabbedPlayer.GetComponent<PlayerHealth>().Damage(1);
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<CameraShake>().SetCameraShake(0.5f);
        }
    }
    
    private void DecideMites()
    {
        for (int i = 0; i < 4; i++)
        {
            
            int miteTile = 0;
            switch (i)
            {
                case 0:
                    miteTile = Random.Range(0, 3);
                    break;
                case 1:
                    miteTile = Random.Range(3, 6);
                    break;
                case 2:
                    miteTile = Random.Range(6, 9);
                    break;
                case 3:
                    miteTile = Random.Range(9, 12);
                    break;
            }
            miteTiles[i] = miteTile;
            playerTiles[miteTile].GetComponent<TileChangeListener>().Warn("Physical");
        }
    }
    private void MiteAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject mite = (GameObject)Instantiate(Resources.Load("Falling Mite"));
            mite.transform.position = playerTiles[miteTiles[i]].transform.position + new Vector3(0f, 20f, 0f);
            mite.GetComponent<FallingMite>().MiteFall(playerTiles[miteTiles[i]]);
        }
    }

    private void MiteDamage()
    {
        for (int i= 0; i < 4; i++)
        {
            try
            {
                PhysicalAttack(playerTiles[miteTiles[i]]);
            }
            catch
            {

            }
            playerTiles[miteTiles[i]].GetComponent<TileChangeListener>().UnWarn();
        }
    }

    void MonsterDeath()
    {
        foreach (GameObject tile in playerTiles)
        {
            tile.GetComponent<TileChangeListener>().UnWarn();  
        }
        monsterDeathVal.UpdateMonsterPoints("SpiderBoss");
        stats.AddToBossesDefeated(1);
    }
}
