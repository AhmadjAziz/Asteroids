using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
public class SpaceShipController : MonoBehaviour
{
    
    [SerializeField] private float rotationalThrust;
    [SerializeField] private float thrustInput;
    [SerializeField] private float rotationalInput;
    [SerializeField] private float upperBoundary;
    [SerializeField] private float lowerBoundary;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
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
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int score;
    //need to set up max speed of spaceship.
    // private float maxShipSpeed = 200;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //starts with no points.
        score = 0;
        scoreText.text = "Score: " + score;

        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        ScreenWrapper();
        FireBullet();

    }
    void FixedUpdate()
    {
        //Add some thrust every few frame to give that delayed feel of old retro games.
        AddThrust();
    }

    /**
     * Checks for input from keyboard, W,A,S.
     * D is unassigned from unity InputManager as no backward movement.
     * **/
    private void CheckMovement()
    {
        thrustInput = Input.GetAxis("Vertical");
        rotationalInput = Input.GetAxis("Horizontal");
    }

    /**
     * Adds relative force that acclerates over time.
     **/
    private void AddThrust()
    {
        rb.AddRelativeForce(Vector2.up * thrustInput);
        //The negative sign helps invert the keys
       // rb.AddTorque(-rotationalInput);
    }

    /**
     * If spaceship goes beyond a boundary, it will pop back from another side.
     **/
    private void ScreenWrapper()
    {

        transform.Rotate(Vector3.forward * rotationalInput *Time.deltaTime * -rotationalThrust);
        Vector2 newPosition = transform.position;
        if (transform.position.y > upperBoundary)
        {
            newPosition.y = lowerBoundary;
        }
        if (transform.position.y < lowerBoundary)
        {
            newPosition.y = upperBoundary;
        }
        if (transform.position.x > rightBoundary)
        {
            newPosition.x = leftBoundary;
        }
        if (transform.position.x < leftBoundary)
        {
            newPosition.x = rightBoundary;
        }
        transform.position = newPosition;
    }

    /**
     * Checks input from mouse to fire bullet.
     **/
    private void FireBullet()
    {
        if(Input.GetButtonDown("Fire"))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletSpeed);
            Destroy(newBullet, 5.0f);
        }
    }

    void PointsScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    /**
     * can be callled to make player invincible.
     **/
    void Invincibility()
    {
        horizontalCollider.enabled = false;
        verticalCollider.enabled = false;
        sr.color = inColor;
    }
    /**
     * Turns off invinciblity.
     **/
    void Targetable()
    {
        horizontalCollider.enabled = true;
        verticalCollider.enabled = true;
        sr.color = normColor;
    }

    //spaceship colliding with asteroid. Player dies if lives end.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //subtracts life on collision
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameObject newDestroy = Instantiate(shipDestroy, transform.position, transform.rotation);
            Destroy(newDestroy, 2f);
            
            //GameOver();
        }
        //plays the collision sound.
        //causes a tiny explosion when spaceship collides with asteroid.
        GameObject newExplosion = Instantiate(shipCollision, transform.position, transform.rotation);
        Destroy(newExplosion, 2f);
        Destroy(collision.gameObject);

        //Makes player invincible for 2 seconds after taking damage to not get hit multiple times at same time.
        Invincibility();
        Invoke("Targetable",2f);
                         
    }



}
