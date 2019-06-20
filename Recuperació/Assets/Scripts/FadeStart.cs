using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeStart : MonoBehaviour {

    public Canvas canvas;

    public bool fadeIn;

    // Use this for initialization
    void Start()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void FadeIn()
    {
        canvas.GetComponent<Canvas>().enabled = true;
        fadeIn = true;
        StartCoroutine(FadeCanvas(canvas.transform.GetChild(0).GetComponent<Image>(), canvas.transform.GetChild(0).GetComponent<Image>().color.a, 1));
    }

    public void FadeOut()
    {
        if (GameObject.Find("MenuController").GetComponent<MenuControl>().delete)
        {
            GameObject.Find("Panel (2)").SetActive(false);
        }
        else
        {
            GameObject.Find("Panel (1)").SetActive(false);

        }
        fadeIn = false;
        StartCoroutine(FadeCanvas(canvas.transform.GetChild(0).GetComponent<Image>(), canvas.transform.GetChild(0).GetComponent<Image>().color.a, 0));
    }

    public IEnumerator FadeCanvas(Image im, float start, float end, float lerpTime = 0.5f)
    {
        float timeStartLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - timeStartLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(canvas.transform.GetChild(0).GetComponent<Image>().color.r, canvas.transform.GetChild(0).GetComponent<Image>().color.g, canvas.transform.GetChild(0).GetComponent<Image>().color.b, currentValue);

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        if (fadeIn) FadeOut();
        else
        {
            canvas.GetComponent<Canvas>().enabled = false;
            if(GameObject.Find("MenuController").GetComponent<MenuControl>().delete)
            {
                GameObject.Find("MenuController").GetComponent<MenuControl>().delete = false;
                StartCoroutine(GameObject.Find("MenuController").GetComponent<MenuControl>().ExecuteAfterTime(2.0f));
            }
        }
    }
}
