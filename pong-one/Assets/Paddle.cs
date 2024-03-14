using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public int playerNumber = 1; // Player number (1 or 2)
    public float speed; // Speed

    void Update()
    {
        // Move the paddle based on the player number
        if (playerNumber == 1)
        {
            // Player 1 uses the 'W' and 'S' keys
            if (Input.GetKey(KeyCode.W))
            {
                MovePaddle(Vector2.up);
            }
            if (Input.GetKey(KeyCode.S))
            {
                MovePaddle(Vector2.down);
            }
        }
        else if (playerNumber == 2)
        {
            // Player 2 uses the up and down arrow keys
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MovePaddle(Vector2.up);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MovePaddle(Vector2.down);
            }
        }
    }

    void MovePaddle(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
