/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: End Zone functions and behaviours
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

/// <summary>
/// Represents an end zone in the game that triggers a win condition when enough rocket parts are collected.
/// </summary>
public class EndArea : Collectible
{
    /// <summary>
    /// Handles the interaction with the end zone.
    /// </summary>
    /// <param name="gameManager">The GameManager instance managing the game.</param>
    /// <param name="interactText">The UI text to display interaction prompts.</param>
    /// <param name="_input">The input handler for player controls.</param>
    public override void Interact(GameManager gameManager, TextMeshProUGUI interactText, StarterAssetsInputs _input)
    {
        base.Interact(gameManager, interactText, _input);

        // Check if enough rocket parts are obtained to repair and take off
        if (gameManager.partsObtained >= 3)
        {
            interactText.text = "Repair and Take Off";

            // Check if interact key is pressed
            if (_input.interact)
            {
                _input.interact = false; // Reset interact input
                gameManager.Win(); // Trigger win condition
            }
        }
        else
        {
            interactText.text = "Need Rocket Parts to Repair";
        }
    }
}
