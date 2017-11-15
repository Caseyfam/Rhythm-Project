using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChangeListener : MonoBehaviour {

    public int tileNumber;
    private GameObject playerOnTile = null;
    private string warning;

    void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            try
            {
                other.GetComponent<ChangeSpaces>().SetCurrentTile(tileNumber);
                playerOnTile = other.gameObject;
                //playerOnTile = other.gameObject.transform.parent.gameObject;
                //Debug.Log("Set " + other.name + "'s tile to " + tileNumber);
            }
            catch
            {
                // Player doesn't have "ChangeSpaces" script for some reason
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOnTile = null;
        }
    }

    public void Warn(string type)
    {
        warning = type;
        switch (type)
        {
            case "Physical":
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "Magic":
            case "Magical":
                GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
        }
    }

    public void UnWarn()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        warning = null;
        // warning = "None";
    }

    public GameObject GetPlayerOnTile()
    {
        return playerOnTile;
        //return playerOnTile.transform.parent.gameObject;
    }

    public void DamagePlayerPhysical(int damage)
    {
        GameObject player = GetPlayerOnTile();
        if (player != null)
        {
            if (!player.GetComponent<PlayerDefend>().GetPlayerDefending())
            {
                player.GetComponent<PlayerHealth>().Damage(damage);
            }
        }
    }

    public void DamagePlayerMagical(int damage)
    {
        GameObject player = GetPlayerOnTile();
        if (player != null)
        {
            player.GetComponent<PlayerHealth>().Damage(damage);
        }
    }

    public string GetTileWarning()
    {
        return warning;
    }
}
