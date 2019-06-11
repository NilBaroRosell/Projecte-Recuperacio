using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 1f;
    private float boostMultiplier = 2f;
    private float movementMultiplier = 0.5f;

    public Camera mainCamera;
    public GameObject planeReference;
    public GameObject prop;
    public GameObject propBlured;

    private Vector3 moveDirection = Vector3.zero;

    public float moveHorizontal;
    public float moveVertical;

    void Start()
    {
    }

    void Update()
    {
        if(!GameControl.control.gamePaused) Move();
    }

    void Move()
    {
        //si es mou el la distància de la càmera és 0, 8, -25
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * movementMultiplier, Input.GetAxis("Vertical") * movementMultiplier, speed);
        // si es mou el món: 
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * movementMultiplier, Input.GetAxis("Vertical") * movementMultiplier, 0);

        //borrar si es mou el món
        if (Input.GetButton("Space")) moveDirection = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z * boostMultiplier);

        gameObject.transform.position += moveDirection;
        // si es mou el món: 
        //if (Input.GetButtonDown("Space")) gameObject.transform.position += new Vector3(0.0f, 0.0f, 10);
        //if (Input.GetButtonUp("Space")) gameObject.transform.position -= new Vector3(0.0f, 0.0f, 10);

        if (gameObject.transform.position.y > 14.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 14.5f, gameObject.transform.position.z);
        if (gameObject.transform.position.y < 4.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4.5f, gameObject.transform.position.z);
        if (gameObject.transform.position.x > 30) gameObject.transform.position = new Vector3(30.0f, gameObject.transform.position.y, gameObject.transform.position.z);
        if (gameObject.transform.position.x < -10) gameObject.transform.position = new Vector3(-10.0f, gameObject.transform.position.y, gameObject.transform.position.z);

        prop.SetActive(false);
        propBlured.SetActive(true);
        propBlured.transform.Rotate(1000 * Time.deltaTime, 0, 0);
    }
}
