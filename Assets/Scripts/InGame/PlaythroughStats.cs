using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaythroughStats : MonoBehaviour {

    private int monstersDefeated = 0;
    private int bossesDefeated = 0;

    public void AddToMonstersDefeated(int value)
    {
        monstersDefeated += value;
    }

    public void AddToBossesDefeated(int value)
    {
        bossesDefeated += value;
    }

    public int GetMonstersDefeated()
    {
        return monstersDefeated;
    }

    public int GetBossesDefeated()
    {
        return bossesDefeated;
    }
}
