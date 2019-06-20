using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBoost : MonoBehaviour {

    public int boostExtra;
    public bool stop = false;
    public AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().boostAmount += boostExtra;
            source.Play();
            if (other.gameObject.GetComponent<PlayerController>().boostAmount > 100) other.gameObject.GetComponent<PlayerController>().boostAmount = 100;
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
