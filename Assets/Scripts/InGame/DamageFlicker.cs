using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlicker : MonoBehaviour {

    private bool flickering = false;
    private bool spriteVisible = false;
    private int frameCount = 0;

    private int maxFramesForFlicker = 3; // Frames before visibility state flips
    private float flickerTime = 0.8f; // Length of flicker

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (flickering)
        {
            frameCount++;

            if (frameCount == maxFramesForFlicker)
            {
                if (spriteVisible)
                {
                    sr.enabled = true;
                    spriteVisible = !spriteVisible;
                }
                else
                {
                    sr.enabled = false;
                    spriteVisible = !spriteVisible;
                }
                frameCount = 0;
            }
        }
    }

    public void DoDamageFlicker()
    {
        flickering = true;
        try
        {
            StartCoroutine(FlickerTime(flickerTime));
        }
        catch
        {
            // Player set to inactive state
        }
    }

    IEnumerator FlickerTime(float time)
    {
        yield return new WaitForSeconds(time);
        sr.enabled = true;
        flickering = false;
        spriteVisible = false;
        frameCount = 0;
    }


}
