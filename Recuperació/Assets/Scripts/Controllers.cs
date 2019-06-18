using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllers : MonoBehaviour {

    //public bool narrowZone;
    public bool narrowZone;
    public bool first;
    private bool crossed;
    public bool stop = false;
    private bool wentThrough = false;

    // Use this for initialization
    void Start()
    {
        crossed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == ("Plane") && !wentThrough)
        {
            GameControl.control.penaltyTime += 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("Plane") && !crossed)
        {
            if (narrowZone)
            {
                GameControl.control.score += 20;
                wentThrough = true;
            }
            else
            {
                if (first)
                {
                    GameControl.control.score += 10;
                }
                else
                {
                    GameControl.control.score += 5;
                    crossed = true;
                }
            }
            StartCoroutine(ShowAfterTime(1.0f));
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
