using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentItem : MonoBehaviour {

    public static PersistentItem instance;

	void Awake ()
    {
		if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
	}
	
}
