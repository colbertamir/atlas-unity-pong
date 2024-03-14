using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class Ball : MonoBehaviour
{
    public float speed; // Speed of the ball
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private ScoreManager scoreManager; // Reference to the ScoreManager script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager script in the scene
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
            // Calculate the rebound angle based on the contact point with the paddle
            float hitPointY = collision.contacts[0].point.y;
            float paddleCenterY = collision.collider.bounds.center.y;
            float yOffsetFromCenter = hitPointY - paddleCenterY;
            float normalizedYOffset = yOffsetFromCenter / (collision.collider.bounds.size.y / 2);
            float angle = normalizedYOffset * 45f; // Adjust the angle multiplier as needed

            // Calculate the new direction of the ball based on the rebound angle
            Vector2 direction = Quaternion.Euler(0, 0, angle) * rb.velocity.normalized;

            // Apply the new direction to the ball's velocity
            rb.velocity = direction * speed;
        }
        else if (collision.gameObject.CompareTag("TopBorder") || collision.gameObject.CompareTag("BottomBorder"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            // Increment the score and reset the ball's position
            if (collision.gameObject.name == "LeftGoal")
            {
                scoreManager.IncrementPlayer1Score();
            }
            else if (collision.gameObject.name == "RightGoal")
            {
                scoreManager.IncrementPlayer2Score();
            }
            ResetBall();
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        LaunchBall();
        
        // Check if any player has reached the winning score
        if (scoreManager.player1Score >= scoreManager.winningScore || scoreManager.player2Score >= scoreManager.winningScore)
        {
            // If yes, wait for 2 seconds and then switch to the MainMenu scene
            Invoke("SwitchToMainMenu", 2f);
        }
    }

    void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
