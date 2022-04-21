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

    private void AsteroidPush()
    {
        Vector2 thrust = new Vector2
                         (Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

    
}
