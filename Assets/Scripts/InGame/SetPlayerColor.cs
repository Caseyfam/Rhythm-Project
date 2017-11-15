using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerColor : MonoBehaviour {

	void Start()
    {
        try
        {
            StoredColors storedColors = GameObject.Find("Menu Logic").GetComponent<StoredColors>();
            GetComponent<SpriteRenderer>().color = storedColors.GetColor(GetComponent<PlayerVals>().GetPlayerNumber());
            GetComponent<PlayerVals>().SetColor(storedColors.GetColor(GetComponent<PlayerVals>().GetPlayerNumber()));
        }
        catch
        {
            // No Menu Logic object from main menu passed so we set it to White
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<PlayerVals>().SetColor(Color.white);
        }
    }
}
