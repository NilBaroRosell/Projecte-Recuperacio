using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainController : MonoBehaviour
{

    public GameObject FirstTerrain1, FirstTerrain2, SecondTerrain1, SecondTerrain2, ThirdTerrain1, ThirdTerrain2;
    private GameObject StartPoint, FinishPoint, ExtraTerrain, ExtraTerrain2, ExtraTerrain3;
    public GameObject Last;
    public bool first1Activated, first2Activated, second1Activated, second2Activated, third1Activated, third2Activated;
    public bool change;
    public int next;
    public int first;
    public int[] order;
    public int randomNum;
    public int toHide, toShow;
    public int terrainAcount;
    public float firstZ;

    private void Awake()
    {
        if (GameObject.Find("StartPoint") != null) StartPoint = GameObject.Find("StartPoint");
        if (GameObject.Find("FinishPoint") != null) FinishPoint = GameObject.Find("FinishPoint");
        if (GameObject.Find("ExtraTerrain") != null) ExtraTerrain = GameObject.Find("ExtraTerrain");
        if (GameObject.Find("ExtraTerrain2") != null) ExtraTerrain2 = GameObject.Find("ExtraTerrain2");
        if (GameObject.Find("ExtraTerrain3") != null) ExtraTerrain3 = GameObject.Find("ExtraTerrain3");

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            for (int i = 3; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            toHide = 0;
            toShow = 3;
        }
        else
        {
            first1Activated = true;
            first2Activated = false;
            second1Activated = true;
            second2Activated = false;
            third1Activated = true;
            third2Activated = false;
            FirstTerrain2.SetActive(false);
            ExtraTerrain3.SetActive(false);
            SecondTerrain2.SetActive(false);
            ThirdTerrain2.SetActive(false);
            FinishPoint.SetActive(false);
            ExtraTerrain2.SetActive(false);
            change = false;
            order = new int[3];
            order[0] = 3;
            order[1] = 1;
            order[2] = 5;
            first = 3;
            Last = ThirdTerrain1;
            firstZ = ExtraTerrain.transform.position.z;
        }

        terrainAcount = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Random.InitState(42);
        if (change)
        {
            if (SceneManager.GetActiveScene().name == "Level1") AddNew2();
            else
            {
                switch (terrainAcount)
                {
                    case 1:
                        ExtraTerrain.SetActive(false);
                        break;
                    case 2:
                        StartPoint.SetActive(false);
                        break;
                    case 3:
                        AddNew();
                        break;
                    case 4:
                        AddNew();
                        break;
                    case 5:
                        AddNew();
                        break;
                    case 6:
                        FinishPoint.SetActive(true);
                        FinishPoint.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                        Last = FinishPoint;
                        break;
                    case 7:
                        ExtraTerrain2.SetActive(true);
                        ExtraTerrain2.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                        Last = ExtraTerrain2;
                        break;
                    case 8:
                        ExtraTerrain3.SetActive(true);
                        ExtraTerrain3.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                        Last = ExtraTerrain3;
                        break;
                    default:
                        break;
                }
                terrainAcount++;
            }
            change = false;
        }
    }

    public void AddNew()
    {
        next = RandomNext();

        switch (next)
        {
            case 1:
                FirstTerrain1.SetActive(true);
                showSons(FirstTerrain1);
                FirstTerrain1.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                Last = FirstTerrain1;
                first1Activated = true;
                break;
            case 2:
                FirstTerrain2.SetActive(true);
                showSons(FirstTerrain2);
                FirstTerrain2.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                Last = FirstTerrain2;
                first2Activated = true;
                break;
            case 3:
                SecondTerrain1.SetActive(true);
                showSons(SecondTerrain1);
                SecondTerrain1.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                Last = SecondTerrain1;
                second1Activated = true;
                break;
            case 4:
                SecondTerrain2.SetActive(true);
                showSons(SecondTerrain2);
                SecondTerrain2.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                Last = SecondTerrain2;
                second2Activated = true;
                break;
            case 5:
                ThirdTerrain1.SetActive(true);
                showSons(ThirdTerrain1);
                ThirdTerrain1.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                Last = ThirdTerrain1;
                third1Activated = true;
                break;
            case 6:
                ThirdTerrain2.SetActive(true);
                showSons(ThirdTerrain2);
                ThirdTerrain2.transform.position = Last.transform.position + new Vector3(0.0f, 0.0f, 999.11f);
                Last = ThirdTerrain2;
                third2Activated = true;
                break;
            default:
                break;
        }
        order[0] = order[1];
        order[1] = order[2];
        order[2] = next;
    }

    public void AddNew2()
    {
        gameObject.transform.GetChild(toHide).gameObject.SetActive(false);
        gameObject.transform.GetChild(toShow).gameObject.SetActive(true);

        toHide++;
        toShow++;
    }

    public int RandomNext()
    {
        int finalNext = (int)Random.Range(1, 7);
        randomNum = finalNext;
        if (finalNext == order[0] || finalNext == order[1] || finalNext == order[2]) finalNext = RandomNext();
        return finalNext;
    }

    public void showSons(GameObject father)
    {
        for (int i = 0; i < father.transform.childCount; i++)
        {
            for (int j = 0; j < father.transform.GetChild(i).gameObject.transform.childCount; j++)
            {
                father.transform.GetChild(i).gameObject.transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }

    public void showSecondTime()
    {
        FirstTerrain1.SetActive(false);
        FirstTerrain2.SetActive(false);
        SecondTerrain1.SetActive(false);
        SecondTerrain2.SetActive(false);
        ThirdTerrain1.SetActive(false);
        ThirdTerrain2.SetActive(false);

        ExtraTerrain.SetActive(true);
        ExtraTerrain.transform.position = new Vector3(ExtraTerrain.transform.position.x, ExtraTerrain.transform.position.y, firstZ);
        Last = ExtraTerrain;
        StartPoint.SetActive(true);
        showSons(StartPoint);
        StartPoint.transform.position = new Vector3(StartPoint.transform.position.x, StartPoint.transform.position.y, Last.transform.position.z + 999.11f);
        Last = StartPoint;
        FinishPoint.SetActive(true);
        showSons(FinishPoint);
        FinishPoint.transform.position = new Vector3(FinishPoint.transform.position.x, FinishPoint.transform.position.y, Last.transform.position.z + 999.11f);
        Last = FinishPoint;
    }

    public void skipRace()
    {
        if (GameObject.Find("AlexPlane") != null) GameObject.Find("AlexPlane").transform.position = new Vector3(GameObject.Find("AlexPlane").transform.position.x, GameObject.Find("AlexPlane").transform.position.y, Last.transform.position.z + 720);
        else if (GameObject.Find("EmilyPlane") != null) GameObject.Find("EmilyPlane").transform.position = new Vector3(GameObject.Find("EmilyPlane").transform.position.x, GameObject.Find("EmilyPlane").transform.position.y, Last.transform.position.z + 720);
        else if (GameObject.Find("WilliamPlane") != null) GameObject.Find("WilliamPlane").transform.position = new Vector3(GameObject.Find("WilliamPlane").transform.position.x, GameObject.Find("WilliamPlane").transform.position.y, Last.transform.position.z + 720);

        ExtraTerrain2.SetActive(true);
        ExtraTerrain2.transform.position = new Vector3(ExtraTerrain2.transform.position.x, ExtraTerrain2.transform.position.y, Last.transform.position.z + 999.11f);
        Last = ExtraTerrain2;
        ExtraTerrain3.SetActive(true);
        ExtraTerrain3.transform.position = new Vector3(ExtraTerrain3.transform.position.x, ExtraTerrain3.transform.position.y, Last.transform.position.z + 999.11f);
        Last = FinishPoint;
    }
}
