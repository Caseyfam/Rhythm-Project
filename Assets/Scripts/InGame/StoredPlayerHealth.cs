using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredPlayerHealth : MonoBehaviour {

    private int[] healthVals = new int[4];

    public void SetHealth(int playerNumber, int health)
    {
        healthVals[playerNumber - 1] = health;
    }

    public int GetHealth(int playerNumber)
    {
        return healthVals[playerNumber - 1];
    }
}
