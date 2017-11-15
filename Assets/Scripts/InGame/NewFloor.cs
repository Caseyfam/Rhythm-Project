using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFloor : MonoBehaviour {

    private int sceneNumber;
    public string nextFloor;
    StoredPlayerHealth storedHealth;

    // Use this for initialization
    void Start ()
    {
        sceneNumber = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        storedHealth = GameObject.Find("Passed Items").GetComponent<StoredPlayerHealth>();
    }

    public void LoadScene()
    {
        int nextSceneIndex;
        switch (nextFloor)
        {
            case "Foyer":
                nextSceneIndex = 2;
                break;
            case "Caves":
                nextSceneIndex = 3;
                break;
            case "SpiderBoss":
                nextSceneIndex = 4;
                break;
            case "Mines":
                nextSceneIndex = 5;
                break;
            default:
                nextSceneIndex = 0;
                break;
        }
        SaveAllStats();
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
    }

    void ReviveTheDead()
    {
        GameObject[] storedPlayers = GetComponent<PlayerStorer>().GetPlayers();

        foreach(GameObject player in storedPlayers)
        {
            try
            {
                if (player.GetComponent<PlayerHealth>().GetHealth() <= 0)
                {
                    player.GetComponent<PlayerHealth>().SetHealth(1);
                }
            }
            catch
            {

            }
        }
    }

    void SaveAllStats()
    {
        // Need to save player health for next floor
        GameObject[] storedPlayers = GetComponent<PlayerStorer>().GetPlayers();
        
        foreach (GameObject player in storedPlayers)
        {
            try
            {
                storedHealth.SetHealth(player.GetComponent<PlayerVals>().GetPlayerNumber(), player.GetComponent<PlayerHealth>().GetHealth());
            }
            catch
            {
                // Trying to access health on null players
            }
        }
    }
	
}
