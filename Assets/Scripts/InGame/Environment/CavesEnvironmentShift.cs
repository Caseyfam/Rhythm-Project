using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavesEnvironmentShift : MonoBehaviour {

    private bool isShifting = false;

    public GameObject[] segments;
    private GameObject backSegment;
    private float bpm;
    private Vector3 firstPos, secondPos, thirdPos, backPos;

    void Start()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
        firstPos = segments[0].transform.position;
        secondPos = segments[1].transform.position;
        thirdPos = segments[2].transform.position;
    }
    
    // Update is called once per frame

    void Update ()
    {
		if (isShifting)
        {
            // Need to tie movement shift to BPM probably


            segments[0].transform.position = Vector3.MoveTowards(segments[0].transform.position, firstPos, 0.1f);
            //segments[0].transform.position = Vector3.SmoothDamp(segments[0].transform.position, firstPos, 0.1f);
            segments[1].transform.position = Vector3.MoveTowards(segments[1].transform.position, secondPos, 0.1f);
            segments[2].transform.position = Vector3.MoveTowards(segments[2].transform.position, new Vector3(segments[2].transform.position.x - 1f, segments[2].transform.position.y, segments[2].transform.position.z), 0.1f);
            if (segments[1].transform.position == secondPos)
            {
                segments[2].transform.position = thirdPos;
                isShifting = false;
            }
        }
	}

    void ShiftPositions()
    {
        backSegment = segments[0];
        segments[0] = segments[1];
        segments[1] = segments[2];
        segments[2] = backSegment;
    }

    public void ShiftCaves()
    {
        ShiftPositions();
        isShifting = true;
    }
}
