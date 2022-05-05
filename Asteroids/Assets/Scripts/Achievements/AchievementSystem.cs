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
        SpaceShipController.scoreReached += ThresholdScoreReached;
        SpaceShipController.asteroidMilstoneReached += ThresholdAsteroidsDestroyed;
    }

    private void OnDestroy()
    {
        SpaceShipController.scoreReached -= ThresholdScoreReached;
        SpaceShipController.asteroidMilstoneReached -= ThresholdAsteroidsDestroyed;
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
}
