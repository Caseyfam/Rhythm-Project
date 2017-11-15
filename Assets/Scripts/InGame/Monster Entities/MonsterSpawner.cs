using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterTiles;
    public GameObject[] monsters;
    private bool monsterAlive = false;

    private bool waitTickFlag = false;
    private int waitTickCounter = 0;

    private bool canSpawnMonsters = true;

    void CheckIfMonsters()
    {
        monsterAlive = false;
        for (int i = 0; i < monsterTiles.Length; i++)
        {
            if (monsterTiles[i].GetComponent<MonsterTileListener>().GetMonsterOnTile() != null)
            {
                monsterAlive = true;
            }
        }
    }

    void OnTick()
    {
        CheckIfMonsters();

        if (!waitTickFlag)
        {
            if (monsterAlive == false)
            {
                waitTickCounter++;
                if (waitTickCounter == 2)
                {
                    waitTickFlag = true;
                }
            }
        }
        if (waitTickFlag)
        {
            CheckIfMonsters();
            if (monsterAlive == false && canSpawnMonsters)
            {
                if (monsters.Length > 0)
                {
                    SpawnMonster(monsters[Random.Range(0, monsters.Length)]);
                }       
                waitTickFlag = false;
                waitTickCounter = 0;
            }
        }
    }

    void SpawnMonster(GameObject monster)
    {
        GameObject randomTile = monsterTiles[Random.Range(0, monsterTiles.Length)];
        GetComponent<MonsterInstantiate>().InstantiateMonster(monster, new Vector3(randomTile.transform.position.x, randomTile.transform.position.y, randomTile.transform.position.z), Quaternion.Euler(15f, 0f, 0f));
        // GameObject createdMonster = Instantiate(monster, new Vector3(randomTile.transform.position.x, randomTile.transform.position.y + 0.7f, randomTile.transform.position.z + .3f), Quaternion.Euler(35f, 0f, 0f));
    }

    public void SetCanSpawnMonsters(bool val)
    {
        canSpawnMonsters = val;
    }

    public bool GetCanSpawnMonsters()
    {
        return canSpawnMonsters;
    }
	
}
