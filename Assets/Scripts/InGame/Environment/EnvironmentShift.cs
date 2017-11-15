using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentShift : MonoBehaviour {

    private bool canShift = true;

    public void ShiftEnvironment()
    {
        if (canShift)
        {
            switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
            {
                case "Menu":
                    // This is the main menu and thus shouldn't load
                    break;
                case "Foyer":
                    GetComponent<FoyerEnvironmentShift>().ShiftFoyer();
                    break;
                case "Caves":
                    GetComponent<CavesEnvironmentShift>().ShiftCaves();
                    // This is currently "Caves"
                    break;
                default:
                    break;
            }
        }
    }

    public void SetCanShift(bool val)
    {
        canShift = val;
    }

    public bool GetCanShift()
    {
        return canShift;
    }
}
