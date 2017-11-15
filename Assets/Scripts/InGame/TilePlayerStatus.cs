using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlayerStatus : MonoBehaviour {

    private bool tileBeingAttacked = false;

    public void SetTileBeingAttackedState(bool flag)
    {
        tileBeingAttacked = flag;
    }

    public bool GetTileBeingAttackedState()
    {
        return tileBeingAttacked;
    }
}
