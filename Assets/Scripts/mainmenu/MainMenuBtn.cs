/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 24/06/2024
 * Date Modified: 25/06/2024
 * Description: Script handling button events and functions on main menu.
 */

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
    }
}
