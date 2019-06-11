using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinished : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(GameObject.Find("GameController").GetComponent<GameControl>().level);

        if (other.gameObject.tag == "Player" && Input.GetButton("Use"))
        {
            Debug.Log("Object touched");

            if (GameObject.Find("GameController").GetComponent<GameControl>().level <= 3)
            {
                GameObject.Find("GameController").GetComponent<GameControl>().level++;
                GameObject.Find("GameController").GetComponent<GameControl>().levels[GameObject.Find("GameController").GetComponent<GameControl>().level - 1] = true;
                GameObject.Find("GameController").GetComponent<GameControl>().saveData();

                if(GameObject.Find("GameController").GetComponent<GameControl>().level == 4)
                {
                    GameObject.Find("GameController").GetComponent<GameControl>().level = 3;
                    SceneManager.LoadScene("Menus");
                }
                else GameObject.Find("MenuController").GetComponent<MenuControl>().loadLevel(GameObject.Find("GameController").GetComponent<GameControl>().level - 1);
            }
        }
    }
}
