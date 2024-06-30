/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 29/06/2024
 * Date Modified: 29/06/2024
 * Description: Pause Menu Functions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StarterAssetsInputs _inputs;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject bgBlur;

    public bool IsPaused = false;

    // Update is called once per frames
    private void Update()
    {
        if (_inputs.pause)
        {
            if (IsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        winMenu.SetActive(false);
        HUD.SetActive(true);
        bgBlur.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        _inputs.pause = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
        bgBlur.SetActive(true);
        IsPaused = true;
        _inputs.pause = false;
    }

    public void Restart()
    {
        Resume();
        Cursor.lockState = CursorLockMode.Locked;
        gameManager.newScene = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        gameManager.playerHealth = 75;
    }
}
