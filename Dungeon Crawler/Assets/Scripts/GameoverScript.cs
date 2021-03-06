﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverPanelText;
    public Button gameOverButton;
    public Button endlessModeButton;
    public GameObject dungeonGenerator;

    //reason for loss if death or victory
    public void GameOver(bool death)
    {
        if (!death)
        {
            //yellow
            gameOverPanel.GetComponent<Image>().color = new Color(1,0.92f,0.016f,0.4f);
            gameOverPanelText.color = new Color(1, 0.92f, 0.016f, 1);
            gameOverPanelText.text = "YOU WIN, CONGRATULATIONS";
            gameOverButton.GetComponent<Image>().color = new Color(1, 0.92f, 0.016f, 1);
            endlessModeButton.gameObject.SetActive(true);
        }
        else
        {
            //red
            gameOverPanel.GetComponent<Image>().color = new Color(0.2242566f, 0.01966893f, 0.3207547f, 0.4f);
            gameOverPanelText.color = new Color(0.6024374f, 0.01054644f, 0.745283f, 1f);
            gameOverPanelText.text = "YOU HAVE DIED";
            gameOverButton.GetComponent<Image>().color = new Color(0.6024374f, 0.01054644f, 0.745283f, 1f);
            endlessModeButton.gameObject.SetActive(false);
        }
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main menu", LoadSceneMode.Single);
    }

    public void EnterEndlessMode()
    {
        if (dungeonGenerator.GetComponent<DungeonRoomGenerator>() != null)
        {
            dungeonGenerator.GetComponent<DungeonRoomGenerator>().level--;
            dungeonGenerator.GetComponent<DungeonRoomGenerator>().endless = true;
            dungeonGenerator.GetComponent<DungeonRoomGenerator>().GenerateNextRoom();
            gameOverPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
