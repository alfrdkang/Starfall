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

public class Medkit : Collectible
{
    public int healValue = 15;
    public override void Interact(GameManager gameManager, TextMeshProUGUI interactText, StarterAssetsInputs _input)
    {
        base.Interact(gameManager, interactText, _input);
        interactText.text = "Press [E] to Pickup " + gameObject.name;

        if (_input.interact)
        {
            _input.interact = false;
            gameManager.PlayerHeal(healValue);
            Destroy(gameObject);
        }
    }
}
