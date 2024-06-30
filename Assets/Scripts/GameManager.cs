/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 24/06/2024
 * Date Modified: 25/06/2024
 * Description: GameManager Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerHealth = 75;
    private int playerMaxHealth = 75;
    public bool newScene = false;

    [SerializeField] private GameObject player;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private StarterAssetsInputs _inputs;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject bgBlur;
    [SerializeField] private PauseMenu pauseMenu;
    public Vector3[] sceneIndexStartPos = new Vector3[] { 
        Vector3.zero,                                           //main menu
        new Vector3(-25.1399994f,0,-78.4000015f),               //spaceship
        new Vector3(-24.2000008f, 0, -74.5f),                   //world
        new Vector3(8.89999962f, 43.9000015f, -200.199997f) };  //cave

    private void FixedUpdate()
    {
        if (newScene == true)
        {
            UpdateHealthUI();
            player.transform.position = sceneIndexStartPos[SceneManager.GetActiveScene().buildIndex];
            player.transform.rotation = Quaternion.Euler(Vector3.zero);
            newScene = false;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        { 
            Destroy(gameObject);
        }

        /* = GameObject.Find("playerHealthBar").GetComponent<Slider>();
        healthText = GameObject.Find("healthText").GetComponent<TextMeshProUGUI>();
        _inputs = GameObject.Find("PlayerCapsule").GetComponent<StarterAssetsInputs>();
        deathMenu = GameObject.Find("Death");
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        HUD = GameObject.Find("HUD");
        bgBlur = GameObject.Find("bgBlur");
        deathMenu.SetActive(false);
        bgBlur.SetActive(false);*/

        UpdateHealthUI();
    }

    public void MoveUpScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MoveToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        newScene = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayerDamage(int damage)
    {
        playerHealth -= damage;
        UpdateHealthUI();

        if (playerHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pauseMenu.IsPaused = true;
        deathMenu.SetActive(true);
        HUD.SetActive(false);
        bgBlur.SetActive(true);
    }

    public void PlayerHeal(int health)
    {
        playerHealth += health;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthText.SetText(playerHealth.ToString() + "/" + playerMaxHealth.ToString());
        playerHealthBar.value = playerHealth;
        playerHealthBar.maxValue = playerMaxHealth;
    }
}
