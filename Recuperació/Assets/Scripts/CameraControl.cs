using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject player;
    public float initialY;
    private float initialRotationX;
    public bool zoomOut;
    public Vector3 stopPosition;

	// Use this for initialization
	void Start () {
        zoomOut = false;
        initialY = gameObject.transform.position.y;
        initialRotationX = gameObject.transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
        if(zoomOut)
        {
            gameObject.transform.position = stopPosition;
            gameObject.transform.eulerAngles = new Vector3(initialRotationX, 0.0f, 0.0f);
        }
        else
        {
            if (player.GetComponent<PlayerController>().back)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.transform.position.x, initialY, gameObject.transform.position.z), 0.75f * Time.deltaTime);
                gameObject.transform.LookAt(player.transform.position);
                //gameObject.transform.eulerAngles = new Vector3(initialRotationX, 0.0f, 0.0f);
            }
            else if (player.GetComponent<PlayerController>().front)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.transform.position.x, initialY, gameObject.transform.position.z), 0.75f * Time.deltaTime);
                gameObject.transform.LookAt(player.transform.position);
                //gameObject.transform.eulerAngles = new Vector3(initialRotationX, 0.0f, 0.0f);
            }
            else if (player.GetComponent<PlayerController>().left)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.transform.position.x, initialY + 8, player.transform.position.z - 15), 0.75f * Time.deltaTime);
                gameObject.transform.eulerAngles = new Vector3(initialRotationX, 0.0f, 0.0f);
            }
            else if (player.GetComponent<PlayerController>().right)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.transform.position.x, initialY + 8, player.transform.position.z - 15), 0.75f * Time.deltaTime);
                gameObject.transform.eulerAngles = new Vector3(initialRotationX, 0.0f, 0.0f);
            }
            else
            {
                if (GameControl.control.boostActivated)
                {
                    if ((Input.GetButton("Space")) && player.GetComponent<PlayerController>().boostAmount > 0)
                    {
                        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.transform.position.x, initialY, player.transform.position.z - 25), 0.75f * Time.deltaTime);
                    }
                    else
                    {
                        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position = new Vector3(player.transform.position.x, initialY, player.transform.position.z - 15), 0.75f * Time.deltaTime);
                    }
                }
                else gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position = new Vector3(player.transform.position.x, initialY, player.transform.position.z - 15), 0.75f * Time.deltaTime);

                gameObject.transform.eulerAngles = new Vector3(initialRotationX, 0.0f, 0.0f);
            }
        }
	}
}
