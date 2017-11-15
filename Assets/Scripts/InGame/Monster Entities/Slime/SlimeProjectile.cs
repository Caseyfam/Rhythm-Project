using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour {

    public Vector3 scaleTarget = new Vector3(10f, 10f, 2f);

    private GameObject tile;
    private Vector3 startPos, endPos;
    private Vector3 velocity = Vector3.zero;

    private float bpm;
    private float startTime, elapsedTime;
    private float trajectoryHeight = 1f;

    private bool isFiring = false;

    void Update()
    {
        elapsedTime = Time.time - startTime;
    }

	void FixedUpdate ()
    {
        if (isFiring)
        {
            float currentTime = elapsedTime * (0.7f / (24 / bpm));

            transform.localScale = Vector3.SmoothDamp(transform.localScale, scaleTarget, ref velocity, 24 / bpm);

            Vector3 currentPos = Vector3.Lerp(startPos, endPos, currentTime);
            currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(currentTime) * Mathf.PI);
            transform.position = currentPos;

            if (transform.position == endPos)
            {
                // Spawn puddle
                GameObject puddle = (GameObject)Instantiate(Resources.Load("Slime Acid"));
                puddle.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                puddle.transform.parent = GameObject.Find("World").transform;
                puddle.GetComponent<SlimeAcid>().SetTile(tile);
                puddle.transform.position = transform.position + new Vector3 (0f, 0.1f, 0f);
                Destroy(this.gameObject);
            }
        }
	}

    public void FireSlime(GameObject tile)
    {
        this.tile = tile;
        startPos = transform.position;
        endPos = tile.transform.position;

        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        startTime = Time.time;

        isFiring = true;
        
    }
}
