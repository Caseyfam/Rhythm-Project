using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathValue : MonoBehaviour {

    public int pointsToFinish;
    private int currentPoints = 0;

    public bool endOnMonsterPoints = true;

    public void UpdateMonsterPoints(string monsterName)
    {
        switch (monsterName)
        {
            case "TestMonsterMagic":
                currentPoints += 2;
                break;
            case "RatMan":
                currentPoints += 5;
                break;
            case "SpiderBoss":
                currentPoints += 10;
                // Maybe if it's SpiderBoss, call function here to immediately end floor or something
                break;
            case "HeartCollectible":
                currentPoints++;
                break;
            case "TestMonster":
                currentPoints++;
                break;
            case "Bat":
                currentPoints += 2;
                break;
            case "Slime":
                currentPoints += 3;
                break;
            case "Skeleton":
                currentPoints += 2;
                break;
            default:
                currentPoints++;
                break;
        }
        if (endOnMonsterPoints)
        {
            CheckIfNextScene();
        }
        
    }

    private void CheckIfNextScene()
    {
        if (currentPoints >= pointsToFinish)
        {
            GetComponent<MonsterSpawner>().SetCanSpawnMonsters(false);
            GetComponent<ScreenDarken>().SetIsDarkening(true);
            GetComponent<ScreenDarken>().SetShowFloorClear(true);
            GameObject.Find("Environment Logic").GetComponent<EnvironmentShift>().SetCanShift(false);
            Debug.Log("Floor Cleared!");
        }
    }
}
