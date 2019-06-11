using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("Plane1_S2") != null) player = GameObject.Find("Plane1_S2");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 6);
	}
}
