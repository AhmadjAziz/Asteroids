using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject highscoresPanel;
    [SerializeField] private GameObject profilesPanel;

    public void StartGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void MenuToOptions()
    {
        menuButtons.SetActive(false);
        optionsMenu.SetActive(true);

    }
    
    public void OptionsToHighscore()
    {
        optionsMenu.SetActive(false);
        highscoresPanel.SetActive(true);
    }

    public void OptionsToProfiles()
    {
        optionsMenu.SetActive(false);
        profilesPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsToMenu()
    {
        menuButtons.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Profile_HighScore_To_Options()
    {
        optionsMenu.SetActive(true);
        profilesPanel.SetActive(false);
        highscoresPanel.SetActive(false);
    }

    //transitions between scenes
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }


}
