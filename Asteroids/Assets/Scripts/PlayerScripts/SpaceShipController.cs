using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class SpaceShipController : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int lives;
    [SerializeField] private BoxCollider2D horizontalCollider;
    [SerializeField] private BoxCollider2D verticalCollider;
    [SerializeField] private GameObject shipCollision;
    [SerializeField] private GameObject shipDestroy;
    [SerializeField] private Color inColor;
    [SerializeField] private Color normColor;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text highscoreListText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject newHighScorePanel;
    [SerializeField] private GameObject spaceship;
    [SerializeField] private ManageGame manageGame;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ScreenWrapper screenWrapper;

    //Achievement system using unity observer pattern (Events)
    public static event Action<float> scoreReached;
    public static event Action<int> asteroidMilstoneReached;

    private ManageGame mg;
    private int totalAsteroidsDestroyed;
    private SpriteRenderer sr;
    private int score;
    
    public bool canHit = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Destroy(GetComponent<Animator>(),1f);

    }
    // Start is called before the first frame update
    void Start()
    {
        //starts with no points.
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;

        totalAsteroidsDestroyed = 0;
        mg = GameObject.FindObjectOfType<ManageGame>();


    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.CheckMovement();
        screenWrapper.CheckBounds();
        FireBullet();

    }
    void FixedUpdate()
    {
        //Add some thrust every few frame to give that delayed feel of old retro games.
        playerMovement.AddThrust();
    }

    /**
     * Checks input from mouse to fire bullet.
     **/
    private void FireBullet()
    {
        if (Input.GetButtonDown("Fire"))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletSpeed);
            Destroy(newBullet, 2.0f);
        }
    }
    void CountAsteroidsDestroyed()
    {
        totalAsteroidsDestroyed++;
        asteroidMilstoneReached(totalAsteroidsDestroyed);
    }

    //Gain points on killing asteroids and enemy. Also calls Achievement system when threshold score is reached.
    void PointsScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        scoreReached(score);
    }

    /**
     * can be callled to make player invincible.
     **/
    void Invulnerable()
    {
        canHit = false;
        horizontalCollider.enabled = false;
        verticalCollider.enabled = false;
        sr.color = inColor;
    }
    /**
     * Turns off invinciblity.
     **/
    void Targetable()
    {
        canHit = true;
        horizontalCollider.enabled = true;
        verticalCollider.enabled = true;
        sr.color = normColor;
    }

    //spaceship colliding with asteroid. Player dies if lives end.
    void OnCollisionEnter2D(Collision2D collision)
    {
        LoseLife();
        // destroy the item spaceship collides with.
        Destroy(collision.gameObject);
        mg.UpdateNumAsteroids(-1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet") && canHit == true)
        {
            LoseLife();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("PlusLife"))
        {
            GainLife();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("MinusLife"))
        {
            LoseLife();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Invincible"))
        {
            Invulnerable();
            Invoke("Targetable", 5f);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Size"))
        {
            IncreaseSize();
            Invoke("normalSize", 5f);
            Destroy(other.gameObject);
        }

    }

    void IncreaseSize()
    {
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
    void normalSize()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    void LoseLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            Death();
            return;
        }

        //causes a tiny explosion when spaceship collides with asteroid.
        GameObject newExplosion = Instantiate(shipCollision, transform.position, transform.rotation);
        Destroy(newExplosion, 2f);

        //Makes player invincible for 2 seconds after taking damage to not get hit multiple times at same time.
        Invulnerable();
        Invoke("Targetable", 3f);
    }

    void GainLife()
    {
        lives++;
        livesText.text = "Lives: " + lives;
    }

    void Death()
    {
        GameObject newDestroy = Instantiate(shipDestroy, transform.position, transform.rotation);
        Destroy(newDestroy, 2f);

        //should chage this code
        Invulnerable();
        sr.enabled = false;
        NewHighScore();


    }

    public void NewHighScore()
    {
        if (manageGame.CheckForHighScore(score))
        {
            highscoreText.text = score.ToString();
            newHighScorePanel.SetActive(true);

            if(PlayerPrefs.GetString("CurrentPlayer").Equals(""))
            {
                PlayerPrefs.SetString("CurrentPlayer", "Player");
            }
            //Set the highscore player name
            PlayerPrefs.SetString("HighscorePlayer", PlayerPrefs.GetString("CurrentPlayer"));
            //Set the highscore
            PlayerPrefs.SetInt("Highscore", score);

            //Displays the highscore
            highscoreListText.text = "HIGH SCORES" + "\n\n" + PlayerPrefs.GetString("HighscorePlayer") + ": " + PlayerPrefs.GetInt("Highscore");
            Invoke("GameOverPanel", 3f);
        }
        else
        {
            //Displays the highscore
            highscoreListText.text = "HIGH SCORES" + "\n\n" + PlayerPrefs.GetString("HighscorePlayer") + ": " + PlayerPrefs.GetInt("Highscore");
            GameOverPanel();
        }
    }

    void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
        newHighScorePanel.SetActive(false);
    }
}
