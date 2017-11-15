using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatManMinion : MonoBehaviour {

    private int tickCounter;
    private int whenToGo;
    private float bpm;

    private bool goingUp = false;
    private bool goingDown = false;
    private Vector3 target;
    private GameObject tile;
    private GameObject parentObj;

    void Awake()
    {
        tickCounter = 0;
        parentObj = transform.parent.gameObject;
    }

	void FixedUpdate ()
    {
		if (goingUp)
        {
            parentObj.transform.position = Vector3.MoveTowards(parentObj.transform.position, target, bpm / 500);
            if (parentObj.transform.position == target)
            {
                goingUp = false;
                goingDown = true;
                target = parentObj.transform.position - new Vector3(0f, 5f, 0f);

                // Call back to attack here
                try
                {
                    GameObject.Find("Rat Man").GetComponentInChildren<RatMan>().TileAttack(tile);
                }
                catch
                {

                }
            }
        }
        if (goingDown)
        {
            parentObj.transform.position = Vector3.MoveTowards(parentObj.transform.position, target, bpm / 250);
            if (parentObj.transform.position == target)
            {
                tile.GetComponent<TileChangeListener>().UnWarn();
                Destroy(transform.parent.gameObject);
            }
        }
	}

    void OnTick()
    {
        tickCounter++;

        if (tickCounter == (whenToGo - 4))
        {
            tile.GetComponent<TileChangeListener>().Warn("Physical");
        }

        if (tickCounter == (whenToGo - 3))
        {
            GameObject enterParticle = (GameObject)Instantiate(Resources.Load("Dig Particle"));
            var mainPr = enterParticle.GetComponent<ParticleSystem>().main;
            mainPr.duration = 10f;
            enterParticle.transform.parent = GameObject.Find("World").transform;
            enterParticle.transform.position = tile.transform.position;
            enterParticle.GetComponent<ParticleSystem>().Play();
        }

        if (tickCounter == whenToGo)
        {
            // Execute attack
            goingUp = true;
            target = parentObj.transform.position + new Vector3(0f, 1f, 0f);
        }
    }
    void OnTock()
    {
        tickCounter++;

        if (tickCounter == (whenToGo - 3))
        {
            GameObject enterParticle = (GameObject)Instantiate(Resources.Load("Dig Particle"));
            var mainPr = enterParticle.GetComponent<ParticleSystem>().main;
            mainPr.duration = 4f;
            enterParticle.transform.parent = GameObject.Find("World").transform;
            enterParticle.transform.position = tile.transform.position;
            enterParticle.GetComponent<ParticleSystem>().Play();
        }

        if (tickCounter == (whenToGo - 4))
        {
            tile.GetComponent<TileChangeListener>().Warn("Physical");
        }

        if (tickCounter == whenToGo)
        {
            // Execute attack
            goingUp = true;
            target = parentObj.transform.position + new Vector3(0f, 2f, 0f);
        }
    }

    public void ReceiveTickCount(int beatToGo, float bpm)
    {
        whenToGo = beatToGo;
        this.bpm = bpm;
    }

    public void ReceiveTile(GameObject tile)
    {
        this.tile = tile;
    }
}
