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

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int playerHealth = 75;
    private int playerMaxHealth = 75;

    private Slider playerHealthBar;
    private TextMeshProUGUI healthText;

    /*public static int currentScore;
    public static TextMeshProUGUI scoreText;*/

    private void Awake()
    {
        //scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();
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
    }

    public void MoveUpScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    /*public void IncreaseScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = currentScore.ToString();
    }*/

    public void PlayerDamage(int damage)
    {
        playerHealth -= damage;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthText.SetText(playerHealth.ToString() + "/" + playerMaxHealth.ToString());
        playerHealthBar.value = playerHealth;
        playerHealthBar.maxValue = playerMaxHealth;
    }
}
