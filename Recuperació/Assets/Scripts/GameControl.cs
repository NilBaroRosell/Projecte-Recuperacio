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
    public bool firstCheck;

    public bool[] levels;
    
    public static GameControl control;
    private GameObject checkpoints;

    private GameObject Plane, AlexPlane, EmilyPlane, WilliamPlane, secondCheckPoint;

    public int score;
    public int penaltyTime;

    public float finalScore;

    public float startTimer;
    public float finishTimer;
    public bool stopTimer;
    public string finalTimer;
    public bool nextEvent;
    public int actualEvent;
    public bool coinsActivated, timerActivated, penaltyActivated, boostActivated, messagesActivated, targetActivated, onPointActivated, movementActivated, tricksActivated;
    private string alexTime, emilyTime, williamTime;
    public int playerScore, alexScore, emilyScore, williamScore, playerGlobal, alexGlobal, emilyGlobal, williamGlobal;

    public AudioSource menuMusic, gameMusic, planeSound;

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

        playerGlobal = 0;
        alexGlobal = 0;
        emilyGlobal = 0;
        williamGlobal = 0;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Random.InitState(42);

        if (Input.GetKeyDown(KeyCode.X))
        {
            level=3;
            levels[level - 1] = true;
            saveData();
            GameObject.Find("MenuController").GetComponent<MenuControl>().loadLevel(level - 1);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            level = 2;
            levels[level - 1] = true;
            saveData();
            GameObject.Find("MenuController").GetComponent<MenuControl>().loadLevel(level - 1);
        }

        if (SceneManager.GetActiveScene().name != "Menus") LevelsControl();

        if (Input.GetKey(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menus")
        {
            Time.timeScale = 0.0f;
            GameObject.Find("MenuController").GetComponent<MenuControl>().goPauseMenu();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gamePaused = true;
            gameMusic.Stop();
            planeSound.Stop();
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
                GameObject.Find("PenaltyTime").GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
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
                Level2();
                break;
            case 3:
                Level3();
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
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }               
                break;
            case 2:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 3:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text, 30);
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMovementControls();
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 4:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    movementActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().movementControl = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMovementControls();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 5:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(3).GetComponent<Text>().text, 30); // message well done
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 6:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(4).GetComponent<Text>().text, 30); // message boost 1
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 7:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(5).GetComponent<Text>().text, 30); // message boost 2
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowBoost();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowBoostControl();
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 8:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage(); // boost controls shown & 5 sec to try boost
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().boostControl = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideBoostControl();
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 9:
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
            case 10:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(7).GetComponent<Text>().text, 30); // message boost 2
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 11:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage(); // boost controls shown & 5 sec to try boost
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 12:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(8).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(7.0f));
                }
                break;
            case 13:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(9).GetComponent<Text>().text, 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowTime();
                    StartCoroutine(ExecuteAfterTime(7.0f));
                }
                break;
            case 14:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(10).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 15:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(11).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(2.0f));
                }
                break;
            case 16:
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
            case 17:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been  " + finalTimer + ".", 30); // message 
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 18:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(12).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 19:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(13).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 20:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(14).GetComponent<Text>().text, 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowCoins();
                    StartCoroutine(ExecuteAfterTime(7.0f));
                }
                break;
            case 21:
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
            case 22:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been  " + finalTimer + " and you got " + score.ToString() + " points, so your final time is : " + calculateFinalTime(finishTimer, score, penaltyTime) + ".", 30); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(4.0f)); 
                }
                break;
            case 23:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(15).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 24:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowPenalty();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(16).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(6.0f));
                }
                break;
            case 25:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(17).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 26:
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
            case 27:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been  " + finalTimer + ", you got " + score.ToString() + " points and have been penalized with " + penaltyTime + " seconds, so your final time is : " + calculateFinalTime(finishTimer, score, penaltyTime) + ".", 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 28:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(18).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 29:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(19).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 30:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 31:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been  " + finalTimer + ", you got " + score.ToString() + " points and have been penalized with " + penaltyTime + " seconds, so your final time is : " + calculateFinalTime(finishTimer, score, penaltyTime) + ".", 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 32:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(20).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 33:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(21).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 34:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    boostActivated = true;
                    movementActivated = true;
                }
                break;
            case 35:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been  " + finalTimer + ", you got " + score.ToString() + " points and have been penalized with " + penaltyTime + " seconds, so your final time is : " + calculateFinalTime(finishTimer, score, penaltyTime) + ".", 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 36:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(22).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 37:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(23).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(6.0f));
                }
                break;
            case 38:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(24).GetComponent<Text>().text, 25); // message 
                    onPointActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowOnPoint();
                    StartCoroutine(ExecuteAfterTime(6.0f));
                }
                break;
            case 39:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(25).GetComponent<Text>().text, 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowTricksControls();
                    StartCoroutine(ExecuteAfterTime(7.0f));
                }
                break;
            case 40:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    tricksActivated = true;
                    boostActivated = true;
                    movementActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().tricksControl = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideTricksControls();
                }
                break;
            case 41:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    tricksActivated = false;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been  " + finalTimer + ", you got " + score.ToString() + " points and have been penalized with " + penaltyTime + " seconds, so your final time is : " + calculateFinalTime(finishTimer, score, penaltyTime) + ".", 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 42:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(26).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 43:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(27).GetComponent<Text>().text, 25); // message 
                    StartCoroutine(ExecuteAfterTime(7.0f));
                }
                break;
            case 44:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(28).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 45:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(29).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 46:
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
            case 47:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    tricksActivated = false;
                    boostActivated = false;
                    movementActivated = false;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been  " + finalTimer + ", you got " + score.ToString() + " points and have been penalized with " + penaltyTime + " seconds, so your final time is : " + calculateFinalTime(finishTimer, score, penaltyTime) + ".", 25); // message 
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 48:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(30).GetComponent<Text>().text, 30); // message 
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 49:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("Camera").GetComponent<CameraControl>().zoomOut = true;
                    GameObject.Find("Camera").GetComponent<CameraControl>().stopPosition = GameObject.Find("Camera").transform.position;
                }
                break;
            case 50:
                if(nextEvent)
                {
                    nextEvent = false;
                    level++;
                    levels[level - 1] = true;
                    saveData();
                    GameObject.Find("MenuController").GetComponent<MenuControl>().Fade(level - 1);
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
            case 1:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    messagesActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 2:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowHUD2();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    boostActivated = true;
                    coinsActivated = true;
                    messagesActivated = true;
                    movementActivated = true;
                    onPointActivated = true;
                    penaltyActivated = true;
                    targetActivated = true;
                    timerActivated = true;
                    tricksActivated = true;
                }
                break;
            case 3:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been " + calculateFinalTime(finishTimer, score, penaltyTime) + ". A new record. Now it’s Alex’s turn.", 30);
                    boostActivated = false;
                    coinsActivated = false;
                    movementActivated = false;
                    onPointActivated = false;
                    penaltyActivated = false;
                    targetActivated = false;
                    timerActivated = false;
                    tricksActivated = false;
                    getTimes1();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 4:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideHUD();
                    GameObject.Find("TERRAIN").GetComponent<TerrainController>().showSecondTime();
                    GameObject.Find("Fade").GetComponent<Fade>().skip = false;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 5:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 6:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 7:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 8:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("Fade").GetComponent<Fade>().skip = true;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 9:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 10:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    if(playerScore > alexScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + alexTime + ". He’s in second position right behind you. Now it’s Emily’s turn.", 30);
                    else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + alexTime + ". He’s right now in first position, you’re second for now. Now it’s Emily’s turn.", 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 11:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("Fade").GetComponent<Fade>().skip = false;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 12:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 13:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 14:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 15:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("Fade").GetComponent<Fade>().skip = true;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 16:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 17:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    if (playerScore > emilyScore)
                    {
                        if(playerScore > alexScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. Her time is " + emilyTime + ". She’s in third position, you're still first. Now it’s William’s turn.", 30);
                        else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. Her time is " + emilyTime + ". She’s in third position, you're still second. Now it’s William’s turn.", 30);
                    }
                    else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. Her time is " + emilyTime + ". She’s right now in second position, you’re third for now. Now it’s William’s turn.", 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 18:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("Fade").GetComponent<Fade>().skip = false;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 19:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 20:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(3).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 21:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 22:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("Fade").GetComponent<Fade>().skip = true;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 23:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 24:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    if (playerScore > williamScore)
                    {
                        if (playerScore > alexScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s in fourth position, you’re still first. Let’s have a look at the race results.", 30);
                        else if(playerScore > emilyScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s in fourth position, you’re still second. Let’s have a look at the race results.", 30);
                        else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s in fourth position right behind you. Let’s have a look at the race results.", 30);
                    }
                    else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s right now in third position, you’re fourth for now. Let’s have a look at the race results.", 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 25:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    WilliamPlane.GetComponent<PlaneMovement>().active = false;
                    planeSound.Stop();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().printRaceResults();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 26:
                if(nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideResults();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(4).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(3.0f));
                }
                break;
            case 27:
                if(nextEvent)
                {
                    nextEvent = false;
                    level++;
                    levels[level - 1] = true;
                    saveData();
                    GameObject.Find("MenuController").GetComponent<MenuControl>().Fade(level - 1);
                }
                break;
            default:
                break;
        }
    }

    public void Level3()
    {
        switch (actualEvent)
        {
            case 1:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    messagesActivated = true;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 2:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowHUD2();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    boostActivated = true;
                    coinsActivated = true;
                    messagesActivated = true;
                    movementActivated = true;
                    onPointActivated = true;
                    penaltyActivated = true;
                    targetActivated = true;
                    timerActivated = true;
                    tricksActivated = true;
                }
                break;
            case 3:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations, your time has been " + calculateFinalTime(finishTimer, score, penaltyTime) + ". A new record. Now it’s Alex’s turn.", 30);
                    boostActivated = false;
                    coinsActivated = false;
                    movementActivated = false;
                    onPointActivated = false;
                    penaltyActivated = false;
                    targetActivated = false;
                    timerActivated = false;
                    tricksActivated = false;
                    getTimes2();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 4:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideHUD();
                    GameObject.Find("TERRAIN").GetComponent<TerrainController>().showSecondTime();
                    GameObject.Find("Fade").GetComponent<Fade>().skip = false;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 5:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 6:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 7:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 8:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("Fade").GetComponent<Fade>().skip = true;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 9:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 10:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    if (playerScore > alexScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + alexTime + ". He’s in second position right behind you. Now it’s Emily’s turn.", 30);
                    else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + alexTime + ". He’s right now in first position, you’re second for now. Now it’s Emily’s turn.", 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 11:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("Fade").GetComponent<Fade>().skip = false;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 12:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 13:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 14:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 15:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("Fade").GetComponent<Fade>().skip = true;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 16:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 17:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    if (playerScore > alexScore)
                    {
                        if (playerScore > emilyScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. Her time is " + emilyTime + ". She’s in second position right behind you. Now it’s William’s turn.", 30);
                        else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. Her time is " + emilyTime + ". She’s in first position,  you’re second for now.Now it’s William’s turn.", 30);
                    }
                    else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. Her time is " + emilyTime + ". She’s right now in first position, you’re third for now. Now it’s William’s turn.", 30);
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 18:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("Fade").GetComponent<Fade>().skip = false;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 19:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 20:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(3).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 21:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 22:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("Fade").GetComponent<Fade>().skip = true;
                    GameObject.Find("Fade").GetComponent<Fade>().FadeIn();
                }
                break;
            case 23:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    StartCoroutine(ExecuteAfterTime(1.0f));
                }
                break;
            case 24:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    if (playerScore > emilyScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s right now in third position, you’re still first. Let’s have a look at the race results.", 30);
                    else
                    {
                        if (playerScore > williamScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s in third position right behind you. Let’s have a look at the race results.", 30);
                        else if (playerScore > alexScore) GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s in second position, you’re third for now. Let’s have a look at the race results.", 30);
                        else GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: And it’s done. His time is " + williamTime + ". He’s in second position, you're fourth for now. Let’s have a look at the race results.", 30);
                    }
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 25:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    WilliamPlane.GetComponent<PlaneMovement>().active = false;
                    planeSound.Stop();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().printRaceResults2();
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 26:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideResults();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: " + GameObject.Find("Messages").transform.GetChild(1).transform.GetChild(4).GetComponent<Text>().text, 30);
                    StartCoroutine(ExecuteAfterTime(4.0f));
                }
                break;
            case 27:
                if (nextEvent)
                {
                    if (nextEvent)
                    {
                        nextEvent = false;
                        actualEvent++;
                        WilliamPlane.GetComponent<PlaneMovement>().active = false;
                        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideMessage();
                        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().printChampionshipResults();
                        StartCoroutine(ExecuteAfterTime(5.0f));
                    }
                }
                break;
            case 28:
                if (nextEvent)
                {
                    nextEvent = false;
                    actualEvent++;
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideResults();
                    if (playerGlobal >= emilyGlobal)
                    {
                        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations! You’re the campion. Enjoy your well deserved victory.", 30);
                        GameObject.Find("Trophy1st").GetComponent<MeshRenderer>().enabled = true;
                        GameObject.Find("Trophy1st").transform.position = WilliamPlane.transform.GetChild(2).transform.position + new Vector3(0, -3.6f, 7.5f);
                    }
                    else if (playerGlobal >= alexGlobal)
                    {
                        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations! You were so close in your debut, keep up the good work.", 30);
                        GameObject.Find("Trophy2nd").GetComponent<MeshRenderer>().enabled = true;
                        GameObject.Find("Trophy2nd").transform.position = WilliamPlane.transform.GetChild(2).transform.position + new Vector3(0, -3.6f, 7.5f);
                    }
                    else if (playerGlobal >= williamGlobal)
                    {
                        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: Congratulations! You have achieved a top 3 position in your debut, keep up the good work.", 30);
                        GameObject.Find("Trophy3rd").GetComponent<MeshRenderer>().enabled = true;
                        GameObject.Find("Trophy3rd").transform.position = WilliamPlane.transform.GetChild(2).transform.position + new Vector3(0, -3.6f, 7.5f);
                    }
                    else
                    {
                        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ShowMessage("Dany: You’ve done everything you could, we’re proud. Next time will be your chance.", 30);
                    }
                    StartCoroutine(ExecuteAfterTime(5.0f));
                }
                break;
            case 29:
                if(nextEvent)
                { 
                    nextEvent = false;
                    levels[level] = true;
                    saveData();
                    GameObject.Find("MenuController").GetComponent<MenuControl>().goMainMenuFromPause();
                }
                break;
            default:
                break;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        StopAllCoroutines();
        if (SceneManager.GetActiveScene().name == "Menus")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuMusic.Play();
            gameMusic.Stop();
            planeSound.Stop();
        }
        else
        {
            menuMusic.Stop();
            gameMusic.Play();
            planeSound.Play();
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            switch (level)
            {
                case 2:
                    GameObject.Find("Plane").GetComponent<PlayerController>().speed = 2.0f;
                    GameObject.Find("Plane").GetComponent<PlayerController>().Start();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideHUD();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    boostActivated = false;
                    coinsActivated = false;
                    messagesActivated = false;
                    movementActivated = false;
                    onPointActivated = false;
                    penaltyActivated = false;
                    targetActivated = false;
                    timerActivated = false;
                    tricksActivated = false;
                    gamePaused = false;
                    score = 0;
                    penaltyTime = 0;
                    stopTimer = true;
                    nextEvent = false;
                    actualEvent = 1;
                    //StartCoroutine(ExecuteAfterTime(3.0f));
                    break;
                case 3:
                    if (GameObject.Find("Plane") != null)
                    {
                        Plane = GameObject.Find("Plane");
                        Plane.SetActive(true);
                    }
                    if (GameObject.Find("AlexPlane") != null)
                    {
                        AlexPlane = GameObject.Find("AlexPlane");
                        AlexPlane.GetComponent<MeshRenderer>().enabled = false;
                        AlexPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        AlexPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        AlexPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = false;
                    }
                    if (GameObject.Find("EmilyPlane") != null)
                    {
                        EmilyPlane = GameObject.Find("EmilyPlane");
                        EmilyPlane.GetComponent<MeshRenderer>().enabled = false;
                        EmilyPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        EmilyPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        EmilyPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = false;
                    }
                    if (GameObject.Find("WilliamPlane") != null)
                    {
                        WilliamPlane = GameObject.Find("WilliamPlane");
                        WilliamPlane.GetComponent<MeshRenderer>().enabled = false;
                        WilliamPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        WilliamPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        WilliamPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = false;
                    }
                    GameObject.Find("Plane").GetComponent<PlayerController>().speed = 2.0f;
                    GameObject.Find("Plane").GetComponent<PlayerController>().Start();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideHUD();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    boostActivated = false;
                    coinsActivated = false;
                    messagesActivated = false;
                    movementActivated = false;
                    onPointActivated = false;
                    penaltyActivated = false;
                    targetActivated = false;
                    timerActivated = false;
                    tricksActivated = false;
                    gamePaused = false;
                    score = 0;
                    penaltyTime = 0;
                    playerScore = 0;
                    alexScore = 0;
                    emilyScore = 0;
                    williamScore = 0;
                    playerGlobal = 0;
                    alexGlobal = 0;
                    emilyGlobal = 0;
                    williamGlobal = 0;
                    stopTimer = true;
                    nextEvent = false;
                    actualEvent = 1;
                    break;
                case 4:
                    if (GameObject.Find("Plane") != null)
                    {
                        Plane = GameObject.Find("Plane");
                        Plane.SetActive(true);
                    }
                    if (GameObject.Find("AlexPlane") != null)
                    {
                        AlexPlane = GameObject.Find("AlexPlane");
                        AlexPlane.GetComponent<MeshRenderer>().enabled = false;
                        AlexPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        AlexPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        AlexPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = false;
                    }
                    if (GameObject.Find("EmilyPlane") != null)
                    {
                        EmilyPlane = GameObject.Find("EmilyPlane");
                        EmilyPlane.GetComponent<MeshRenderer>().enabled = false;
                        EmilyPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        EmilyPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        EmilyPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = false;
                    }
                    if (GameObject.Find("WilliamPlane") != null)
                    {
                        WilliamPlane = GameObject.Find("WilliamPlane");
                        WilliamPlane.GetComponent<MeshRenderer>().enabled = false;
                        WilliamPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        WilliamPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        WilliamPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = false;
                    }
                    GameObject.Find("Plane").GetComponent<PlayerController>().speed = 2.0f;
                    GameObject.Find("Plane").GetComponent<PlayerController>().Start();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideHUD();
                    GameObject.Find("CanvasInGame").GetComponent<HUDControler>().ResetHUD();
                    boostActivated = false;
                    coinsActivated = false;
                    messagesActivated = false;
                    movementActivated = false;
                    onPointActivated = false;
                    penaltyActivated = false;
                    targetActivated = false;
                    timerActivated = false;
                    tricksActivated = false;
                    gamePaused = false;
                    score = 0;
                    penaltyTime = 0;
                    playerScore = 0;
                    alexScore = 0;
                    emilyScore = 0;
                    williamScore = 0;
                    playerGlobal = 0;
                    alexGlobal = 0;
                    emilyGlobal = 0;
                    williamGlobal = 0;
                    stopTimer = true;
                    nextEvent = false;
                    actualEvent = 1;
                    if (playerGlobal == 0)
                    {
                        playerGlobal = 5;
                        alexGlobal = 3;
                        emilyGlobal = 2;
                        williamGlobal = 1;
                    }
                    break;
                default:
                    break;
            }
        }
        if (GameObject.Find("CHECKPOINTS") != null) checkpoints = GameObject.Find("CHECKPOINTS");
    }

    public void change()
    {
        if (Plane.activeSelf)
        {
            AlexPlane.GetComponent<MeshRenderer>().enabled = true;
            AlexPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
            AlexPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
            AlexPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = true;
            AlexPlane.GetComponent<PlaneMovement>().active = true;
            Plane.SetActive(false);
        }
        else if (AlexPlane.activeSelf)
        {
            EmilyPlane.GetComponent<MeshRenderer>().enabled = true;
            EmilyPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
            EmilyPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
            EmilyPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = true;
            EmilyPlane.GetComponent<PlaneMovement>().active = true;
            AlexPlane.SetActive(false);
        }
        else if (EmilyPlane.activeSelf)
        {
            WilliamPlane.GetComponent<MeshRenderer>().enabled = true;
            WilliamPlane.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
            WilliamPlane.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
            WilliamPlane.transform.GetChild(2).gameObject.GetComponent<Camera>().enabled = true;
            WilliamPlane.GetComponent<PlaneMovement>().active = true;
            EmilyPlane.SetActive(false);
        }
    }

    public void getTimes1()
    {
        if (penaltyTime > 125)
        {
            if(score > 5000)
            {
                float alex = (float)Random.Range(finalScore - 20, finalScore - 10);
                float emily = (float)Random.Range(finalScore - 10, finalScore);
                float william = (float)Random.Range(finalScore, finalScore + 10);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                alexScore += 5;
                emilyScore += 3;
                playerScore += 2;
                williamScore += 1;
                alexGlobal += 5;
                emilyGlobal += 3;
                playerGlobal += 2;
                williamGlobal += 1;
            }
            else
            {
                float alex = (float)Random.Range(finalScore - 30, finalScore - 20);
                float emily = (float)Random.Range(finalScore - 20, finalScore - 10);
                float william = (float)Random.Range(finalScore - 10, finalScore);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                alexScore += 5;
                emilyScore += 3;
                williamScore += 2;
                playerScore += 1;
                alexGlobal += 5;
                emilyGlobal += 3;
                williamGlobal += 2;
                playerGlobal += 1;
            }
        }
        else if (penaltyTime > 75)
        {
            if(score > 4000)
            {
                float alex = (float)Random.Range(finalScore - 10, finalScore);
                float emily = (float)Random.Range(finalScore, finalScore + 10);
                float william = (float)Random.Range(finalScore + 10, finalScore + 20);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                alexScore += 5;
                playerScore += 3;
                emilyScore += 2;
                williamScore += 1;
                alexGlobal += 5;
                playerGlobal += 3;
                emilyGlobal += 2;
                williamGlobal += 1;
            }
            else
            {
                float alex = (float)Random.Range(finalScore - 20, finalScore - 10);
                float emily = (float)Random.Range(finalScore - 10, finalScore);
                float william = (float)Random.Range(finalScore, finalScore + 10);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                alexScore += 5;
                emilyScore += 3;
                playerScore += 2;
                williamScore += 1;
                alexGlobal += 5;
                emilyGlobal += 3;
                playerGlobal += 2;
                williamGlobal += 1;
            }
        }
        else if(penaltyTime > 40)
        {
            if(score > 4000)
            {
                float alex = (float)Random.Range(finalScore, finalScore + 10);
                float emily = (float)Random.Range(finalScore + 10, finalScore + 20);
                float william = (float)Random.Range(finalScore + 20, finalScore + 30);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                playerScore += 5;
                alexScore += 3;
                emilyScore += 2;
                williamScore += 1;
                playerGlobal += 5;
                alexGlobal += 3;
                emilyGlobal += 2;
                williamGlobal += 1;
            }
            else
            {
                float alex = (float)Random.Range(finalScore - 10, finalScore);
                float emily = (float)Random.Range(finalScore, finalScore + 10);
                float william = (float)Random.Range(finalScore + 10, finalScore + 20);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                alexScore += 5;
                playerScore += 3;
                emilyScore += 2;
                williamScore += 1;
                alexGlobal += 5;
                playerGlobal += 3;
                emilyGlobal += 2;
                williamGlobal += 1;
            }
        }
        else
        {
            float alex = (float)Random.Range(finalScore, finalScore + 10);
            float emily = (float)Random.Range(finalScore + 10, finalScore + 20);
            float william = (float)Random.Range(finalScore + 20, finalScore + 30);
            alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
            emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
            williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

            playerScore += 5;
            alexScore += 3;
            emilyScore += 2;
            williamScore += 1;
            playerGlobal += 5;
            alexGlobal += 3;
            emilyGlobal += 2;
            williamGlobal += 1;
        }
    }

    public void getTimes2()
    {
        if (penaltyTime > 125)
        {
            if (score > 4000)
            {
                float emily = (float)Random.Range(finalScore - 20, finalScore - 10);
                float william = (float)Random.Range(finalScore - 10, finalScore);
                float alex = (float)Random.Range(finalScore, finalScore + 10);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                emilyScore += 5;
                alexScore += 3;
                playerScore += 2;
                williamScore += 1;
                emilyGlobal += 5;
                alexGlobal += 3;
                playerGlobal += 2;
                williamGlobal += 1;
            }
            else
            {
                float emily = (float)Random.Range(finalScore - 30, finalScore - 20);
                float william = (float)Random.Range(finalScore - 20, finalScore - 10);
                float alex = (float)Random.Range(finalScore - 10, finalScore);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                emilyScore += 5;
                alexScore += 3;
                williamScore += 2;
                playerScore += 1;
                emilyGlobal += 5;
                alexGlobal += 3;
                williamGlobal += 2;
                playerGlobal += 1;
            }
        }
        else if (penaltyTime > 75)
        {
            if (score > 4000)
            {
                float emily = (float)Random.Range(finalScore - 10, finalScore);
                float william = (float)Random.Range(finalScore, finalScore + 10);
                float alex = (float)Random.Range(finalScore + 10, finalScore + 20);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                emilyScore += 5;
                playerScore += 3;
                alexScore += 2;
                williamScore += 1;
                emilyGlobal += 5;
                playerGlobal += 3;
                alexGlobal += 2;
                williamGlobal += 1;
            }
            else
            {
                float emily = (float)Random.Range(finalScore - 20, finalScore - 10);
                float william = (float)Random.Range(finalScore - 10, finalScore);
                float alex = (float)Random.Range(finalScore, finalScore + 10);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                emilyScore += 5;
                alexScore += 3;
                playerScore += 2;
                williamScore += 1;
                emilyGlobal += 5;
                alexGlobal += 3;
                playerGlobal += 2;
                williamGlobal += 1;
            }
        }
        else if (penaltyTime > 40)
        {
            if (score > 4000)
            {
                float emily = (float)Random.Range(finalScore, finalScore + 10);
                float william = (float)Random.Range(finalScore + 10, finalScore + 20);
                float alex = (float)Random.Range(finalScore + 20, finalScore + 30);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                playerScore += 5;
                emilyScore += 3;
                alexScore += 2;
                williamScore += 1;
                playerGlobal += 5;
                emilyGlobal += 3;
                alexGlobal += 2;
                williamGlobal += 1;
            }
            else
            {
                float emily = (float)Random.Range(finalScore - 10, finalScore);
                float william = (float)Random.Range(finalScore, finalScore + 10);
                float alex = (float)Random.Range(finalScore + 10, finalScore + 20);
                alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
                emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
                williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

                emilyScore += 5;
                playerScore += 3;
                alexScore += 2;
                williamScore += 1;
                emilyGlobal += 5;
                playerGlobal += 3;
                alexGlobal += 2;
                williamGlobal += 1;
            }
        }
        else
        {
            float emily = (float)Random.Range(finalScore, finalScore + 10);
            float william = (float)Random.Range(finalScore + 10, finalScore + 20);
            float alex = (float)Random.Range(finalScore + 20, finalScore + 30);
            alexTime = ((int)alex / 60).ToString() + ":" + (alex % 60).ToString("f2");
            emilyTime = ((int)emily / 60).ToString() + ":" + (emily % 60).ToString("f2");
            williamTime = ((int)william / 60).ToString() + ":" + (william % 60).ToString("f2");

            playerScore += 5;
            emilyScore += 3;
            alexScore += 2;
            williamScore += 1;
            playerGlobal += 5;
            emilyGlobal += 3;
            alexGlobal += 2;
            williamGlobal += 1;
        }
    }

    public string calculateFinalTime(float time, int score, int penalty)
    {
        time += penalty;
        time -= (float)score / 100;
        finalScore = time;
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
