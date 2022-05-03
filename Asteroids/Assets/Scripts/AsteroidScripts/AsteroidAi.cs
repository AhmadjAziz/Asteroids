using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidAi : MonoBehaviour
{
  
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AsteroidsMovement asteroidMovement;
    [SerializeField] private int asteroidSize; //3 = Large, 2 = Medium, 1 = Small
    [SerializeField] private GameObject asteroidMedium;
    [SerializeField] private GameObject asteroidSmall;
    [SerializeField] private int points;
    [SerializeField] private GameObject onHitEffect;
    [SerializeField] private ScreenWrapper screenWrap;

    private ManageGame mg;
    private GameObject spaceship;

    

    // Start is called before the first frame update
    void Start()
    {
        
        //Find Player.
        spaceship = GameObject.FindWithTag("Player");
        mg = GameObject.FindObjectOfType<ManageGame>();
    }

    void Update()
    {
        screenWrap.CheckBounds();
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
            spaceship.SendMessage("CountAsteroidsDestroyed");

            //Causes an explosion at the asteroid hit by bullet.
            GameObject newExplosion = Instantiate(onHitEffect, transform.position, transform.rotation);
            Destroy(newExplosion, 2f);
            //Destroys the old asteroid
            Destroy(gameObject);

           
        }
        
    }
}
