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

    public void Interact()
    {
        interactText.enabled = true;
        interactText.text = "Press [E] to Pickup " + gameObject.name;

        if (_input.interact)
        {
            Destroy(gameObject);
        }
    }
}
