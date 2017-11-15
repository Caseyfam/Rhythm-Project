using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInstantiate : MonoBehaviour {

    private bool isScaling = false;

    private GameObject spawnedMonster;
    private Vector3 originalScale;
    private float bpm;

	// Use this for initialization
	void Awake ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isScaling)
        {
            if (spawnedMonster != null && originalScale != null)
            {
                spawnedMonster.transform.localScale = Vector3.MoveTowards(spawnedMonster.transform.localScale, originalScale, (bpm / (20 * bpm)));
            }

            if (spawnedMonster.transform.localScale == originalScale)
            {
                spawnedMonster.transform.parent = GameObject.Find("World").transform;
                isScaling = false;
            }
        }
	}

    public void InstantiateMonster(GameObject monster, Vector3 position, Quaternion rotation)
    {
        originalScale = monster.transform.localScale;
        spawnedMonster = Instantiate(monster, position, rotation);
        spawnedMonster.transform.localScale = new Vector3(0f, 0f, 0f);
        GameObject smoke = (GameObject)Instantiate(Resources.Load("Cartoon Smoke Particle"));
        smoke.transform.position = spawnedMonster.transform.position;
        spawnedMonster.name = monster.name;
        isScaling = true;
    }
}
