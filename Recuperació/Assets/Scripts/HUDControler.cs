using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDControler : MonoBehaviour {

    public GameObject Multiplier;
    public GameObject Messages;
    public GameObject Boost;
    public GameObject Time;
    public GameObject Coins;
    public GameObject Penalty;
    public GameObject OnPoint;
    public GameObject results;
    public bool movementControl, boostControl, tricksControl;

    // Use this for initialization
    void Start () {
        movementControl = false;
        boostControl = false;
        tricksControl = false;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void HideHUD()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ShowHUD()
    {
        for (int i = 0; i < gameObject.transform.childCount - 1; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ShowHUD2()
    {
        for (int i = 4; i < gameObject.transform.childCount - 2; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ResetHUD()
    {
        Boost.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
        GameObject.Find("Plane").GetComponent<PlayerController>().boostAmount = 100;
        GameControl.control.score = 0;
        GameControl.control.penaltyTime = 0;
        GameControl.control.stopTimer = true;
        Time.GetComponent<TextMeshProUGUI>().text = "0:00:00";
    }

    public void ShowTarget(int multiplier)
    {
        Multiplier.SetActive(true);
        Multiplier.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
        Multiplier.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + multiplier.ToString();
    }

    public void DecreaseTarget(float amount)
    {
        Multiplier.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().fillAmount -= amount;
    }

    public void HideTarget()
    {
        Multiplier.SetActive(false);
    }

    public void ShowMessage(string textToShow, int size)
    {
        if(GameControl.control.messagesActivated)
        {
            Messages.SetActive(true);
            Messages.GetComponent<Image>().enabled = true;
            Messages.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
            Messages.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = size;
            Messages.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textToShow;
        }
    }

    public void HideMessage()
    {
        Messages.GetComponent<Image>().enabled = false;
        Messages.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void ShowBoost()
    {
        Boost.SetActive(true);
    }

    public void ShowTime()
    {
        Time.SetActive(true);
        Time.GetComponent<TextMeshProUGUI>().text = "0:00:00";
    }

    public void ShowCoins()
    {
        Coins.SetActive(true);
    }

    public void ShowPenalty()
    {
        Penalty.SetActive(true);
    }

    public void ShowOnPoint()
    {
        OnPoint.SetActive(true);
    }

    public void ShowMovementControls()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideMovementControls()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        movementControl = false;
    }

    public void ShowBoostControl()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void HideBoostControl()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        boostControl = false;
    }

    public void ShowTricksControls()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void HideTricksControls()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        tricksControl = false;
    }

    public void printRaceResults()
    {
        results.SetActive(true);

        if (GameControl.control.playerScore > GameControl.control.alexScore)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Robert        5 points\n\n2. Alex            3 points\n\n3. Emily          2 points\n\n4. William       1 point";
        }
        else if (GameControl.control.playerScore > GameControl.control.emilyScore)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Alex            5 points\n\n2. Robert        3 points\n\n3. Emily          2 points\n\n4. William       1 point";
        }
        else if (GameControl.control.playerScore > GameControl.control.williamScore)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Alex            5 points\n\n2. Emily          3 points\n\n3. Robert        2 points\n\n4. William       1 point";
        }
        else
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Alex            5 points\n\n2. Emily          3 points\n\n3. William       2 points\n\n4. Robert        1 point";
        }
    }

    public void printRaceResults2()
    {
        results.SetActive(true);

        if (GameControl.control.playerScore > GameControl.control.alexScore)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Robert        5 points\n\n2. Emily          3 points\n\n3. William       2 points\n\n4. Alex            1 point";
        }
        else if (GameControl.control.playerScore > GameControl.control.emilyScore)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Emily          5 points\n\n2. Robert        3 points\n\n3. William       2 points\n\n4. Alex            1 point";
        }
        else if (GameControl.control.playerScore > GameControl.control.williamScore)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Emily          5 points\n\n2. William       3 points\n\n3. Robert        2 points\n\n4. Alex            1 point";
        }
        else
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Emily          5 points\n\n2. William       3 points\n\n3. Alex            2 points\n\n4. Robert        1 point";
        }
    }

    public void HideResults()
    {
        results.SetActive(false);
    }

    public void printChampionshipResults()
    {
        results.SetActive(true);

        if (GameControl.control.playerGlobal >= GameControl.control.emilyGlobal)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Robert        " + GameControl.control.playerGlobal + " points\n\n2. Emily          " + GameControl.control.emilyGlobal + " points\n\n3. Alex            " + GameControl.control.alexGlobal + " points\n\n4. William       " + GameControl.control.williamGlobal + " points";
        }
        else if (GameControl.control.playerGlobal >= GameControl.control.alexGlobal)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Emily          " + GameControl.control.emilyGlobal + " points\n\n2. Robert        " + GameControl.control.playerGlobal + " points\n\n3. Alex            " + GameControl.control.alexGlobal + " points\n\n4. William       " + GameControl.control.williamGlobal + " points";
        }
        else if (GameControl.control.playerGlobal >= GameControl.control.williamGlobal)
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Emily          " + GameControl.control.emilyGlobal + " points\n\n2. Alex            " + GameControl.control.alexGlobal + " points\n\n3. Robert        " + GameControl.control.playerGlobal + " points\n\n4. William       " + GameControl.control.williamGlobal + " points";
        }
        else
        {
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 30;
            results.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1. Emily          " + GameControl.control.emilyGlobal + " points\n\n2. Alex            " + GameControl.control.alexGlobal + " points\n\n3. William       " + GameControl.control.williamGlobal + " points\n\n4. Robert        " + GameControl.control.playerGlobal + " points";
        }
    }
}
