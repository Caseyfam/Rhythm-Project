using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationOnTick : MonoBehaviour {

    public Sprite[] sprites;
    public bool updateOnTock = false;
    public bool randomizeSprites = false;
    private float bpm;
    private int spriteIndex = 0;
    private int previousSpriteIndex = 0;
    SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
	}
    
    void OnTick()
    {
        if (updateOnTock)
        {
            UpdateSprite();
        }
    }
    
    void OnTock()
    {
        UpdateSprite();
    }

    void UpdateSprite()
    {
        sr.sprite = sprites[spriteIndex];
        if (randomizeSprites)
        {
            spriteIndex = FindRandom();
        }
        else
        {
            spriteIndex++;
            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }
        }
    }

    int FindRandom()
    {
        previousSpriteIndex = spriteIndex;
        int newSpriteIndex = Random.Range(0, sprites.Length);
        if (newSpriteIndex != previousSpriteIndex)
        {
            return newSpriteIndex;
        }
        else
        {
            FindRandom();
        }
        return FindRandom();
    }
	

}
