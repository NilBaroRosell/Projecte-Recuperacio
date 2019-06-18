using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndFinish : MonoBehaviour {

    public bool start;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(start)
            {
                GameControl.control.startTimer = Time.time;
                GameControl.control.stopTimer = false;
            }
            else
            {
                GameControl.control.stopTimer = true;
                GameControl.control.nextEvent = true;
            }
        }
    }
}
