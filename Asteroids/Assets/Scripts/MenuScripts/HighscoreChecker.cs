using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreChecker : MonoBehaviour
{

    [SerializeField] private Text highscore;
    // Start is called before the first frame update
    void Awake()
    {
        highscore.text = "HIGH SCORES" + "\n\n" + PlayerPrefs.GetString("HighscorePlayer") + ": " + PlayerPrefs.GetInt("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
