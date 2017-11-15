using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTileListener : MonoBehaviour {

    private GameObject monsterOnTile = null;
    public int tileNumber;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster" && other.GetComponent<GenericMonster>())
        {
            try
            {
                monsterOnTile = other.gameObject;
            }
            catch
            {
                monsterOnTile = other.transform.GetChild(0).gameObject;
                Debug.Log("Caught");
            }
            try
            {
                monsterOnTile.GetComponent<GenericMonster>().SetCurrentTile(tileNumber);
            }
            catch
            {

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
        {
            monsterOnTile = null;
        }
    }

    public GameObject GetMonsterOnTile()
    {
        return monsterOnTile;
    }

    public bool HasMonsterOnTile()
    {
        if (monsterOnTile != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
