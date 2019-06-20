using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    public int coinsToAdd;
    public bool stop = false;
    public AudioSource source;

    private void Update()
    {
        if (!GameControl.control.gamePaused) gameObject.transform.Rotate(0.0f, 0.0f, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameControl.control.score += coinsToAdd;
            source.Play();
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
