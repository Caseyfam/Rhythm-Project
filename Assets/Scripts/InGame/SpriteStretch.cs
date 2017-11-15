using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteStretch : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;
    public Vector3 stretchOffset = new Vector3(0f, 1f, 0f);
    public float smoothTime = 0.05f;
    private bool isStrecthing = true;

    void Awake()
    {
        originalScale = transform.localScale;
    }

	// Use this for initialization
	void Start ()
    {
        target = originalScale + stretchOffset;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isStrecthing)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, target, ref velocity, smoothTime);
        }
	}

    void OnTock()
    {
        if (tag != "Monster") // Differentiate up and down movement from players
        {
            target = originalScale + stretchOffset;
        }
        else
        {
            target = originalScale;
        }
    }

    void OnTick()
    {
        if (tag != "Monster")
        {
            target = originalScale;
        }
        else
        {
            target = originalScale + stretchOffset;
        }
    }

    public void SetIsStretching(bool stretch)
    {
        isStrecthing = stretch;
    }
    
    public bool GetIsStretching()
    {
        return isStrecthing;
    }
}
