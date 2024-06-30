/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: Script attached to game's collectibles
 */

using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.Windows;

public class Collectible : MonoBehaviour
{
    private TextMeshProUGUI interactText;
    private StarterAssetsInputs _input;
    [SerializeField] private int transitionSceneIndex = 0;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        interactText = GameObject.Find("interactText").GetComponent<TextMeshProUGUI>();
        _input = GameObject.Find("PlayerCapsule").GetComponent<StarterAssetsInputs>();
    }

    public void Interact()
    {
        interactText.enabled = true;
        if (!gameObject.CompareTag("TransitionArea"))
        {
            interactText.text = "Press [E] to Pickup " + gameObject.name;

            if (_input.interact)
            {
                _input.interact = false;
                Destroy(gameObject);
            }
        } else
        {
            if (gameObject.name == "ExitArea")
            {
                interactText.text = "Press [E] to Exit";
            } else
            {
                interactText.text = "Press [E] to Enter";
            }
            if (_input.interact)
            {
                _input.interact = false;
                gameManager.MoveToScene(transitionSceneIndex);
            }
        }
    }
}
