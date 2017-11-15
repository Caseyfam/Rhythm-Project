using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    StoredMenuValues menuVals;
    PlayerStorer playerStorer;
    public GameObject[] playerShells;
    public GameObject[] starterTiles = new GameObject[4];

    private int numberOfPlayers = 0;

	// Use this for initialization
	void Awake ()
    {
        playerStorer = GetComponent<PlayerStorer>();
		try
        {
            menuVals = GameObject.Find("Menu Logic").GetComponent<StoredMenuValues>();

            for (int i = 1; i <= 4; i++)
            {
                if (!menuVals.GetPlayerType(i).Equals("Off"))
                {
                    numberOfPlayers++;
                }
            }
            playerStorer.CreatePlayersArray(4);

            for (int i = 1; i <= 4; i++)
            {
                switch (menuVals.GetPlayerType(i))
                {
                    // Remember that i == playerNumber

                    // Need to...
                    // - Instantiate empty shell of a player
                    // - Set player class
                    // - Set player number
                    // - Preferably change player object's name
                    // - Set player inputs using PlayerInput.SetPlayerInputs(string attackButton, string defendButton, string verticalAxis, string horizontalAxis)

                    case "Player":
                        // Debug.Log(i + " is a player");
                        GameObject player = SpawnShell(i);
                        player.GetComponent<PlayerVals>().SetPlayerNumber(i);
                        playerStorer.SetPlayer(i, player);
                        player.transform.parent.gameObject.transform.parent = GameObject.Find("World").transform;
                        player.transform.parent.gameObject.transform.name = "Player" + i;
                        try
                        {
                            player.transform.position = starterTiles[i - 1].transform.position + new Vector3(0f, 0.5f, 0f);
                        }
                        catch
                        {
                            // Could not set player position to starter tile
                        }
                        
                        switch (i) // Set appropriate inputs to specific character
                        {
                            case 1:
                                player.GetComponent<PlayerInput>().SetPlayerInputs("Attack", "Defend", "Vertical", "Horizontal");
                                break;
                            case 2:
                                player.GetComponent<PlayerInput>().SetPlayerInputs("Attack2", "Defend2", "Vertical2", "Horizontal2");
                                break;
                            case 3:
                                player.GetComponent<PlayerInput>().SetPlayerInputs("Attack3", "Defend3", "Vertical3", "Horizontal3");
                                break;
                            case 4:
                                player.GetComponent<PlayerInput>().SetPlayerInputs("Attack4", "Defend4", "Vertical4", "Horizontal4");
                                break;
                            default:
                                Debug.Log("Could not set player inputs correctly for player " + i);
                                break;
                        }

                        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Mines"))
                        {
                            SpawnMinecart(player);
                        }

                        break;
                    case "CPU":
                        // Debug.Log(i + " is a cpu");
                        GameObject cpu = SpawnShell(i);
                        cpu.GetComponent<PlayerVals>().SetPlayerNumber(i);
                        Destroy(cpu.GetComponent<PlayerInput>());
                        cpu.AddComponent<CPUInput>();
                        playerStorer.SetPlayer(i, cpu);
                        cpu.transform.parent.gameObject.gameObject.transform.parent = GameObject.Find("World").transform;
                        cpu.transform.parent.gameObject.gameObject.transform.name = "Player" + i;
                        try
                        {
                            cpu.transform.position = starterTiles[i - 1].transform.position + new Vector3(0f, 0.5f, 0f);
                        }
                        catch
                        {
                            // Could not set cpu position to starter tile
                        }
                        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Mines"))
                        {
                            SpawnMinecart(cpu);
                        }

                        break;
                    case "Off":
                        // Debug.Log(i + " is off");
                        break;
                    default:
                        break;
                }
            }
        }
        catch // Spawns a debug P1 if you load the scene without going through the main menu first
        {
            GameObject player = Instantiate(playerShells[0]).GetComponentInChildren<ChangeSpaces>().gameObject;
            playerStorer.CreatePlayersArray(1);
            player.GetComponent<PlayerVals>().SetPlayerNumber(1);
            playerStorer.SetPlayer(1, player);
            player.transform.parent.gameObject.transform.parent = GameObject.Find("World").transform;
            player.transform.parent.gameObject.transform.name = "Player" + 1;
            try
            {
                player.transform.position = starterTiles[0].transform.position + new Vector3(0f, 0.5f, 0f);
            }
            catch
            {
                // Could not set player position to starter tile
            }
            player.GetComponent<PlayerInput>().SetPlayerInputs("Attack", "Defend", "Vertical", "Horizontal");

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Mines"))
            {
                SpawnMinecart(player);
            }
        }

        GetComponent<ScreenDarken>().SetIsUnDarkening(true);
	}

    private GameObject SpawnShell(int currentEntry)
    {
        int playerShellNum = 0;
        switch (menuVals.GetFighterClass(currentEntry))
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

        GameObject shell = Instantiate(playerShells[playerShellNum]).GetComponentInChildren<ChangeSpaces>().gameObject;
        return shell;
    }

    private void SpawnMinecart(GameObject player)
    {
        GameObject minecart = (GameObject)Instantiate(Resources.Load("Minecart"));
        minecart.transform.position = player.GetComponentInChildren<ChangeSpaces>().gameObject.transform.position - new Vector3(0.3f, 0.5f, 0f);
        minecart.transform.parent = player.GetComponentInChildren<ChangeSpaces>().gameObject.transform;
        minecart.name = "Minecart";
    }

    public int GetNumberOfPlayer()
    {
        return numberOfPlayers;
    }
	
}
