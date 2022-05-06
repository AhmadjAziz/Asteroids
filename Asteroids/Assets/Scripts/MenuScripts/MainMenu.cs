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
    [SerializeField] private Animator canvasAnim;
    [SerializeField] private Animator optionsAnim;
    
    private GameObject currentMenu;

    public void StartGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void MenuToOptions()
    {
        
        optionsMenu.SetActive(true);
        menuButtons.SetActive(false);
        
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

    public void ProfileToOptions()
    {
        optionsMenu.SetActive(true);
        profilesPanel.SetActive(false);
    }

    public void HighscoreToOptions()
    {
        optionsMenu.SetActive(true);
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
