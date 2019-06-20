using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public bool stop = false;
    public AudioSource source;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameControl.control.penaltyTime += 5;
            source.Play();
        }
        StartCoroutine(ShowAfterTime(5.0f));
        if (!stop)
        {
            gameObject.transform.position -= new Vector3(0, 0, 30);
            stop = true;
        }
    }

    public IEnumerator ShowAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.transform.position += new Vector3(0, 0, 30);
        stop = false;
    }
}
