using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationalThrust;
    [SerializeField] private Rigidbody2D rb;
    
    
    private float thrustInput;
    private float rotationalInput;
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    public void AddThrust()
    {
        rb.AddRelativeForce(Vector2.up * thrustInput);
        transform.Rotate(Vector3.forward * rotationalInput * Time.deltaTime * -rotationalThrust);
        //The negative sign helps invert the keys
        // rb.AddTorque(-rotationalInput);
    }

    public void CheckMovement()
    {
        thrustInput = Input.GetAxis("Vertical");
        rotationalInput = Input.GetAxis("Horizontal");
    }
}
