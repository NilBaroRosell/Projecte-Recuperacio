using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed = 2f;
    private float boostMultiplier = 2f;
    private float movementMultiplier = 0.5f;
    private float trickMultiplier = 0.8f;
    public float boostAmount, boostLost;
    public int targetMultiplier;

    public Camera mainCamera;
    public GameObject prop;
    public GameObject propBlured;
    public GameObject loopAvailable;

    private Vector3 moveDirection = Vector3.zero;

    private float moveHorizontal;
    private float moveVertical;

    public bool back, left, right;

    private int rotation;
    public Vector3 actualRotation, initialRotation;

    public int avoidObstacle;
    private int spin, flip;

    public bool onPoint;
    private float decreaseAmount = 0.0055f;

    public void Start()
    {
        boostAmount = 100;
        if (GameObject.Find("BoostBar") != null) GameObject.Find("BoostBar").GetComponent<Image>().fillAmount = boostAmount;
        boostLost = 0.5f;
        back = false;
        left = false;
        right = false;
        rotation = 0;
        actualRotation = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        initialRotation = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        onPoint = true;
        if (GameObject.Find("LoopAvailable") != null) loopAvailable = GameObject.Find("LoopAvailable");
        targetMultiplier = 1;
        avoidObstacle = 5;
        spin = 20;
        flip = 30;
    }

    void Update()
    {
        checkTarget();
        actualRotation = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        if (Mathf.Abs(actualRotation.x) - Mathf.Abs(initialRotation.x) < 0.0001 && Mathf.Abs(actualRotation.x) - Mathf.Abs(initialRotation.x) > -0.0001) onPoint = true;
        else onPoint = false;
        if (!GameControl.control.gamePaused) Move();
        if(GameControl.control.boostActivated)
        {
            if (GameObject.Find("BoostBar") != null)
            {
                GameObject.Find("BoostBar").GetComponent<Image>().fillAmount = boostAmount / 100;
                if (GameObject.Find("BoostBar").GetComponent<Image>().fillAmount < 0.02f) GameObject.Find("BoostBar").GetComponent<Image>().fillAmount = 0.02f;
            }
        }
        if(GameControl.control.onPointActivated)
        {
            if (onPoint) loopAvailable.SetActive(true);
            else loopAvailable.SetActive(false);
        }
    }

    void Move()
    {
        if(GameControl.control.movementActivated)
        {
            if(GameControl.control.tricksActivated)
            {
                if (back) Backflip();
                else if (left) LeftSpin();
                else if (right) RightSpin();
                else if (Input.GetButtonDown("Backflip") && onPoint)
                {
                    back = true;
                    Backflip();
                }
                else if (Input.GetButtonDown("LeftSpin"))
                {
                    left = true;
                    LeftSpin();
                }
                else if (Input.GetButtonDown("RightSpin"))
                {
                    right = true;
                    RightSpin();
                }
                else
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal") * movementMultiplier, Input.GetAxis("Vertical") * movementMultiplier, speed);

                    if (Input.GetButton("Space") && boostAmount > 0)
                    {
                        moveDirection = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z * boostMultiplier);
                        boostAmount -= boostLost;
                    }

                    gameObject.transform.position += moveDirection;

                    if (gameObject.transform.position.y > 17.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 17.5f, gameObject.transform.position.z);
                    if (gameObject.transform.position.y < 9) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 9f, gameObject.transform.position.z);
                    if (gameObject.transform.position.x > 30) gameObject.transform.position = new Vector3(30.0f, gameObject.transform.position.y, gameObject.transform.position.z);
                    if (gameObject.transform.position.x < -10) gameObject.transform.position = new Vector3(-10.0f, gameObject.transform.position.y, gameObject.transform.position.z);

                    gameObject.transform.Rotate(0.0f, 2 * Input.GetAxis("Horizontal"), 0.0f);
                }
            }
            else
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal") * movementMultiplier, Input.GetAxis("Vertical") * movementMultiplier, speed);

                if (GameControl.control.boostActivated)
                {
                    if (Input.GetButton("Space") && boostAmount > 0)
                    {
                        moveDirection = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z * boostMultiplier);
                        boostAmount -= boostLost;
                    }
                }

                gameObject.transform.position += moveDirection;

                if (gameObject.transform.position.y > 17.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 17.5f, gameObject.transform.position.z);
                if (gameObject.transform.position.y < 9) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 9f, gameObject.transform.position.z);
                if (gameObject.transform.position.x > 30) gameObject.transform.position = new Vector3(30.0f, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x < -10) gameObject.transform.position = new Vector3(-10.0f, gameObject.transform.position.y, gameObject.transform.position.z);

                gameObject.transform.Rotate(0.0f, 2 * Input.GetAxis("Horizontal"), 0.0f);
            }
        }
        else
        {
            moveDirection = new Vector3(0.0f, 0.0f, speed);
            gameObject.transform.position += moveDirection;
        }
        
        prop.SetActive(false);
        propBlured.SetActive(true);
        propBlured.transform.Rotate(1000 * Time.deltaTime, 0, 0);
    }

    void Backflip()
    {
        moveDirection = new Vector3(0.0f, 5 * movementMultiplier, speed * trickMultiplier);
        gameObject.transform.position += moveDirection;
        if (rotation > -360)
        {
            gameObject.transform.Rotate(-10.0f, 0.0f, 0.0f);
            rotation -= 10;
        }
        else
        {
            GameControl.control.score += flip * targetMultiplier;
            rotation = 0;
            back = false;
        }
    }

    void LeftSpin()
    {
        moveDirection = new Vector3(-movementMultiplier, 0.0f, speed * trickMultiplier);
        gameObject.transform.position += moveDirection;
        if (rotation > -360)
        {
            gameObject.transform.Rotate(0.0f, -10.0f, 0.0f);
            rotation -= 10;
        }
        else
        {
            GameControl.control.score += spin * targetMultiplier;
            rotation = 0;
            left = false;
        }
    }

    void RightSpin()
    {
        moveDirection = new Vector3(movementMultiplier, 0.0f, speed * trickMultiplier);
        gameObject.transform.position += moveDirection;
        if (rotation < 360)
        {
            gameObject.transform.Rotate(0.0f, +10.0f, 0.0f);
            rotation += 10;
        }
        else
        {
            GameControl.control.score += spin * targetMultiplier;
            rotation = 0;
            right = false;
        }
    }


    public void checkTarget()
    {
        if (targetMultiplier != 1)
        {
            StartCoroutine(MultiplierTimeLeft(3.0f));
        }
    }

    public IEnumerator MultiplierTimeLeft(float time)
    {
        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().DecreaseTarget(decreaseAmount);
        yield return new WaitForSeconds(time);

        GameObject.Find("CanvasInGame").GetComponent<HUDControler>().HideTarget();
        targetMultiplier = 1;
    }
}
