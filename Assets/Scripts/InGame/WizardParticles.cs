using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardParticles : MonoBehaviour {

    private GameObject target;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool added = false;

    private Color playerColor;

	// Update is called once per frame
	void Update ()
    {
	    if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.3f);
        }	

        if (transform.position == targetPosition)
        {
            if (added == false)
            {
                GameObject trail = (GameObject)Instantiate(Resources.Load("Wiz Particle Explosion"));

                trail.transform.parent = GameObject.Find("World").transform;
                trail.GetComponent<ParticleSystem>().startColor = playerColor;
                try
                {
                    trail.transform.position = target.transform.position;
                }
                catch
                {
                    trail.transform.position = targetPosition;
                }
                
                added = true;
            }

            GetComponent<ParticleSystem>().enableEmission = false; // Fuck off, their suggestion doesn't even work
            StartCoroutine(KillParticles(5f));
        }
	}

    public void LaunchParticle(GameObject target, Color color)
    {
        this.target = target;
        targetPosition = target.transform.position;
        playerColor = color;
        GetComponent<ParticleSystem>().startColor = playerColor;
        isMoving = true;
    }

    IEnumerator KillParticles(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
