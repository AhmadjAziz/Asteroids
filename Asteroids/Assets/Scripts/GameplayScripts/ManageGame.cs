using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    [SerializeField] private int numAsteroids; // total number of asteroids in the scene.
    [SerializeField] private int levelNumber;
    [SerializeField] GameObject asteroid;
    [SerializeField] EnemySpaceships enemy;
    [SerializeField] PowerupScript powerupScript;

    public void UpdateNumAsteroids(int change)
    {
        numAsteroids += change;

        //Check for no asteroids.
        if(numAsteroids <= 0)
        {
            //Start new Level.
            Invoke("StartNewLevel", 3f);
        }
    }
    
    void StartNewLevel()
    {
        powerupScript.powerupsCalled = 0;
        levelNumber++;
        numAsteroids = levelNumber * 2;
        enemy.NewLevel();

        //Spawn New Asteroids.
        for (int i = 0; i < numAsteroids; i++)
        {
            Vector2 spawnPos = new Vector2(Random.RandomRange(-38, 38), 22f);
            Instantiate(asteroid, spawnPos, Quaternion.identity);
        }
    }

    //Invokes when play again is clicked in death menu.
    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScreen");
    }
    //Invokes when Main Menu is clicked in death menu.
    public void ReturnStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public bool CheckForHighScore(int score)
    {
        int currentHighScore = PlayerPrefs.GetInt("highscore");
        if (score > currentHighScore)
        {
            return true;
        }
        return false;
    }
}
