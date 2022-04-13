using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidAi : MonoBehaviour
{
    [SerializeField] private float maxThrust;
    [SerializeField] private float maxTorque;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float upperBoundary;
    [SerializeField] private float lowerBoundary;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    [SerializeField] private int asteroidSize; //3 = Large, 2 = Medium, 1 = Small
    [SerializeField] private GameObject asteroidMedium;
    [SerializeField] private GameObject asteroidSmall;
    [SerializeField] private int points;
    [SerializeField] private GameObject onHitEffect;

    private ManageGame mg;
    private GameObject spaceship;

    // Start is called before the first frame update
    void Start()
    {
        AsteroidPush();

        //Find Player.
        spaceship = GameObject.FindWithTag("Player");
        mg = GameObject.FindObjectOfType<ManageGame>();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenWrapper();
        
    }

    //Moves the asteroids in random directions at the start of the scence.
    private void AsteroidPush()
    {
        Vector2 thrust = new Vector2
                         (Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

    //Makes it so that if asteroids go out of game view it comes back in from the other side.
    private void ScreenWrapper()
    {
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

    //Collision between asteroid and objects. i.e bullet.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks by tag for a bullet trigger.
        if (collision.CompareTag("bullet"))
        {
            //destroy the bullet.
            Destroy(collision.gameObject);
            
            //Check the size and based on spawn the next asteroid.
            if(asteroidSize == 3)
            {
                //Spawn two medium asteroid.
                Instantiate(asteroidMedium, transform.position, transform.rotation);
                Instantiate(asteroidMedium, transform.position, transform.rotation);
                
                mg.UpdateNumAsteroids(1);

            } else if(asteroidSize == 2)
            {
                //Spawn two small asteroids.
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                
                mg.UpdateNumAsteroids(1);

            } else if (asteroidSize == 1)
            {
                mg.UpdateNumAsteroids(-1);

            }
            //Destroys the current asteroid hit by bullet.
            spaceship.SendMessage("PointsScore", points);

            //Causes an explosion at the asteroid hit by bullet.
            GameObject newExplosion = Instantiate(onHitEffect, transform.position, transform.rotation);
            Destroy(newExplosion, 2f);
            //Destroys the old asteroid
            Destroy(gameObject);

        }
        
    }
}
