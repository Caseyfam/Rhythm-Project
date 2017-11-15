using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartDisplay : MonoBehaviour
{
    PlayerHealth playerHealth;
    SpriteRenderer sr;
    public Sprite[] heartSprites;

    private int healthVal = 6;
    private bool shouldBeVisible = true;

    private Vector3 originalPosition;

	// Use this for initialization
	void Start ()
    {
        originalPosition = transform.localPosition;
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        shouldBeVisible = true;
        playerHealth = GetComponentInParent<PlayerHealth>();
        sr.color = GetComponentInParent<PlayerVals>().GetColor();
        UpdateHeartDisplay(playerHealth.GetHealth());
    }

    public void UpdateHeartDisplay(int health)
    {
        healthVal = health;

        if (health == 0)
        {
            sr.enabled = false;
        }
        else
        {
            try
            {
                sr.enabled = true;
            }
            catch
            {

            }
            try
            {
                sr.sprite = heartSprites[health - 1];
            }
            catch
            {

            }
            shouldBeVisible = true;
        }
       
    }

    void OnTick()
    {
        if (shouldBeVisible)
        {
            shouldBeVisible = false;
        }
        else
        {
            sr.enabled = false;
        }
    }

    public void ResetHeartPosition()
    {
        transform.localPosition = originalPosition;
    }
}
