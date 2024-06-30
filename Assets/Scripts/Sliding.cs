/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 23/06/2024
 * Date Modified: 23/06/2024
 * Description: Player Movement: Slide Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

/// <summary>
/// Handles sliding behavior for the player character.
/// </summary>
public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform playerObj; // Reference to the player's object transform
    private StarterAssetsInputs _input; // Reference to the input handler
    private CharacterController controller; // Reference to the character controller

    [Header("Sliding")]
    public float maxSlideTime; // Maximum duration of sliding
    public float slideForce; // Force applied during sliding movement
    private float slideTimer; // Timer for sliding duration

    public float slideYScale; // Y scale of the player object during sliding
    private float startYScale; // Initial Y scale of the player object

    public bool sliding; // Flag indicating if the player is currently sliding
    Vector3 inputDirection; // Directional input for sliding movement

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>(); // Get StarterAssetsInputs component
        controller = GetComponent<CharacterController>(); // Get CharacterController component

        startYScale = playerObj.localScale.y; // Store initial Y scale of the player object
    }

    private void FixedUpdate()
    {
        if (sliding)
        {
            SlideMovement(); // Execute sliding movement if sliding flag is true
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartSlide(); // Start sliding when 'C' key is pressed
        }

        //if (Input.GetKeyUp(KeyCode.C) && sliding)
        //{
        //    StopSlide();
        //}
    }

    /// <summary>
    /// Initiates the sliding behavior.
    /// </summary>
    private void StartSlide()
    {
        sliding = true; // Set sliding flag to true

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z); // Adjust player object's Y scale for sliding
        inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y; // Calculate sliding direction based on player input
        controller.Move(Vector3.down); // Apply downward movement to align with ground level

        slideTimer = maxSlideTime; // Reset slide timer
    }

    /// <summary>
    /// Executes the sliding movement.
    /// </summary>
    private void SlideMovement()
    {
        controller.Move(inputDirection.normalized * slideForce); // Apply sliding force in the calculated direction

        slideTimer -= Time.deltaTime; // Decrease slide timer

        if (slideTimer <= 0)
        {
            StopSlide(); // Stop sliding if slide timer runs out
        }
    }

    /// <summary>
    /// Stops the sliding behavior and resets the player's scale.
    /// </summary>
    private void StopSlide()
    {
        sliding = false; // Set sliding flag to false

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z); // Reset player object's Y scale to initial value
    }
}

