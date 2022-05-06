using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This class uses Events to unlock achievements. 
//Achievenemts are unlocked at score 100, 200, 500, 1000, 2000, 5000.
public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private Text achivementText;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        SpaceShipController.scoreReached += CheckHighScore;
        SpaceShipController.asteroidMilstoneReached += CheckAsteroidsDestroyed;
    }

    private void OnDestroy()
    {
        SpaceShipController.scoreReached -= CheckHighScore;
        SpaceShipController.asteroidMilstoneReached -= CheckAsteroidsDestroyed;
    }


    private void ThresholdScoreReached(int score, string achievementQuote)
    {
        string achievementKey = score + " reached";
        if (PlayerPrefs.GetInt(achievementKey) == 1)
            return;

        PlayerPrefs.SetInt(achievementKey, 1);
        achivementText.text = achievementQuote + "\n" + score + " reached";
        SetAchievementPanel();
        Invoke("RemoveAchievementPanel", 2f);
    }

    private void ThresholdAsteroidsDestroyed(int score, string achievementQuote)
    {
        string achievementKey = score + " destroyed";
        if (PlayerPrefs.GetInt(achievementKey) == 1)
            return;

        PlayerPrefs.SetInt(achievementKey, 1);
        achivementText.text = achievementQuote + "\n" + score + " destroyed";
        SetAchievementPanel();
        Invoke("RemoveAchievementPanel", 2f);

    }

    private void SetAchievementPanel()
    {
        achievementPanel.SetActive(true);
    }

    private void RemoveAchievementPanel()
    {
        achievementPanel.SetActive(false);
    }

    //Checks if a player unlocked a highscore achievement.
    private void CheckHighScore(float score)
    {
        if (score >= 100)
            ThresholdScoreReached(100, "Rookie ");

        if (score >= 200)
            ThresholdScoreReached(200, "Beginner");

        if (score >= 500)
            ThresholdScoreReached(500, "Decent");

        if (score >= 1000)
            ThresholdScoreReached(1000, "Master");

        if (score >= 2000)
            ThresholdScoreReached(2000, "Artist");

        if (score >= 5000)
            ThresholdScoreReached(5000, "Star Trooper");
    }

    //Unlocks achievements if the following amount of asteroids are destroyed.
    private void CheckAsteroidsDestroyed(int asteroids)
    {
        //Checks if achievement milestone is reached.
        if (asteroids == 10)
            ThresholdAsteroidsDestroyed(10, "Noob Miner");

        if (asteroids == 50)
            ThresholdAsteroidsDestroyed(50, "Begining Miner");

        if (asteroids == 100)
            ThresholdAsteroidsDestroyed(100, "Decent Miner");

        if (asteroids == 200)
            ThresholdAsteroidsDestroyed(200, "Master Miner");

        if (asteroids == 400)
            ThresholdAsteroidsDestroyed(400, "Artist Miner");

        if (asteroids == 1000)
            ThresholdAsteroidsDestroyed(1000, "Elite Miner");

        if (asteroids == 2000)
            ThresholdAsteroidsDestroyed(2000, "King Pin Miner");
    }
}
