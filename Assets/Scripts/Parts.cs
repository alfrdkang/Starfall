/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: Rocket Parts functions and behaviours
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents rocket parts that can be collected to progress the game.
/// </summary>
public class Parts : Collectible
{
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.Find("PlayerCapsule");
    }

    /// <summary>
    /// Handles the interaction with the rocket part.
    /// </summary>
    /// <param name="gameManager">The GameManager instance managing the game.</param>
    /// <param name="interactText">The UI text to display interaction prompts.</param>
    /// <param name="_input">The input handler for player controls.</param>
    public override void Interact(GameManager gameManager, TextMeshProUGUI interactText, StarterAssetsInputs _input)
    {
        base.Interact(gameManager, interactText, _input);
        interactText.text = "Press [E] to Pickup " + gameObject.name;

        // Check if interact key is pressed
        if (_input.interact)
        {
            _input.interact = false; // Reset interact input
            gameManager.partsObtained += 1; // Increase parts obtained count

            // Check if enough parts are obtained to trigger scene movement
            if (gameManager.partsObtained >= 3)
            {
                // Set the start position for the next scene and move to it
                gameManager.sceneIndexStartPos[SceneManager.GetActiveScene().buildIndex] = new Vector3(-23.0100002f, -0.0799999982f, -114.620003f);
                gameManager.MoveToScene(SceneManager.GetActiveScene().buildIndex);
            }

            Destroy(gameObject); // Destroy the rocket part object
        }
    }
}
