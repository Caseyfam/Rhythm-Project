using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int startHealth = 6;

    private int health;
    private int maxHealth;
    public Sprite damageFlinch;
    public Sprite idle;
    PlayerHeartDisplay heartDisplay;
    CameraShake camShake;

	// Use this for initialization
	void Start ()
    {
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        heartDisplay = GetComponentInChildren<PlayerHeartDisplay>();
        maxHealth = startHealth;
        try
        {
            SetHealth(GameObject.Find("Passed Items").GetComponent<StoredPlayerHealth>().GetHealth(GetComponent<PlayerVals>().GetPlayerNumber()));
        }
        catch
        {
            SetHealth(startHealth);
        }
        
	}

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health) 
    {
        if ((health <= maxHealth) && (health >= 0))
        {
            this.health = health;
        }
        else if (health >= maxHealth)
        {
            this.health = maxHealth;
        }
        heartDisplay.UpdateHeartDisplay(health);
    }

    public void Damage(int damage)
    {
        health = health - damage;
        heartDisplay.UpdateHeartDisplay(health);
        CheckIfDead();
        camShake.SetCameraShake(0.5f);
        try
        {
            switch (GetComponent<PlayerVals>().GetFighterClass())
            {
                case "Wizard":
                    GetComponent<Animator>().Play("WizFlinchRedrawn");
                    GetComponent<DamageFlicker>().DoDamageFlicker();
                    break;
                case "Knight":
                    GetComponent<Animator>().Play("KnightFlinch");
                    GetComponent<DamageFlicker>().DoDamageFlicker();
                    break;
            }
        }
        catch
        {

        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Mines"))
        {
            Animation anim = GetComponentInChildren<Animation>();
            anim["MinecartShake"].speed = GameObject.Find("World").GetComponentInChildren<Beat>().GetBPM() / 60f;
            anim.Play("MinecartShake");
        }
    }

    void OnTick()
    {
        try
        {
            switch (GetComponent<PlayerVals>().GetFighterClass())
            {
                case "Wizard":
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WizFlinchRedrawn"))
                    {
                        GetComponent<Animator>().Play("WizIdle");
                    }
                    break;
                case "Knight":
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("KnightFlinch"))
                    {
                        GetComponent<Animator>().Play("KnightIdle");
                    }
                    break;
            }
        }
        catch
        {

        }
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
