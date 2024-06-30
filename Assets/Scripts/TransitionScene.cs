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
    public int transitionSceneIndex;
    public override void Interact(GameManager gameManager, TextMeshProUGUI interactText, StarterAssetsInputs _input)
    {
        base.Interact(gameManager, interactText, _input);
        if (gameObject.name == "ExitArea")
        {
            interactText.text = "Press [E] to Exit";
        }
        else
        {
            interactText.text = "Press [E] to Enter";
        }
        if (_input.interact)
        {
            _input.interact = false;
            gameManager.newScene = true;
            gameManager.MoveToScene(transitionSceneIndex);
        }
    }
}
