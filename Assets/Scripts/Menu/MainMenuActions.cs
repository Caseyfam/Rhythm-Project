using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuActions : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject runMenu;
    public GameObject eventSystem;
    private StoredMenuValues storedVals;

    void Awake()
    {
        storedVals = GameObject.Find("Menu Logic").GetComponent<StoredMenuValues>();
    }

    public void LoadNewRunMenu()
    {
        mainMenu.SetActive(false);
        runMenu.SetActive(true);
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Start Button"));
    }

    public void LoadTestScene()
    {
        // Will tell the game to store player fighter classes
        for (int i = 1; i <= 4; i++) // Debug, shouldn't set all fighters as Wizards ya dummy
        {
            storedVals.SetFighterClass(i, storedVals.GetFighterClass(i));
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(2); // Loads next scene"
    }
}
