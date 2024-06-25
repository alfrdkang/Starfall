/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 24/06/2024
 * Date Modified: 25/06/2024
 * Description: Script handling playing and animations for first starting cutscene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public GameObject canvas;
    public GameObject glitch1;
    public GameObject glitch2;
    public GameObject glitch3;
    public GameObject glitch4;

    public GameManager gameManager;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Play()
    {
        StartCoroutine(PlayCutscene());
    }

    public IEnumerator PlayCutscene()
    {
        Debug.Log("Started PlayCutscene Coroutine at timestamp : " + Time.time);
        canvas.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        glitch1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        animator.Play("Shake");
        glitch2.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        glitch3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        glitch4.SetActive(true);
        yield return new WaitForSeconds(5f);
        gameManager.MoveUpScene();
    }
}
