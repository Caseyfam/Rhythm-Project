using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossMinion : MonoBehaviour {

    public Sprite[] sprites;

    private GameObject[] tiles;
    private float bpm;
    private int tickCounter = 0;
    private int startTickCounter = 0;

    private int[] path = new int[4];
    private int currentPathIndex = 0;
    private bool isFalling = false;
    private bool started = false;
    private bool shouldDamage = false;

    void Awake()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (started)
        {
            if (isFalling)
            {
                transform.position = Vector3.MoveTowards(transform.position, tiles[path[currentPathIndex]].transform.position + new Vector3(0f, 0.2f, 0f), bpm);
            }
            else
            {
                try
                {
                    transform.position = Vector3.MoveTowards(transform.position, tiles[path[currentPathIndex]].transform.position + new Vector3(0f, 0.2f, 0f), bpm / 1000f);
                    if (transform.position == tiles[path[currentPathIndex]].transform.position + new Vector3(0f, 0.2f, 0f))
                    {
                        shouldDamage = true;
                    }
                }
                catch
                {
                    transform.position = Vector3.MoveTowards(transform.position, tiles[1].transform.position + new Vector3(-5f, 0.2f, 0f), bpm / 1000f);
                }
            }
        }
       
        if (shouldDamage)
        {
            DamagePlayer();
            shouldDamage = false;
        }
    }

    private void ChangeSprites()
    {
        if (GetComponent<SpriteRenderer>().sprite == sprites[0])
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }

    void OnTock()
    {
        ChangeSprites();
    }

    void OnTick()
    {
        ChangeSprites();
        Debug.Log(startTickCounter);

        if (started)
        {
            if (tickCounter >= 1)
            {
                currentPathIndex++;
                if (isFalling)
                {
                    isFalling = false;
                }
                tiles[path[currentPathIndex - 1]].GetComponent<TileChangeListener>().UnWarn();
                
                tickCounter = 0;
            }
            else
            {
                DamagePlayer();
                
                try
                {
                    tiles[path[currentPathIndex + 1]].GetComponent<TileChangeListener>().Warn("Magic");
                }
                catch
                {

                }

                tickCounter++;
            }
        }

        if (startTickCounter == 0)
        {
            started = true;
        }
        if (startTickCounter == 9)
        {
            Destroy(gameObject);
            foreach(GameObject tiles in tiles)
            {
                tiles.GetComponent<TileChangeListener>().UnWarn();
            }
        }
        startTickCounter++;
    }

    private void DamagePlayer()
    {
        try
        {
            tiles[path[currentPathIndex]].GetComponent<TileChangeListener>().GetPlayerOnTile().GetComponent<PlayerHealth>().Damage(1);
            foreach (GameObject tile in tiles)
            {
                tile.GetComponent<TileChangeListener>().UnWarn();
            }
            Destroy(gameObject);
        }
        catch
        {

        }
    }

    public void CreateSpiderMinion(GameObject[] tiles)
    {
        this.tiles = tiles;
        switch (Random.Range(0, 4))
        {
            case 0:
                path = new int[4] { 9, 7, 3, 1 };
                break;
            case 1:
                path = new int[4] { 10, 6, 4, 0 };
                break;
            case 2:
                path = new int[4] { 10, 8, 4, 2 };
                break;
            case 3:
                path = new int[4] { 11, 7, 5, 1 };
                break;
            default:
                path = new int[4] { 9, 7, 3, 1 };
                break;
        }
        tiles[path[0]].GetComponent<TileChangeListener>().Warn("Magic");
    }
}
