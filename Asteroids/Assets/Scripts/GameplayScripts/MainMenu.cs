using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator transitioin;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScreen");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
