using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDiamond : MonoBehaviour {

    public int desiredPlayer;
    public int diamondNumber;

    private Color storedColor;
    public Vector2[] positions;
    public GameObject[] beatDiamonds;
    private Vector2 target;
    private GlobalVariables globalVars;

    private int tickCounter = 0;
    private int activeCounter = 0;
    private bool shouldMove = false;

    void Awake()
    {
        globalVars = GameObject.Find("Game Logic").GetComponent<GlobalVariables>();

        beatDiamonds[7].transform.position = new Vector2(Screen.width / 2, Screen.height / 10);

        for (int i = 6; i >= 0; i--)
        {
            beatDiamonds[i].transform.position = new Vector2(beatDiamonds[i + 1].transform.position.x - (Screen.width / 10), beatDiamonds[i + 1].transform.position.y);
        }
        for (int i = 8; i < 16; i++)
        {
            beatDiamonds[i].transform.position = new Vector2(beatDiamonds[i - 1].transform.position.x + (Screen.width / 10), beatDiamonds[i - 1].transform.position.y);
        }

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = beatDiamonds[i].transform.position;
        }

        try
        {
            storedColor = GameObject.Find("Menu Logic").GetComponent<StoredColors>().GetColor(desiredPlayer);
        }
        catch
        {
            // No color stored
            storedColor = Color.white;
        }

        GetComponent<UnityEngine.UI.Image>().color = storedColor; 
    }
	
	void FixedUpdate()
    {
        if (shouldMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, GetComponentInParent<Beat>().GetBPM() / 6);
        }   
    }

    void OnPreciseTick(bool isActive)
    {
        if (!globalVars.GetPlayerTickPaused())
        {
            if (activeCounter == 3)
            {
                if (diamondNumber == 8 && isActive)
                {
                    transform.localScale = new Vector3(0.6f, 0.6f);
                }
                else
                {
                    transform.localScale = new Vector3(0.3f, 0.3f);
                }
                activeCounter = 0;
            }
            else
            {
                transform.localScale = new Vector3(0.3f, 0.3f);
                activeCounter++;
            }
        }
    }

    void OnTick()
    {
        if (!globalVars.GetPlayerTickPaused())
        {
            if (tickCounter == 0)
            {
                tickCounter = 1;
            }
            else
            {
                shouldMove = true;
                try
                {
                    target = positions[diamondNumber];
                }
                catch
                {
                    // Diamond at 0 loops back
                    target = positions[0];
                }

                if (GetComponent<RectTransform>().position.x > Screen.width) // REALLY SHIT WAY OF TELLING IF UI IS OFF SCREEN
                {
                    transform.position = target;
                }


                if (diamondNumber != 16)
                {
                    diamondNumber++;
                }
                else
                {
                    diamondNumber = 1;
                }

                tickCounter--;
            }
        }
        if (globalVars.GetPlayerTickPaused())
        {
            tickCounter = 1;
        }
    }
}
