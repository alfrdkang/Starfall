using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class MainMenuBtn : MonoBehaviour
{
    public TMP_Text text;
    public StartCutscene cutscene;
    private GameObject description;

    private void Awake()
    {
        description = gameObject.transform.GetChild(1).gameObject;
    }

    public void HoverEnter()
    {
        text.fontStyle = FontStyles.Underline;
        description.SetActive(true);
    }

    public void HoverExit()
    {
        text.fontStyle = FontStyles.Normal;
        description.SetActive(false);
    }

    public void Play()
    {
        cutscene.Play();
    }

    public void Quit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
