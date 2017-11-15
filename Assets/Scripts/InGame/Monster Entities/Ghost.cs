using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : GenericMonster {

    private Animator animator;

    private int MAX_ACTIONS = 3;

    private int tickCounter;
    private int ticksSinceStart = 0;
    private int randomAction;
    private bool actionCompleted = true;

    private float bpm;

    private GameObject fake1, fake2;
    MonsterSpawner monsterSpawner;

    void Awake()
    {
        animator = GetComponent<Animator>();
        LoadElements();
    }

	// Use this for initialization
	void Start ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        monsterSpawner = GameObject.Find("Game Logic").GetComponent<MonsterSpawner>();
        monsterSpawner.SetCanSpawnMonsters(false);
        SetHealth(4);
        tickCounter = SetMonsterTickCounter();

        animator.speed = bpm / 60f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTick()
    {
        if (actionCompleted && tickCounter == 1)
        {
            randomAction = Random.Range(0, MAX_ACTIONS);
            ticksSinceStart = 0;
            actionCompleted = false;
        }
        else if (!actionCompleted)
        {
            switch (randomAction)
            {
                case 0: // IDLE
                    if (ticksSinceStart == 0)
                    {
                        animator.Play("GhostIdle");
                    }
                    if (ticksSinceStart == 2)
                    {
                        actionCompleted = true;
                    }
                    break;
                case 1: // ATTACK
                    switch (ticksSinceStart)
                    {
                        case 0:
                            animator.Play("GhostFlyUp");
                            DestroyFakes();
                            break;
                        case 1:
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
                            break;
                        case 3:
                            animator.Play("GhostAttack");
                            DamageRowPhysical();
                            for (int i = 0; i < 12; i++)
                            {
                                playerTiles[i].GetComponent<TileChangeListener>().UnWarn();
                            }
                            break;
                        case 5:
                            GameObject newTile = rightTiles[Random.Range(0, 3)];
                            transform.parent.gameObject.transform.position = new Vector3(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y, newTile.transform.position.z);
                            animator.Play("GhostFlyDown");
                            actionCompleted = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case 2: // Illusion creation
                    switch (ticksSinceStart)
                    {
                        case 0:
                            animator.Play("GhostFakeOut");
                            DestroyFakes();
                            break;
                        case 2:
                            // Create fakes
                            int newTileNumber = Random.Range(0, 3);
                            transform.parent.gameObject.transform.position = new Vector3(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y, rightTiles[newTileNumber].transform.position.z);

                            switch (newTileNumber)
                            {
                                case 0:
                                    fake1 = SpawnFakeGhost(1);
                                    fake2 = SpawnFakeGhost(2);
                                    break;
                                case 1:
                                    fake1 = SpawnFakeGhost(0);
                                    fake2 = SpawnFakeGhost(2);
                                    break;
                                case 2:
                                    fake1 = SpawnFakeGhost(0);
                                    fake2 = SpawnFakeGhost(1);
                                    break;
                                default:
                                    break;
                            }
                            animator.Play("GhostFakeIn");
                            break;
                        case 3:
                            randomAction = 0;
                            ticksSinceStart = -1;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            ticksSinceStart++;
        }
        tickCounter++;
        if (tickCounter >= 2)
        {
            tickCounter = 0;
        }
    }

    private GameObject SpawnFakeGhost(int tileNumber)
    {
        GameObject fakeGhost = (GameObject)Resources.Load("GhostFake");
        fakeGhost = Instantiate(fakeGhost, rightTiles[tileNumber].transform.position, fakeGhost.transform.rotation, GameObject.Find("World").transform);
        fakeGhost.name = "GhostFake" + tileNumber;
        //fakeGhost.transform.parent = GameObject.Find("World").transform;
        //fakeGhost.transform.position = rightTiles[tileNumber].transform.position;
        return fakeGhost;
    }

    private void DestroyFakes()
    {
        try
        {
            Destroy(fake1);
        }
        catch
        {

        }
        try
        {
            Destroy(fake2);
        }
        catch
        {

        }
    }

    void MonsterDeath()
    {
        DestroyFakes();
        monsterDeathVal.UpdateMonsterPoints("Ghost");
        stats.AddToMonstersDefeated(1);
        monsterSpawner.SetCanSpawnMonsters(true);
    }
}
