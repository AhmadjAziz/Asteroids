using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMovement: MonoBehaviour
{
    [SerializeField] private float maxThrust;
    [SerializeField] private float maxTorque;
    [SerializeField] private float upperBoundary;
    [SerializeField] private float lowerBoundary;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        AsteroidPush();

    }

    // Update is called once per frame
   

    private void AsteroidPush()
    {
        Vector2 thrust = new Vector2
                         (Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

    //Check bounds for asteroids.
    internal void CheckBounds()
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
}
