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

public class Interactor : MonoBehaviour
{
    /// <summary>
    /// Raycast Interact Range
    /// </summary>
    public float InteractRange;

    /// <summary>
    /// Interact UI Text when Player Raycast Hits Interactable
    /// </summary>
    public TextMeshProUGUI interactText;

    private void Start()
    {
        interactText = GameObject.Find("interactText").GetComponent<TextMeshProUGUI>();
        interactText.enabled = false;
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Collectible collectObj))
            {
                collectObj.Interact();
            } else
            {
                interactText.enabled = false;
            }

            /*else if (hitInfo.collider.gameObject.TryGetComponent(out DoorScript door) && !FindObjectOfType<DialogueManager>().diagActive)
            {
                if (keyObtained)
                {
                    if (hitInfo.collider.gameObject.GetComponent<DoorScript>().doorOpened)
                    {
                        interactText.text = "Press [E] to Close Door";
                        interactText.enabled = true;
                    }
                    else
                    {
                        interactText.text = "Press [E] to Open Door";
                        interactText.enabled = true;
                    }
                }
                else
                {
                    interactText.text = "LOCKED";
                    interactText.enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.E) && keyObtained == true)
                {
                    door.Interact();
                }
                else if (Input.GetKeyDown(KeyCode.E) && keyObtained == false)
                {
                    Debug.Log("This door is locked.");
                }
            }*/
        }
        else
        {
            interactText.enabled = false;
        }
    }
}