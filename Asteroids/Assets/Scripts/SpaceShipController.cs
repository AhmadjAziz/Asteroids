using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public Rigidbody2D rb;
    //public float thrust;
    //public float rotationalThrust;
    private float thrustInput;
    private float rotationalInput;
    public float upperBoundary;
    public float lowerBoundary;
    public float leftBoundary;
    public float rightBoundary;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        ScreenWrapper();
    }
    void FixedUpdate()
    {
        addThrust();
    }

    /**
     * Checks for input from keyboard, W,A,S.
     * D is unassigned from unity InputManager as no backward movement.
     * **/
    private void checkInput()
    {
        thrustInput = Input.GetAxis("Vertical");
        rotationalInput = Input.GetAxis("Horizontal");
    }

    /**
     * Adds relative force that acclerates over time.
     **/
    private void addThrust()
    {
        rb.AddRelativeForce(Vector2.up * thrustInput);
        //The negative sign helps invert the keys
        rb.AddTorque(-rotationalInput);
    }

    /**
     * If spaceship goes beyond a boundary, it will pop back from another side.
     **/
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
