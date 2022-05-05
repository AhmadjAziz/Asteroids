using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;
    public void StartGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void HighScore()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
