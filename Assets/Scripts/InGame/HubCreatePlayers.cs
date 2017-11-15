using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCreatePlayers : MonoBehaviour {

    public GameObject[] playerShells;
    StoredMenuValues storedMenuVals;
    StoredColors storedColors;

    void Awake()
    {
        try
        {
            storedMenuVals = GameObject.Find("Menu Logic").GetComponent<StoredMenuValues>();
            storedColors = GameObject.Find("Menu Logic").GetComponent<StoredColors>();
        }
        catch
        {
            // No Menu Logic in scene
        }

        // Create players
        if (storedMenuVals != null)
        {
            for (int i = 1; i < 4; i++)
            {
                switch (storedMenuVals.GetPlayerType(i))
                {
                    case "Player":
                        GameObject player = SpawnShell(i);
                        player.GetComponentInChildren<HubPlayerInput>().SetInput(i);
                        player.GetComponentInChildren<SpriteRenderer>().color = storedColors.GetColor(i);
                        break;
                    case "CPU":
                        break;
                    case "Off":
                        break;
                }
            }
        }

        
    }

    private GameObject SpawnShell(int currentEntry)
    {
        int playerShellNum = 0;
        switch (storedMenuVals.GetFighterClass(currentEntry))
        {
            case "Wizard":
                playerShellNum = 0;
                break;
            case "Knight":
                playerShellNum = 1;
                break;
            default:
                playerShellNum = 0;
                break;
        }

        GameObject shell = Instantiate(playerShells[playerShellNum]);
        return shell;
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
