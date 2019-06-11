using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public int level, lastLevelPlayed;
    public bool music;
    public float volume;
    public bool fullscreen;
    public int[] resolution;
    public bool continueAvailable;
    public bool gamePaused;

    public bool[] levels;
    
    public static GameControl control;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);
        
        if(control == null)
        {
            control = this;
        }
        else
        {
            Destroy(gameObject);
        }

        resolution = new int[2];
        levels = new bool[5];
        loadData();

        for (int i = 0; i < level; i++)
        {
            levels[i] = true;
        }
        for (int i = level; i < 5; i++)
        {
            levels[i] = false;
        }
	}

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Menus")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        gamePaused = false;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            GameObject.Find("MenuController").GetComponent<MenuControl>().goPauseMenu();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gamePaused = true;
        }
	}

    public void saveData()
    {
        SaveSystem.savePlayer(this);
    }

    public void loadData()
    {
        PlayerData data = SaveSystem.loadPlayer();
        if ( data == null)
        {
            level = 1;
            lastLevelPlayed = 1;
            music = false;
            volume = -10;
            fullscreen = true;
            resolution[0] = 1920;
            resolution[1] = 1080;
            continueAvailable = false;
        }
        else
        {
            level = data.level;
            lastLevelPlayed = data.lastLevelPlayed;
            music = data.music;
            volume = data.volume;
            fullscreen = data.fullscreen;
            resolution = data.resolution;
            continueAvailable = true;
        }
    }
}
