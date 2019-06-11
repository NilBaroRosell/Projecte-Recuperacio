using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    public GameObject player;
	
	void FixedUpdate () {
        if(Input.GetButton("Space")) gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 5.0f, player.transform.position.z - 7.0f), 10f * Time.deltaTime);
        else gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 5.0f, player.transform.position.z - 6.0f), 5f * Time.deltaTime);
    }
}
