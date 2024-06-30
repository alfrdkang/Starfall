/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: Player Interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;
using UnityEngine.Windows;

public class Interactor : MonoBehaviour
{
    /// <summary>
    /// Raycast Interact Range
    /// </summary>
    public float InteractRange;

    /// <summary>
    /// Interaction UI Prompt
    /// </summary>
    private TextMeshProUGUI interactText;
    private StarterAssetsInputs input;
    private GameManager gameManager;

    private void Awake()
    {
        interactText = GameObject.Find("interactText").GetComponent<TextMeshProUGUI>();
        input = GameObject.Find("PlayerCapsule").GetComponent<StarterAssetsInputs>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        interactText.enabled = false;
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Collectible collectObj))
            {
                collectObj.Interact(gameManager, interactText, input);
            } else
            {
                interactText.enabled = false;
            }
        }
        else
        {
            interactText.enabled = false;
        }
    }
}