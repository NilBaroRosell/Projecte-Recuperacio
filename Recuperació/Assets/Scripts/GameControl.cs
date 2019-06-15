using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    private GameObject checkpoints;

    public int score;
    public int penaltyTime;

    public float startTimer;
    public float finishTimer;
    public bool stopTimer;
    public string finalTimer;
    public bool nextEvent;
    private int actualEvent;
    public bool coinsActivated, timerActivated, penaltyActivated, boostActivated, messagesActivated, targetActivated, onPointActivated, movementActivated, tricksActivated;

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
        score = 0;
        penaltyTime = 0;
        stopTimer = true;
        nextEvent = false;
        actualEvent = 1;
        boostActivated = false;
        coinsActivated = false;
        messagesActivated = false;
        movementActivated = false;
        onPointActivated = false;
        penaltyActivated = false;
        targetActivated = false;
        timerActivated = false;
        tricksActivated = false;

        if (GameObject.Find("CHECKPOINTS") != null) checkpoints = GameObject.Find("CHECKPOINTS");

        switch (level)
        {
            case 1:
                GameObject.Find("Plane").GetComponent<PlayerController>().speed = 2.0f;
                GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideHUD();
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LevelsControl();

        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            GameObject.Find("MenuController").GetComponent<MenuControl>().goPauseMenu();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gamePaused = true;
        }

        if (GameObject.Find("ScoreNumber") != null) GameObject.Find("ScoreNumber").GetComponent<TextMeshProUGUI>().text = score.ToString();
        if (GameObject.Find("PenaltyTime") != null)
        {
            if(penaltyTime > 0)
            {
                GameObject.Find("PenaltyTime").GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
                GameObject.Find("PenaltyTime").GetComponent<TextMeshProUGUI>().text = "+" + penaltyTime.ToString() + " seconds";
            }
            else if (penaltyTime < 0)
            {
                GameObject.Find("PenaltyTime").GetComponent<TextMeshProUGUI>().color = new Color32 (0, 255, 0, 255);
                GameObject.Find("PenaltyTime").GetComponent<TextMeshProUGUI>().text = penaltyTime.ToString() + " seconds";
            }
            else
            {
                GameObject.Find("PenaltyTime").GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("PenaltyTime").GetComponent<TextMeshProUGUI>().text = "+" + penaltyTime.ToString() + " seconds";
            }
        }

        if(timerActivated)
        {
            if (!stopTimer)
            {
                finishTimer = Time.time - startTimer;
                finalTimer = ((int)finishTimer / 60).ToString() + ":" + (finishTimer % 60).ToString("f2");
                if (GameObject.Find("Time") != null) GameObject.Find("Time").GetComponent<TextMeshProUGUI>().text = finalTimer;
            }
        }
    }

    public void LevelsControl()
    {
        switch(level)
        {
            case 1:
                Level1();
                break;
            case 2:
            break;
            case 3:
            break;
            default: break;
        }
    }

    public void Level1()
    {
        switch(actualEvent)
        {
            case 1:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    messagesActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }               
                break;
            case 2:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text, 30); //Message load and clear
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 3:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text, 30);//Message we must check some stuff
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 4:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text, 30);//Message please move
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 5:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text, 30); //Message let's move
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 6:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    movementActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage(); // movement controls shown & 10 sec to move
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().movementControl = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMovementControls();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 7:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(3).GetComponent<Text>().text, 30); // message well done
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 8:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(4).GetComponent<Text>().text, 30); // message boost 1
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 9:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(5).GetComponent<Text>().text, 30); // message boost 2
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowBoost();
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 10:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text, 30); // message let's do it
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowBoost();
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 11:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage(); // boost controls shown & 5 sec to try boost
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().boostControl = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowBoostControl();
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 12:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(6).GetComponent<Text>().text, 30); // message boost 2
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 13:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(7).GetComponent<Text>().text, 30); // message boost 2
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 14:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text, 30); // message let's do it
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 15:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage(); // boost controls shown & 5 sec to try boost
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 16:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(8).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 17:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(9).GetComponent<Text>().text, 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowTime();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 18:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(10).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 19:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(11).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 20:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 21:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    timerActivated = true;
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 22:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Enhorabona, el teu temps ha sigut de " + finalTimer, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 23:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(12).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 24:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(13).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 25:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(14).GetComponent<Text>().text, 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowCoins();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 26:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 27:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    coinsActivated = true;
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 28:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Enhorabona, el teu temps ha sigut de " + finalTimer + " i has aconseguit " + score.ToString() + " punts, per tant el teu temps final ha sigut: " + calculateFinalTime(finishTimer, score, penaltyTime), 30); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(3.0f)); 
                }
                break;
            case 29:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(15).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 30:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowPenalty();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(16).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 31:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(17).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 32:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 33:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    penaltyActivated = true;
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 34:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Enhorabona, el teu temps ha sigut de " + finalTimer + ", has aconseguit " + score.ToString() + " punts i has tingut " + penaltyTime + " segons de penalització, per tant el teu temps final ha sigut: " + calculateFinalTime(finishTimer, score, penaltyTime), 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 35:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(18).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 36:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(19).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 37:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 38:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 39:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Enhorabona, el teu temps ha sigut de " + finalTimer + ", has aconseguit " + score.ToString() + " punts i has tingut " + penaltyTime + " segons de penalització, per tant el teu temps final ha sigut: " + calculateFinalTime(finishTimer, score, penaltyTime), 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 40:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(20).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 41:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(21).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 42:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 43:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 44:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Enhorabona, el teu temps ha sigut de " + finalTimer + ", has aconseguit " + score.ToString() + " punts i has tingut " + penaltyTime + " segons de penalització, per tant el teu temps final ha sigut: " + calculateFinalTime(finishTimer, score, penaltyTime), 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 45:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(22).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 46:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(23).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 47:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(24).GetComponent<Text>().text, 25); // message 
                    onPointActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowOnPoint();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 48:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(25).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(6.0f));
                }
                break;
            case 49:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 50:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    tricksActivated = true;
                    boostActivated = true;
                    movementActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().tricksControl = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowTricksControls();
                }
                break;
            case 51:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    tricksActivated = false;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Enhorabona, el teu temps ha sigut de " + finalTimer + ", has aconseguit " + score.ToString() + " punts i has tingut " + penaltyTime + " segons de penalització, per tant el teu temps final ha sigut: " + calculateFinalTime(finishTimer, score, penaltyTime), 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 52:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(26).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 53:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(27).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 54:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(28).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 55:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(29).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 56:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 57:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    targetActivated = true;
                    tricksActivated = true;
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 58:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    tricksActivated = false;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Enhorabona, el teu temps ha sigut de " + finalTimer + ", has aconseguit " + score.ToString() + " punts i has tingut " + penaltyTime + " segons de penalització, per tant el teu temps final ha sigut: " + calculateFinalTime(finishTimer, score, penaltyTime), 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 59:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(30).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 60:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Robert: " + GameObject.Find("Messages").transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 61:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("Camera").GetComponent<CameraControl>().zoomOut = true;
                    GameObject.Find("Camera").GetComponent<CameraControl>().stopPosition = GameObject.Find("Camera").transform.position;
                }
                break;
            case 62:
                if(nextEvent)
                {
                    nextEvent = false;
                    level++;
                    levels[level - 1] = true;
                    saveData();
                    GameObject.Find("MenuController").GetComponent<MenuControl>().loadLevel(level - 1);
                }
                break;
            default:
                break;
        }
    }

    public void Level2()
    {
        switch (actualEvent)
        {

        }
    }

    public void Level3()
    {
        switch (actualEvent)
        {

        }
    }

    public string calculateFinalTime(float time, int score, int penalty)
    {
        time += penalty;
        time -= (float)score / 100;
        return ((int)time / 60).ToString() + ":" + (time % 60).ToString("f2");
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

    public IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        nextEvent = true;
    }
}
