/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 24/06/2024
 * Date Modified: 25/06/2024
 * Description: Script handling playing and animations for first starting cutscene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles playing and animations for the first starting cutscene.
/// </summary>
public class StartCutscene : MonoBehaviour
{
    /// <summary>
    /// The canvas GameObject to deactivate during the cutscene.
    /// </summary>
    public GameObject canvas;

    /// <summary>
    /// Glitch effect GameObjects to activate during specific moments of the cutscene.
    /// </summary>
    public GameObject glitch1;
    public GameObject glitch2;
    public GameObject glitch3;
    public GameObject glitch4;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Initiates the playing of the cutscene.
    /// </summary>
    public void Play()
    {
        StartCoroutine(PlayCutscene());
    }

    /// <summary>
    /// Coroutine to play the cutscene sequence.
    /// </summary>
    /// <returns>IEnumerator for the coroutine.</returns>
    public IEnumerator PlayCutscene()
    {
        Debug.Log("Started PlayCutscene Coroutine at timestamp : " + Time.time);

        canvas.SetActive(false); // Deactivate the canvas during the cutscene.
        yield return new WaitForSeconds(3.0f); // Wait for 3 seconds.

        glitch1.SetActive(true); // Activate glitch effect 1.
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds.

        animator.Play("Shake"); // Play the "Shake" animation.
        glitch2.SetActive(true); // Activate glitch effect 2.
        yield return new WaitForSeconds(1.0f); // Wait for 1 second.

        glitch3.SetActive(true); // Activate glitch effect 3.
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds.

        glitch4.SetActive(true); // Activate glitch effect 4.
        yield return new WaitForSeconds(5f); // Wait for 5 seconds.

        SceneManager.LoadScene(1); // Load the next scene (index 1).
    }
}
