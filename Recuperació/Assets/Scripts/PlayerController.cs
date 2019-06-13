using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 1f;
    private float boostMultiplier = 2f;
    private float movementMultiplier = 0.5f;
    public float boostAmount, boostLost;

    public Camera mainCamera;
    public GameObject prop;
    public GameObject propBlured;

    private Vector3 moveDirection = Vector3.zero;

    private float moveHorizontal;
    private float moveVertical;

    public bool back, front, left, right;

    private int rotation;

    void Start()
    {
        boostAmount = 100;
        boostLost = 0.5f;
        back = false;
        front = false;
        left = false;
        right = false;
        rotation = 0;
    }

    void Update()
    {
        if(!GameControl.control.gamePaused) Move();
    }

    void Move()
    {
        if (back) Backflip();
        else if (front) Frontflip();
        else if (left) LeftSpin();
        else if (right) RightSpin();
        else if (Input.GetButtonDown("Backflip"))
        {
            back = true;
            Backflip();
        }
        else if (Input.GetButtonDown("Frontflip"))
        {
            front = true;
            Frontflip();
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

            if (gameObject.transform.position.y > 20.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 20.5f, gameObject.transform.position.z);
            if (gameObject.transform.position.y < 8.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 8.5f, gameObject.transform.position.z);
            if (gameObject.transform.position.x > 30) gameObject.transform.position = new Vector3(30.0f, gameObject.transform.position.y, gameObject.transform.position.z);
            if (gameObject.transform.position.x < -10) gameObject.transform.position = new Vector3(-10.0f, gameObject.transform.position.y, gameObject.transform.position.z);

            gameObject.transform.Rotate(0.0f, 2 * Input.GetAxis("Horizontal"), 0.0f);
        }

        prop.SetActive(false);
        propBlured.SetActive(true);
        propBlured.transform.Rotate(1000 * Time.deltaTime, 0, 0);

    }

    void Backflip()
    {
        moveDirection = new Vector3(0.0f, 5 * movementMultiplier, speed);
        gameObject.transform.position += moveDirection;
        if (rotation > -360)
        {
            gameObject.transform.Rotate(-10.0f, 0.0f, 0.0f);
            rotation -= 10;
        }
        else
        {
            rotation = 0;
            back = false;
        }
    }

    void Frontflip()
    {
        moveDirection = new Vector3(0.0f, -5 * movementMultiplier, speed);
        gameObject.transform.position += moveDirection;
        if (rotation < 360)
        {
            gameObject.transform.Rotate(10.0f, 0.0f, 0.0f);
            rotation += 10;
        }
        else
        {
            rotation = 0;
            front = false;
        }
    }

    void LeftSpin()
    {
        moveDirection = new Vector3(-2 * movementMultiplier, 0.0f, speed);
        gameObject.transform.position += moveDirection;
        if (rotation > -360)
        {
            gameObject.transform.Rotate(0.0f, -10.0f, 0.0f);
            rotation -= 10;
        }
        else
        {
            rotation = 0;
            left = false;
        }
    }

    void RightSpin()
    {
        moveDirection = new Vector3(2 * movementMultiplier, 0.0f, speed);
        gameObject.transform.position += moveDirection;
        if (rotation < 360)
        {
            gameObject.transform.Rotate(0.0f, +10.0f, 0.0f);
            rotation += 10;
        }
        else
        {
            rotation = 0;
            right = false;
        }
    }
}
