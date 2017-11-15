using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMite : MonoBehaviour {

    private float miteSpeed = 1f;
    private bool isFalling = false;
    private bool shardsWent = false;
    private GameObject target;

    void Awake()
    {
        switch (Random.Range(0,2))
        {
            case 0:
                break;
            case 1:
                transform.rotation = new Quaternion(0, 1, 0, 0);
                break;
        }
    }

	// Update is called once per frame
	void Update ()
    {
		if (isFalling)
        {
            transform.position = transform.position - (new Vector3(0f, 0.05f, 0f) * miteSpeed);
            if (transform.position.y <= target.transform.position.y)
            {
                if (!shardsWent)
                {
                    GameObject fragments = (GameObject)Instantiate(Resources.Load("Mite Shards"));
                    fragments.transform.position = transform.position;
                    shardsWent = true;
                }
                StartCoroutine(WaitToDestroy(1f));
            }
        }
	}

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    public void MiteFall (GameObject tile)
    {
        miteSpeed = GameObject.Find("World").GetComponent<Beat>().GetBPM() / 60f;
        isFalling = true;
        target = tile;
    }
}
