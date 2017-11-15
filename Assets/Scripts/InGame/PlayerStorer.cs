using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorer : MonoBehaviour
{

    public GameObject[] players;

    public void CreatePlayersArray(int size)
    {
        players = new GameObject[size];
    }

    public void SetPlayer(int playerNumber, GameObject player)
    {
        try
        {
            players[playerNumber - 1] = player;
        }
        catch
        {
            Debug.Log("Player inserted outside array size");
        }
    }

    public GameObject GetPlayer(int playerNumber)
    {
        return players[playerNumber - 1];
    }

    public GameObject[] GetPlayers()
    {
        return players;
    }


}
