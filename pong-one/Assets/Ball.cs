using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to change the speed of the ball
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    // Launch the ball in a random direction
    void LaunchBall()
    {
        float randomAngle = Random.Range(45f, 135f); // Random angle between 45 and 135 degrees
        Vector2 launchDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right; // Convert angle to vector
        rb.velocity = launchDirection * speed;
    }

    // Handle collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Reverse the direction of the ball when it collides with a paddle
            Vector2 reflection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal).normalized;
            rb.velocity = reflection * speed;
        }
    }

    // Reset ball position and velocity
    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        LaunchBall();
    }
}