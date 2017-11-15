using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoyerEnvironmentShift : MonoBehaviour {

    private float bpm;
    public GameObject section1, section2, door;
    private Vector3 positionBack = new Vector3(-6.602374f, -14.9f, 39.6f);
    private Vector3 positionMid = new Vector3(-6.602374f, -12.98213f, 19.32639f);
    private Vector3 positionFront = new Vector3(-6.602374f, -10.9f, -1.1f);

    private bool isShifting = false;
    private bool doorDone = false;

	// Use this for initialization
	void Start ()
    {
        bpm = GameObject.Find("World").GetComponent<Beat>().GetBPM();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isShifting)
        {
            door.transform.rotation = Quaternion.RotateTowards(door.transform.rotation, new Quaternion(-0.68f, 0, 0, 0.72f), bpm / 30f);
            float angle = Quaternion.Angle(door.transform.rotation, new Quaternion(-0.68f, 0, 0, 0.72f));
            if (angle < 17)
            {
                doorDone = true;
            }
            if (doorDone)
            {
                section1.transform.localPosition = Vector3.MoveTowards(section1.transform.localPosition, positionMid, bpm / 600f);
                section2.transform.localPosition = Vector3.MoveTowards(section2.transform.localPosition, positionFront, bpm / 600f);

                if (section2.transform.localPosition == positionFront && section1.transform.localPosition == positionMid)
                {
                    section1.transform.localPosition = positionBack;
                    section2.transform.localPosition = positionMid;
                    door.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                    isShifting = false;
                    doorDone = false;
                }
            }
            
        }
	}

    public void ShiftFoyer()
    {
        isShifting = true;
    }
}
