using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour {

    public GameObject prop;
    public GameObject propBlured;
    public bool active;

    // Use this for initialization
    void Start () {
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameControl.control.gamePaused && active)
        {
            gameObject.transform.position += new Vector3(0.0f, 0.0f, 2.0f);
            prop.SetActive(false);
            propBlured.SetActive(true);
            propBlured.transform.Rotate(1000 * Time.deltaTime, 0, 0);
        }  
    }
}
