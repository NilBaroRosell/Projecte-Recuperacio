using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public int multiplier;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (GameObject.Find("Plane").GetComponent<PlayerController>().front || GameObject.Find("Plane").GetComponent<PlayerController>().back || GameObject.Find("Plane").GetComponent<PlayerController>().left || GameObject.Find("Plane").GetComponent<PlayerController>().right)
            {
                GameObject.Find("Plane").GetComponent<PlayerController>().targetMultiplier = multiplier;
                GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowTarget(multiplier);
            }
            else
            {
                GameControl.control.score += GameObject.Find("Plane").GetComponent<PlayerController>().avoidObstacle;
            }
        }
        Debug.Log("ENTRA");
        Destroy(gameObject);
    }
}
