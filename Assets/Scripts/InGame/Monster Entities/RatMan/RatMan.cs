using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMan : GenericMonster {

    public Sprite[] sprites;
    private SpriteRenderer sr;

    private Vector3 velocity = Vector3.zero;
    private Vector3 target;
    private Vector3 originalPosition;
    private float originalY;

    private float bpm;
    private int tickCounter;

    private bool isDigging = false;
    private bool isReturning = false;
    private bool isRotating = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    private GameObject[] tilesToAttack;
    private GameObject parentObj;

    // Use this for initialization
    void Start ()
    {
        originalY = transform.localPosition.y;
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        sr = GetComponent<SpriteRenderer>();
        SetHealth(3);
        parentObj = transform.parent.gameObject;
        tickCounter = SetMonsterTickCounter();
    }

    void Dig()
    {
        sr.sprite = sprites[1];
        SetInvincibility(true);
        isDigging = true;
        isMovingUp = true;
        target = new Vector3(transform.localPosition.x, originalY, transform.localPosition.z) + new Vector3(0f, 1f, 0f);
        Debug.Log(transform.localPosition + "/n" + target);
    }
	
    void ReturnTrip()
    {
        sr.sprite = sprites[1];
        isMovingUp = true;
        isReturning = true;
        target = originalPosition + new Vector3(0f, 0.4f, 0f);
    }

	void FixedUpdate ()
    {
		if (isDigging)
        {
            if (isMovingUp)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, bpm / 1500);
                if (transform.localPosition == target)
                {
                    isMovingUp = false;
                    isRotating = true;
                }
            }
            if (isRotating)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, new Quaternion(0f, 0f, 1f, 0f), bpm / 5);
                if (transform.localRotation == new Quaternion(0f, 0f, 1f, 0f))
                {
                    isRotating = false;
                    isMovingDown = true;
                    target = transform.localPosition - new Vector3(0f, 3, 0f);
                }
            }
            if (isMovingDown)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, bpm / 300);
                if (transform.localPosition == target)
                {
                    isMovingDown = false;
                    isDigging = false;
                    DetermineTiles();
                }
            }
        }

        if (isReturning)
        {
            transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            if (isMovingUp)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, bpm / 300);
                if (transform.localPosition == target)
                {
                    isMovingUp = false;
                    isMovingDown = true;
                    target = transform.localPosition - new Vector3(0f, 0.5f, 0f);
                }
            }
            if (isMovingDown)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, bpm / 300);
                if (transform.localPosition == target)
                {
                    isMovingDown = false;
                    SetInvincibility(false);
                    isReturning = false;
                    sr.sprite = sprites[0];
                }
            }
        }
	}

    void OnTick()
    {
        tickCounter++;

        switch (tickCounter)
        {
            case 2:
                // MoveToTileNotIncluding(GetCurrentTile(), parentObj);
                break;
            case 4:
                originalPosition = new Vector3(transform.localPosition.x, originalY, transform.localPosition.z);
                Dig();
                // Rat Man breaks my heart and leaves.
                break;
            case 5: // Enter particle
                GameObject enterParticle = (GameObject)Instantiate(Resources.Load("Dig Particle"));
                var mainPr = enterParticle.GetComponent<ParticleSystem>().main;
                mainPr.duration = 8f;
                enterParticle.transform.parent = GameObject.Find("World").transform;
                enterParticle.transform.position = rightTiles[GetCurrentTile() - 1].transform.position;
                enterParticle.GetComponent<ParticleSystem>().Play();
                break;
            case 8:
                int newTile = Random.Range(1, 3);
                SetCurrentTile(newTile);
                parentObj.transform.position = new Vector3(parentObj.transform.position.x, parentObj.transform.position.y, rightTiles[newTile].transform.position.z);
                break;
            case 11: // Exit particle
                GameObject exitParticle = (GameObject)Instantiate(Resources.Load("Dig Particle"));
                exitParticle.transform.parent = GameObject.Find("World").transform;
                exitParticle.transform.position = rightTiles[GetCurrentTile() - 1].transform.position;
                exitParticle.GetComponent<ParticleSystem>().Play();
                break;
            case 12:
                // Rat Man returns!
                ReturnTrip();

                break;
            case 14:
                tickCounter = 0;
                break;
        }
    }

    void DetermineTiles()
    {
        tilesToAttack = new GameObject[6];
        switch (Random.Range(0,1))
        {
            case 0:
                tilesToAttack[0] = playerTiles[0];
                tilesToAttack[1] = playerTiles[2];
                tilesToAttack[2] = playerTiles[4];
                tilesToAttack[3] = playerTiles[6];
                tilesToAttack[4] = playerTiles[8];
                tilesToAttack[5] = playerTiles[10];
                break;
            case 1:
            default:
                tilesToAttack[0] = playerTiles[1];
                tilesToAttack[1] = playerTiles[3];
                tilesToAttack[2] = playerTiles[5];
                tilesToAttack[3] = playerTiles[7];
                tilesToAttack[4] = playerTiles[9];
                tilesToAttack[5] = playerTiles[11];     
                break;
        }

        for (int i = 0; i < tilesToAttack.Length; i++)
        {
            GameObject minion = (GameObject)Instantiate(Resources.Load("Rat Minion"));
            minion.GetComponentInChildren<RatManMinion>().gameObject.transform.position = tilesToAttack[i].transform.position - new Vector3(0f, 1.3f, 0f);
            minion.transform.parent = GameObject.Find("World").transform;
            minion.GetComponentInChildren<SpriteRenderer>().color = sr.color;
            minion.GetComponentInChildren<RatManMinion>().ReceiveTickCount(i + 6, bpm);
            minion.GetComponentInChildren<RatManMinion>().ReceiveTile(tilesToAttack[i]);
        }
    }

    public void TileAttack(GameObject tile)
    {
        PhysicalAttack(tile);
    }

    void MonsterDeath()
    {
        monsterDeathVal.UpdateMonsterPoints("RatMan");
        stats.AddToBossesDefeated(1);
    }
}
