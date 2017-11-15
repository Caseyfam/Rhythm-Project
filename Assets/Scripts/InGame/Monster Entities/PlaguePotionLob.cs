using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    explosion, plague
}

public class PlaguePotionLob : MonoBehaviour {

    private GameObject tile;
    private Vector3 startPos, endPos;
    private Vector3 velocity = Vector3.zero;

    private float bpm;
    private float startTime, elapsedTime;
    private float trajectoryHeight = 1f;
    public bool randomTracjectoryHeight;

    private bool isFiring = false;
    public PotionType potionType;
    
    void Start()
    {
        if (randomTracjectoryHeight)
        {
            trajectoryHeight = Random.Range(0.5f, 2.5f);
        }
    }

    void Update()
    {
        elapsedTime = Time.time - startTime;
    }

    void FixedUpdate()
    {
        if (isFiring)
        {
            float currentTime = elapsedTime * (0.7f / (24 / bpm));

            Vector3 currentPos = Vector3.Lerp(startPos, endPos, currentTime);
            currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(currentTime) * Mathf.PI);
            transform.position = currentPos;

            if (transform.position == endPos)
            {
                if (potionType == PotionType.plague)
                {
                    GameObject puddle = (GameObject)Instantiate(Resources.Load("Slime Acid"));
                    puddle.GetComponent<SpriteRenderer>().color = Color.green;
                    puddle.transform.parent = GameObject.Find("World").transform;
                    puddle.GetComponent<SlimeAcid>().SetTile(tile);
                    puddle.transform.position = transform.position + new Vector3(0f, 0.1f, 0f);

                    Destroy(this.gameObject);
                }
                if (potionType == PotionType.explosion)
                {
                    // Load an explosion after it hits
                    tile.GetComponent<TileChangeListener>().DamagePlayerPhysical(1);
                    tile.GetComponent<TileChangeListener>().UnWarn();
                    Destroy(this.gameObject);
                }
                
            }
        }
    }

    public void FirePotion(GameObject tile)
    {
        this.tile = tile;
        startPos = transform.position;
        endPos = tile.transform.position;

        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        startTime = Time.time;

        isFiring = true;
    }
}
