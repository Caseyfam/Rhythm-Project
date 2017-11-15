using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpriteAnimation : MonoBehaviour {

    UnityEngine.UI.Image thisImage;
    public Sprite[] sprites;
    public float speed;
    private int spriteIndex = 0;

	// Use this for initialization
	void Awake ()
    {
        thisImage = GetComponent<UnityEngine.UI.Image>();
        StartCoroutine(NextSprite(speed));
    }
	
    IEnumerator NextSprite (float time)
    {
        yield return new WaitForSeconds(time);
        spriteIndex++;
        try
        {
            thisImage.sprite = sprites[spriteIndex];
        }
        catch
        {
            spriteIndex = 0;
            thisImage.sprite = sprites[0];
        }
        StartCoroutine(NextSprite(time));
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
