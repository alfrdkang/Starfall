/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: Script attached to game's collectibles
 */

using UnityEngine;
using TMPro;
using StarterAssets;

public class Collectible : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private int transitionSceneIndex = 0;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interact()
    {
        interactText.enabled = true;
        if (!gameObject.CompareTag("TransitionArea"))
        {
            interactText.text = "Press [E] to Pickup " + gameObject.name;

            if (_input.interact)
            {
                Destroy(gameObject);
            }
        } else
        {
            if (gameObject.name == "ExitArea")
            {
                interactText.text = "Press [E] to Exit";

                /*if (_input.interact)
                {
                    Destroy(gameObject);
                }*/
            } else
            {
                interactText.text = "Press [E] to Enter";

                /*if (_input.interact)
                {
                    Destroy(gameObject);
                }*/
            }
            if (_input.interact)
            {
                gameManager.MoveToScene(transitionSceneIndex);
            }
        }
    }
}
