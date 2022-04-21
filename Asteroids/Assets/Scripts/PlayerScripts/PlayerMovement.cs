using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationalThrust;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxThrust;
    [SerializeField] private Vector3 speed;
    [SerializeField] private float forwardThrust;

    private float thrustInput;
    private float rotationalInput;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddThrust()
    {
        speed = rb.velocity;
        transform.Rotate(Vector3.forward * rotationalInput * Time.deltaTime * -rotationalThrust);
       
        if ((speed.y < maxThrust && speed.y > -maxThrust) && (speed.x < maxThrust && speed.x > -maxThrust))
        {
            rb.AddRelativeForce(Vector2.up * thrustInput * forwardThrust);  
        }
         
    }

    public void CheckMovement()
    {
        thrustInput = Input.GetAxis("Vertical");
        rotationalInput = Input.GetAxis("Horizontal");
    }
}
