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
    

    public void EnterPlayerName()
    {
        PlayerPrefs.SetString("CurrentPlayer", playerNameInput.text);
        profilePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        
    }
}
