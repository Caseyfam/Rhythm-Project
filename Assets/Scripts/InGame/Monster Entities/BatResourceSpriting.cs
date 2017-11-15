using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatResourceSpriting : MonoBehaviour {

    public Sprite[] sprites;
    private int tickCounter;
    private int currentSprite = 0;
    private bool spritingUp = true;
    SpriteRenderer sr;

    public bool onHalf = false;
    public bool onEighth = false;

	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();

	}

    private void SetSprites()
    {
        if (spritingUp)
        {
            currentSprite++;
        }
        else
        {
            currentSprite--;
        }

        try
        {
            sr.sprite = sprites[currentSprite];
        }
        catch
        {
            if (spritingUp)
            {
                currentSprite--;
            }
            else
            {
                currentSprite++;
            }
            spritingUp = !spritingUp;
            SetSprites();
        }

    }

    void OnTick()
    {
        SetSprites();
    }

    void OnTock()
    {
        if (onHalf && !onEighth)
        {
            SetSprites();
        }
    }

    void OnEighth()
    {
        if (onEighth && !onHalf)
        {
            SetSprites();
        }
    }
}
