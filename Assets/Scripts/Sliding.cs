using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform playerObj;
    //private Rigidbody rb;
    private FirstPersonController firstPersonController;
    private StarterAssetsInputs _input;
    private CharacterController controller;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;
    private float startMoveSpeed;

    public bool sliding;
    Vector3 inputDirection;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        firstPersonController = GetComponent<FirstPersonController>();
        _input = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<CharacterController>();

        startYScale = playerObj.localScale.y;
        startMoveSpeed = firstPersonController.MoveSpeed;
    }

    private void FixedUpdate()
    {
        if (sliding)
        {
            SlideMovement();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.C) && sliding)
        {
            StopSlide();
        }
    }

    private void StartSlide()
    {
        sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
        controller.Move(Vector3.down);

        slideTimer = maxSlideTime;
    }

    private void SlideMovement()
    {
        controller.Move(inputDirection.normalized * slideForce);

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
        {
            StopSlide();
        }
    }

    private void StopSlide()
    {
        sliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}


