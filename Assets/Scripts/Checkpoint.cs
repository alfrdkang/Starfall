using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;

    /// <summary>
    /// Finds and assigns the GameManager component on awake.
    /// </summary>
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Called when another collider enters this trigger collider.
    /// Sets the player's respawn position for the current scene in GameManager.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the player's respawn position for the current scene
            gameManager.sceneIndexStartPos[SceneManager.GetActiveScene().buildIndex] = transform.position;
        }
    }
}
