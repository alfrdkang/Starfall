using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _inputs;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject pauseVolume;

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
        HUD.SetActive(true);
        pauseVolume.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        _inputs.pause = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
        pauseVolume.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        _inputs.pause = false;
    }
}
