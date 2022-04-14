using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySpaceships : MonoBehaviour
{
     
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;

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
       LookAtPlayer();
    }

    private void FixedUpdate()
    {
        moveCharacter(movement); 
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
}
