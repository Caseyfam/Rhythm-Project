using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpaces : MonoBehaviour {

    private int tickCounter = 0;
    private int currentTile;
    private int originalTile;
    private bool moving = false;
    private bool isTransformMoving = true;
    private Vector3[] originalTileDestinations = new Vector3[12];
    private Vector3[] tileDestinations = new Vector3[12];
    private float increment;

    public GameObject[] tiles; // public for PlayerInput script (not sure if that's good or not)

    private GlobalVariables globalVars;

    private float startTime, elapsedTime;
    private Vector3 startPos;
    private float trajectoryHeight = 0.5f;

    private string sceneName;

    void Awake()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneName.Equals("Mines"))
        {
            trajectoryHeight = trajectoryHeight * 2;
        }

        tiles = new GameObject[12];
        // This is kind of lame but necessary? after turning it into a prefab
        tiles[0] = GameObject.Find("Floor Tile 0");
        tiles[1] = GameObject.Find("Floor Tile 1");
        tiles[2] = GameObject.Find("Floor Tile 2");
        tiles[3] = GameObject.Find("Floor Tile 3");
        tiles[4] = GameObject.Find("Floor Tile 4");
        tiles[5] = GameObject.Find("Floor Tile 5");
        tiles[6] = GameObject.Find("Floor Tile 6");
        tiles[7] = GameObject.Find("Floor Tile 7");
        tiles[8] = GameObject.Find("Floor Tile 8");
        tiles[9] = GameObject.Find("Floor Tile 9");
        tiles[10] = GameObject.Find("Floor Tile 10");
        tiles[11] = GameObject.Find("Floor Tile 11");
    }

	// Use this for initialization
	void Start ()
    {

        globalVars = GameObject.Find("Game Logic").GetComponent<GlobalVariables>();

        for (int i = 0; i < 12; i++)
        {
            try
            {
                originalTileDestinations[i] = tiles[i + 3].transform.position;
            }
            catch
            {
                originalTileDestinations[i] = tiles[i - 9].transform.position;
            }
            
        }
        tileDestinations = originalTileDestinations;
    }
	
	// Update is called once per frame
	void Update ()
    {
        elapsedTime = Time.time - startTime;
        Move();
	}

    void Move()
    {
        if (moving)
        {
            float bpm = GetComponentInParent<Beat>().GetBPM();

            if (isTransformMoving)
            {
                float currentTime = elapsedTime * (0.7f / (12 / bpm));

                Vector3 currentPos = Vector3.Lerp(startPos, tileDestinations[originalTile] + new Vector3(0f, 0.5f, 0f), currentTime);
                currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(currentTime) * Mathf.PI);
                transform.position = currentPos;
            }

            if ((Vector3)transform.position == tileDestinations[originalTile] + new Vector3(0f, 0.5f, 0f))
            {
                tileDestinations = originalTileDestinations;
            }
            
            tickCounter = 0;
        }
    }

    void OnTick()
    {
        if (!globalVars.GetPlayerTickPaused())
        {
            tickCounter++;
            //Debug.Log(tickCounter);
            // If tickCounter == 1, player can perform actions
            if (tickCounter == 2)
            {
                originalTile = currentTile;
                moving = true;
                startTime = Time.time;
                startPos = transform.position;
            }
            else
            {
                moving = false;
                increment = 0f;
            }
        }
        else
        {
            moving = false;
        }
    }

    public void SkipTurn()
    {
        originalTile = currentTile;
        moving = true;
    }

    public int GetCurrentTile()
    {
        return currentTile;
    }

    public GameObject GetCurrentTileObject()
    {
        return tiles[currentTile];
    }

    public void SetCurrentTile(int tile)
    {
        currentTile = tile;
    }

    public Vector3[] GetTileDestinations()
    {
        return tileDestinations;
    }

    public void SetTileDestinations(Vector3[] tileDestinations)
    {
        this.tileDestinations = tileDestinations;
    }

    public void ResetTileDestinations()
    {
        tileDestinations = originalTileDestinations;
    }

    public bool GetIsMoving()
    {
        return moving;
    }

    public void SetTransformMoving (bool flag)
    {
        isTransformMoving = flag;
    }

    public bool GetTransformMoving()
    {
        return isTransformMoving;
    }

}
