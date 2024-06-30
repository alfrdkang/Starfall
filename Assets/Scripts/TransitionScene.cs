/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: Scene Transition Areas' functions and behaviours
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class TransitionScene : Collectible
{
    /// <summary>
    /// Index of the scene to transition to.
    /// </summary>
    public int transitionSceneIndex;

    /// <summary>
    /// Handles interaction with the transition area.
    /// </summary>
    /// <param name="gameManager">The GameManager instance managing game state.</param>
    /// <param name="interactText">Text displayed to prompt interaction.</param>
    /// <param name="_input">Input handler for player controls.</param>
    public override void Interact(GameManager gameManager, TextMeshProUGUI interactText, StarterAssetsInputs _input)
    {
        base.Interact(gameManager, interactText, _input);

        // Display appropriate interaction text
        if (gameObject.name == "ExitArea")
        {
            interactText.text = "Press [E] to Exit";
        }
        else
        {
            interactText.text = "Press [E] to Enter";
        }

        // Check for interaction input
        if (_input.interact)
        {
            _input.interact = false; // Reset interaction flag

            // Trigger scene transition
            gameManager.newScene = true;
            gameManager.MoveToScene(transitionSceneIndex);
        }
    }
}
