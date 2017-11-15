using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : GenericMonster {

    public Sprite[] sprites;
    private int tickCounter;
    private int currentSprite = 0;
    private bool spritingUp = true;
    SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        SetHealth(3);
        tickCounter = SetMonsterTickCounter();
        sr = GetComponent<SpriteRenderer>();
        Animation anim = GetComponent<Animation>();
        anim["BatIdle"].speed = GameObject.Find("World").GetComponent<Beat>().GetBPM() / 90f;
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
        SetSprites();
    }

    void MonsterDeath()
    {
        monsterDeathVal.UpdateMonsterPoints("Bat");
        stats.AddToMonstersDefeated(1);
    }
}
