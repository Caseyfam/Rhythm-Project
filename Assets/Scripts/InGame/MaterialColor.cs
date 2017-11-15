using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialColor : MonoBehaviour {

    public Color overrideColor;
    Material mat;

    void Awake()
    {
        Material tempMat = new Material(GetComponent<Renderer>().sharedMaterial);
        tempMat.color = overrideColor;
        GetComponent<Renderer>().material = tempMat;
    }
}
