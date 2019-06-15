using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllers : MonoBehaviour {

    //public bool narrowZone;
    public bool narrowZone;
    public bool first;
    private bool crossed;

	// Use this for initialization
	void Start () {
        crossed = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (narrowZone)
        {
            if (GameObject.Find("Plane").gameObject.transform.position.z > gameObject.transform.position.z + 1)
            {
                GameControl.control.penaltyTime += 5;
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == ("Plane") && !crossed)
        {
            if (narrowZone)
            {
                GameControl.control.score += 20;
            }
            else
            {
                if (first)
                {
                    Debug.Log("ENTRA1");
                    GameControl.control.score += 10;
                }
                else
                {
                    Debug.Log("ENTRA2");
                    GameControl.control.score += 5;
                    crossed = true;
                }
            }
            Destroy(gameObject);
        }
    }
}
