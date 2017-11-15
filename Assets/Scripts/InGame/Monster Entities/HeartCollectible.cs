using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : GenericMonster {

    private int tickCounter;
    private int heartTick;
    public Sprite[] sprites;

	// Use this for initialization
	void Start ()
    {
        SetHealth(1);
        tickCounter = SetMonsterTickCounter();
        StartCoroutine(WaitForSecondTick(GameObject.Find("World").GetComponent<Beat>().GetTickSpeed() / 2));
    }

    void OnTick()
    {
        heartTick++;
        tickCounter++;
        BroadcastMessage("TickIncrement");
        StartCoroutine(WaitForSecondTick(GameObject.Find("World").GetComponent<Beat>().GetTickSpeed() / 2));

        if (heartTick == 8)
        {
            Animation anim = GetComponentInParent<Animation>();
            anim.Play("HeartFlyAway");
            anim["HeartFlyAway"].speed = GameObject.Find("World").GetComponentInParent<Beat>().bpm / 60f;
        }
        if (heartTick == 10)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitForSecondTick(double time)
    {
        yield return new WaitForSeconds((float)time);
        tickCounter++;
        BroadcastMessage("TickIncrement");
    }

    void TickIncrement()
    {
        switch (tickCounter)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = sprites[3];
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case 6:
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                tickCounter = 0;
                break;
            default:
                break;
        }
    }

    void MonsterDeath()
    {
        GetPlayerThatDamagedMonster().GetComponent<PlayerHealth>().SetHealth(GetPlayerThatDamagedMonster().GetComponent<PlayerHealth>().GetHealth() + 2);
        monsterDeathVal.UpdateMonsterPoints("HeartCollectible");
    }
}
