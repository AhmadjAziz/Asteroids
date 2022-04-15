using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySpaceships : MonoBehaviour
{
     
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private float shootingDelay;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private Collider2D boxCollider;
    [SerializeField] private bool disabled; //true when enemy is disabled.
    [SerializeField] private int points;

    private float lastShot = 0f;
    private Vector2 movement;
    private float angle;
    private Rigidbody2D rb;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            ShootPlayer();
            LookAtPlayer();
        }
       
    }

    private void FixedUpdate()
    {
        moveCharacter(movement); 
    }

    void ShootPlayer()
    {
        if(Time.time > lastShot + shootingDelay)
        {
            lastShot = Time.time;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //shoot a bullet
            GameObject newBullet = Instantiate(bullet, transform.position, rotation);

            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
            lastShot = Time.time;
            //Destroy bullet after 8 seconds.
            Destroy(newBullet, 8f);
        }
    }

    void LookAtPlayer()
    {
        //making Enemy look at player.
        direction = (player.position - transform.position);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(player.position != transform.position)
        {
            rb.rotation = angle;
        }
        
        direction.Normalize();
        movement = direction;
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)rb.position + (direction * speed * Time.fixedDeltaTime));
    }

    private void Disable()
    {
        //turn off colliders and sprite
        circleCollider.enabled = false;
        boxCollider.enabled = false;   
        spriteRenderer.enabled = false;
        disabled = true;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            player.SendMessage("PointsScore", points);
            //Explosion
            GameObject newExplosion = Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            //Destroy the Alien
            Disable();
        }
    }
}
