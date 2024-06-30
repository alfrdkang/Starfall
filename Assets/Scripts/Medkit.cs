/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: Medkit functions and behaviours
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

/// <summary>
/// Represents a medkit that heals the player when interacted with.
/// </summary>
public class Medkit : Collectible
{
    /// <summary>
    /// The amount of health the medkit will restore to the player.
    /// </summary>
    public int healValue = 15;

    /// <summary>
    /// Handles the interaction with the medkit.
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
            gameManager.PlayerHeal(healValue); // Heal the player
            Destroy(gameObject); // Destroy the medkit object
        }
    }
}
