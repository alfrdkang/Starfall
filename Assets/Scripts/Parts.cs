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

public class Parts : Collectible
{
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.Find("PlayerCapsule");
    }
    public override void Interact(GameManager gameManager, TextMeshProUGUI interactText, StarterAssetsInputs _input)
    {
        base.Interact(gameManager, interactText, _input);
        interactText.text = "Press [E] to Pickup " + gameObject.name;

        if (_input.interact) { 
            _input.interact = false;
            gameManager.partsObtained += 1;
            if (gameManager.partsObtained >= 3)
            {
                gameManager.sceneIndexStartPos[SceneManager.GetActiveScene().buildIndex] = new Vector3(-23.0100002f, -0.0799999982f, -114.620003f);
                gameManager.MoveToScene(SceneManager.GetActiveScene().buildIndex);
            }
            Destroy(gameObject);
        }
    }
}
