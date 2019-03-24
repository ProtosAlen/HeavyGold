using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject rules;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowRules()
    {
        rules.SetActive(true);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MainScene");
    }
}
