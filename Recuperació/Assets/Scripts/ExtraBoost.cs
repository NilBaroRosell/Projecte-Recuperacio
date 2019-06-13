using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBoost : MonoBehaviour {

    public int boostExtra;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().boostAmount += boostExtra;
            if (other.gameObject.GetComponent<PlayerController>().boostAmount > 100) other.gameObject.GetComponent<PlayerController>().boostAmount = 100;
            Destroy(gameObject);
        }
    }
}
