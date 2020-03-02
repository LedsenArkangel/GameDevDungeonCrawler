using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("The game quits!");
        Application.Quit();
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
