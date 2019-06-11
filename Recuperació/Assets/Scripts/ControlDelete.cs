using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDelete : MonoBehaviour {


    public bool delete;

	void OnApplicationQuit () {
        if (delete) SaveSystem.deleteData();
	}
	
}
