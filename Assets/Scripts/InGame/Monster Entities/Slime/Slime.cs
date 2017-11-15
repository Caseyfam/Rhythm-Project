using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : GenericMonster {

    public Sprite[] sprites;

    private int tickCounter;
    private int previousSlimeTile = 0;
    private int newSlimeTile = 0;
    private SpriteRenderer sr;

    private bool isAttacking = false;

	// Use this for initialization
	void Start ()
    {
        SetHealth(5);
        sr = GetComponent<SpriteRenderer>();
        tickCounter = SetMonsterTickCounter();
	}

    void OnTick()
    {
        tickCounter++;

        switch (tickCounter)
        {
            case -1:
            case 0:
                sr.sprite = sprites[0];
                break;
            case 1:
                sr.sprite = sprites[1];
                break;
            case 2:
                sr.sprite = sprites[2];
                SpawnProjectile();
                break;
            case 3:
            case 4:
                sr.sprite = sprites[0];
                break;
            case 5:
                sr.sprite = sprites[1];
                break;
            case 6:
                sr.sprite = sprites[2];
                SpawnProjectile();
                break;
            case 7:
                sr.sprite = sprites[0];
                tickCounter = -1;
                break;
        }
    }

    void SpawnProjectile()
    {
        GameObject projectile = (GameObject)Instantiate(Resources.Load("Slime Projectile"));
        projectile.transform.parent = GameObject.Find("World").transform;
        projectile.transform.position = transform.position;
        projectile.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        FindSlimeTile();
        projectile.GetComponent<SlimeProjectile>().FireSlime(GameObject.Find("Floor Tile " + newSlimeTile));
        previousSlimeTile = newSlimeTile;
    }

    void FindSlimeTile()
    {
        newSlimeTile = Random.Range(0, 11);
        if (newSlimeTile == previousSlimeTile)
        {
            FindSlimeTile();
        }
        else
        {
            GameObject.Find("Floor Tile " + newSlimeTile).GetComponent<TileChangeListener>().Warn("Magic");
        }
    }

    void MonsterDeath()
    {
        monsterDeathVal.UpdateMonsterPoints("Slime");
        stats.AddToMonstersDefeated(1);
    }
	
	
}
