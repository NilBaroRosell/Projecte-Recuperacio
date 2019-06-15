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
        for (int i = 0; i < gameObject.transform.childCount; i++)
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
            Messages.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = size;
            Messages.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textToShow;
        }
    }

    public void HideMessage()
    {
        Messages.SetActive(false);
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
        StartCoroutine(HideAfterTime(5.0f));
    }

    public void HideMovementControls()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        movementControl = false;
    }

    public void ShowBoostControl()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(HideAfterTime(5.0f));
    }

    public void HideBoostControl()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        boostControl = false;
    }

    public void ShowTricksControls()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        StartCoroutine(HideAfterTime(5.0f));
    }

    public void HideTricksControls()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        tricksControl = false;
    }

    public IEnumerator HideAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (movementControl) HideMovementControls();
        if (boostControl) HideBoostControl();
        if (tricksControl) HideTricksControls();
    }
}
