using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverPanelText;
    public Button gameOverButton;

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
        }
        gameOverPanel.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main menu", LoadSceneMode.Single);
    }
}
