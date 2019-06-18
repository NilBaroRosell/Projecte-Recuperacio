using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public int multiplier;
    public bool stop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (/*GameObject.Find("Plane").GetComponent<PlayerController>().front || GameObject.Find("Plane").GetComponent<PlayerController>().back ||*/ GameObject.Find("Plane").GetComponent<PlayerController>().left || GameObject.Find("Plane").GetComponent<PlayerController>().right)
            {
                GameObject.Find("Plane").GetComponent<PlayerController>().targetMultiplier = multiplier;
                GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowTarget(multiplier);
            }
            else
            {
                GameControl.control.score += GameObject.Find("Plane").GetComponent<PlayerController>().avoidObstacle;
            }
            StartCoroutine(ShowAfterTime(5.0f));
            if (!stop)
            {
                gameObject.transform.position -= new Vector3(0, 0, 30);
                stop = true;
            }
        }
    }

    public IEnumerator ShowAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.transform.position += new Vector3(0, 0, 30);
        stop = false;
    }
}
