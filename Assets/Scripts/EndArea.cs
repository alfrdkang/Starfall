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

public class EndArea : Collectible
{
    public override void Interact(GameManager gameManager, TextMeshProUGUI interactText, StarterAssetsInputs _input)
    {
        base.Interact(gameManager, interactText, _input);
        if (gameManager.partsObtained >= 3)
        {
            interactText.text = "Repair and Take Off";
            if (_input.interact)
            {
                _input.interact = false;
                gameManager.Win();
            }
        }
        else
        {
            interactText.text = "Need Rocket Parts to Repair";
        }
    }
}
