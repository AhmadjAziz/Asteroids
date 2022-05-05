using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{
    [SerializeField] private Text highscoreText;
    [SerializeField] private InputField playerNameInput;
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject mainMenuPanel;
    public void ShowHighScorer()
    {
        if (PlayerPrefs.GetInt("Highscore") == 0)
        {
            highscoreText.text = "HIGHEST SCORE" + "\n\n";
        }
        else
        {
            highscoreText.text = "HIGHEST SCORE" + "\n\n" + PlayerPrefs.GetString("HighscorePlayer") + ": " + PlayerPrefs.GetInt("Highscore");
        }
    }

    public void EnterPlayerName()
    {
        PlayerPrefs.SetString("CurrentPlayer", playerNameInput.text);
        profilePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        
    }
}
