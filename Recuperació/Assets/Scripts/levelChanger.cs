using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelChanger : MonoBehaviour {

    public Animator animator;
    public int levelToLoad;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void fadeToLevel(int level)
    {
        Time.timeScale = 1.0f;
        animator.SetTrigger("Fade_Out");
        levelToLoad = level;
    }

    public void onFadeCompleted()
    {
        animator.ResetTrigger("Fade_Out");
        GameObject.Find("MenuController").GetComponent<MenuControl>().loadLevel(levelToLoad);
    }
}
