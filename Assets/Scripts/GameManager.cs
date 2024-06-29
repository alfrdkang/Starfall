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

    private int playerHealth = 75;
    private int playerMaxHealth = 75;
    private bool newScene = false;

    [SerializeField] private GameObject player;
    private Slider playerHealthBar;
    private TextMeshProUGUI healthText;
    private StarterAssetsInputs _inputs;
    private GameObject deathMenu;
    private GameObject HUD;
    private GameObject bgBlur;
    private PauseMenu pauseMenu;
    private Vector3[] sceneIndexStartPos = new Vector3[] { Vector3.zero, Vector3.zero, new Vector3(8.89999962f, 43.9000015f, -200.199997f) };

    private void FixedUpdate()
    {
        if (newScene == true)
        {
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

        playerHealthBar = GameObject.Find("playerHealthBar").GetComponent<Slider>();
        healthText = GameObject.Find("healthText").GetComponent<TextMeshProUGUI>();
        /*_inputs = GameObject.Find("PlayerCapsule").GetComponent<StarterAssetsInputs>();
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
        /*Time.timeScale = 0f;
        pauseMenu.IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        deathMenu.SetActive(true);
        HUD.SetActive(false);
        bgBlur.SetActive(true);*/
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
