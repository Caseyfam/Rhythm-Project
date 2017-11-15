using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : GenericMonster {

    public Sprite[] sprites;
    private int spriteIndex = 2;
    private int tickCounter = 0;
    private int chosenTile = 0;

    private bool isThrowing = false;
    private bool isGoingToThrow = false;
    private bool boneThrow = false;
    private bool boneSpawned = false;

    private float bpm;
    private float startTime, elapsedTime;
    private float trajectoryHeight = 2f;
    private Vector3 startPos;

    private int isGoingToThrowCounter = 0;

    private GameObject bone = null;

	void Start ()
    {
        SetHealth(3);
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
    }
	
    void Update()
    {
        elapsedTime = Time.time - startTime;
        if (boneThrow)
        {
            float currentTime = elapsedTime * (0.7f / (24 / bpm));

            bone.transform.rotation = Quaternion.RotateTowards(bone.transform.rotation, new Quaternion(0f, 0f, Random.Range(-1f, 1f), 0f), bpm / 30);

            Vector3 currentPos = Vector3.Lerp(startPos, playerTiles[chosenTile].transform.position, currentTime);
            currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(currentTime) * Mathf.PI);
            bone.transform.position = currentPos;

            if (bone.transform.position == playerTiles[chosenTile].transform.position)
            {
                playerTiles[chosenTile].GetComponent<TileChangeListener>().UnWarn();
                GameObject player = playerTiles[chosenTile].GetComponent<TileChangeListener>().GetPlayerOnTile();
                if (player != null)
                {
                    if (!player.GetComponentInChildren<PlayerDefend>().GetPlayerDefending())
                    {
                        player.GetComponent<PlayerHealth>().Damage(1);
                    }
                }
                GameObject boneParticleSystem = (GameObject)Instantiate(Resources.Load("BoneBreakParticleSystem"));
                boneParticleSystem.transform.position = bone.transform.position;

                Destroy(bone);
                isGoingToThrowCounter = 0;
                isGoingToThrow = false;
                boneThrow = false;
                boneSpawned = false;
            }
        }
    }


	void OnTick()
    {
        tickCounter++;
        if (tickCounter == 1)
        {
            if (!boneThrow)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[1];

            }
        }
        if (tickCounter == 2)
        {
            if (!boneThrow)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            
            tickCounter = 0;
            if ((Random.Range(0, 3) > 0) && !isThrowing && !boneThrow && !isGoingToThrow)
            {
                isGoingToThrow = true;
                chosenTile = Random.Range(0, 12);
                playerTiles[chosenTile].GetComponent<TileChangeListener>().Warn("Physical");
            }
        }

        if (isGoingToThrow)
        {
            isGoingToThrowCounter++;
            if (isGoingToThrowCounter == 3)
            {
                isThrowing = true;
                isGoingToThrowCounter = 0;
            }
        }
    }

    void OnEighth()
    {
        if (isThrowing)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
            if (spriteIndex == 7)
            {
                if (!boneSpawned)
                {
                    bone = (GameObject)Instantiate(Resources.Load("Bone"));
                    bone.transform.position = transform.position + new Vector3(0.3f, 1.5f, 0f);
                    spriteIndex = 2;
                    startTime = Time.time;
                    startPos = bone.transform.position;
                    boneThrow = true;
                    

                    isThrowing = false;
                    boneSpawned = true;
                }
                
            }
            if (spriteIndex < sprites.Length - 1)
            {
                spriteIndex++;
            }
        }
        
    }

    void MonsterDeath()
    {
        monsterDeathVal.UpdateMonsterPoints("Skeleton");
        stats.AddToMonstersDefeated(1);
        try
        {
            Destroy(bone);
        }
        catch
        {
            // No bone to destroy probably
        }

        try
        {
            playerTiles[chosenTile].GetComponent<TileChangeListener>().UnWarn();
        }
        catch
        {
            
        }
    }
}
