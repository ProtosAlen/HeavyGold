using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject settings;
    public GameObject credits;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
     
    public void ToggleTutorial()
    {
        if(tutorial.activeSelf)
        {
            tutorial.SetActive(false);
        }
        else
        {
            tutorial.SetActive(true);
        }
    }

    public void ToggleCredits()
    {
        if (credits.activeSelf)
        {
            credits.SetActive(false);
        }
        else
        {
            credits.SetActive(true);
        }
    }

    public void ToggleSettings()
    {
        if (settings.activeSelf)
        {
            settings.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
        }
    }
}
