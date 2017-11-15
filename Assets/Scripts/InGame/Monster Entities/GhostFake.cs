using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFake : GenericMonster {

    private Animator animator;

    private int tickCounter;

    private float bpm;

    void Awake()
    {
        animator = GetComponent<Animator>();
        SetEnvironmentShift(false);
        LoadElements();
    }

	// Use this for initialization
	void Start ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        SetHealth(1);
        tickCounter = SetMonsterTickCounter();

        animator.speed = bpm / 60f;
        animator.Play("GhostFakeIn");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTick()
    {
        if (tickCounter == 2)
        {
            animator.Play("GhostIdle");
        }
        tickCounter++;
    }

    public void DestroyGhost()
    {
        Destroy(this.gameObject);
    }

    void MonsterDeath()
    {

    }
}
