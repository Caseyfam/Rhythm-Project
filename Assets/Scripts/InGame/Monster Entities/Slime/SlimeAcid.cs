using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAcid : MonoBehaviour {

    public Sprite[] sprites;
    public int slimeDamage = 1;

    private SpriteRenderer sr;
    private GameObject player = null;

    private int tickCounter;
    private GameObject tile;

    private bool isShrinking = false;
    private float originalXScale;

    private float shrinkAmount;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length - 1)];
        shrinkAmount = GameObject.Find("World").GetComponent<Beat>().GetBPM() / 300;
    }

    void Update()
    {
        if (isShrinking)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkAmount, transform.localScale.y - shrinkAmount, transform.localScale.z - shrinkAmount);
            if (transform.localScale.x <= (0.1f * originalXScale))
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTick()
    {
        
        switch (tickCounter)
        {
            case 1:
            case 3:
            case 5:
                if (player != null)
                {
                    player.GetComponent<PlayerHealth>().Damage(slimeDamage);
                    player.GetComponent<DamageFlicker>().DoDamageFlicker();
                    tile.GetComponent<TileChangeListener>().Warn("Magical");
                }
                break;
            case 7:
                if (player != null)
                {
                    player.GetComponent<PlayerHealth>().Damage(slimeDamage);
                    player.GetComponent<DamageFlicker>().DoDamageFlicker();
                }
                // All of this below is so particles fade out on Destroy()
                GameObject particleChild = GetComponentInChildren<ParticleSystem>().gameObject;
                particleChild.transform.parent = GameObject.Find("World").transform;
                particleChild.GetComponent<ParticleSystem>().Stop();
                var mainParticle = particleChild.GetComponent<ParticleSystem>().main;
                if (!mainParticle.loop)
                {
                    Destroy(particleChild, mainParticle.duration);
                }
                particleChild.GetComponent<DestroyAfterTime>().TimedDestroy(3f);
                tile.GetComponent<TileChangeListener>().UnWarn();
                originalXScale = transform.localScale.x;
                isShrinking = true;
                break;
        }
        tickCounter++;

        // Gotta pass that FIX from monster Start() to here too
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }

    public void SetTile(GameObject tile)
    {
        this.tile = tile;
    }
}
