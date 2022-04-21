using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField] private float maxThrust;
    [SerializeField] private float maxTorque;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ScreenWrapper screenWrap;

    void Start()
    {
        PowerupPush();
    }
    private void Update()
    {
        screenWrap.CheckBounds();
    }

    private void PowerupPush()
    {
        Vector2 thrust = new Vector2
                         (Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

}
