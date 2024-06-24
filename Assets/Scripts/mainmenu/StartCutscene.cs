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
        glitch2.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        glitch3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        glitch4.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameManager.MoveUpScene();
    }
}
