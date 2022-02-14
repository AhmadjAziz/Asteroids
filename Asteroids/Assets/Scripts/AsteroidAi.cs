using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidAi : MonoBehaviour
{
    public float maxThrust;
    public float maxTorque;
    public Rigidbody2D rb;
    public float upperBoundary;
    public float lowerBoundary;
    public float leftBoundary;
    public float rightBoundary;

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

    private void AsteroidPush()
    {
        Vector2 thrust = new Vector2
                         (Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

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
}
