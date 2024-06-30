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
    public virtual void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        interacttext.enabled = true;
    }
}
