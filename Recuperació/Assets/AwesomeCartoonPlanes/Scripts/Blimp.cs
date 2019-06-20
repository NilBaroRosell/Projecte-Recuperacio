using UnityEngine;
using System.Collections;

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //

public class Blimp : MonoBehaviour {
	public GameObject prop1;
	public GameObject prop2;
	public GameObject prop1Blured;
	public GameObject prop2Blured;

	public bool engenOn;

	void Update () 
	{
		if (engenOn) {
			prop1.SetActive (false);
			prop2.SetActive (false);
			prop1Blured.SetActive (true);
			prop2Blured.SetActive (true);
			prop1Blured.transform.Rotate (1000 * Time.deltaTime, 0, 0);
			prop2Blured.transform.Rotate (1000 * Time.deltaTime, 0, 0);
		} else {
			prop1.SetActive (true);
			prop2.SetActive (true);
			prop1Blured.SetActive (false);
			prop2Blured.SetActive (false);
		}
	}
}

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //