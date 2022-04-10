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

    // Start is called before the first frame update
    void Start()
    {
        AsteroidPush();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
