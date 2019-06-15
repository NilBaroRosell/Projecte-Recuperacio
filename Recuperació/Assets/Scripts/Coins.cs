using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    public int coinsToAdd;

    private void Update()
    {
        gameObject.transform.Rotate(0.0f, 0.0f, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameControl.control.score += coinsToAdd;
            Destroy(gameObject);
        }
    }
}
