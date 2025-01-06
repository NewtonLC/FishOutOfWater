using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : BaseObstacle
{
    public float bounceHeight = 2.5f;  // How high the ball bounces
    private float gravity = -7f;
    private float yVelocity;        // Current vertical velocity
    private float floorY = -3.5f;      // Y position of the "floor"

    new void Start()
    {
        base.Start(); // Call the base Start method
    }

    new void FixedUpdate()
    {
        base.FixedUpdate(); // Call the base Update method for movement to the left

        // Apply gravity to vertical velocity
        yVelocity += gravity * Time.deltaTime;

        // Move the ball up or down based on velocity
        transform.position += new Vector3(0, yVelocity * Time.deltaTime, 0);

        // Check if the ball hits the floor
        if (transform.position.y <= floorY)
        {
            // Snap to the floor to prevent sinking
            transform.position = new Vector3(transform.position.x, floorY, transform.position.z);

            // Reverse velocity to make it "bounce" upwards
            yVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * bounceHeight);
        }
    }
}
