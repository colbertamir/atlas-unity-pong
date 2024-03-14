using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed; // Speed of the ball
    private Rigidbody2D rb;
    private Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        LaunchBall();
    }

    void LaunchBall()
    {
        float randomDirection = Random.Range(0f, 1f);
        Vector2 launchDirection = new Vector2((randomDirection < 0.5f) ? -1 : 1, Random.Range(-0.5f, 0.5f)).normalized;
        rb.velocity = launchDirection * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Vector2 reflection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal).normalized;
            rb.velocity = reflection * speed;
        }
        else if (collision.gameObject.CompareTag("TopBorder") || collision.gameObject.CompareTag("BottomBorder"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        LaunchBall();
    }
}
