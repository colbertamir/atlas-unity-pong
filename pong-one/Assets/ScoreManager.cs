using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int winningScore = 11; // Winning score
    public int player1Score = 0; // Player 1's score
    public int player2Score = 0; // Player 2's score
    public TMP_Text leftScoreText; // Reference to the UI Text element for Player 1's score
    public TMP_Text rightScoreText; // Reference to the UI Text element for Player 2's score
    public TMP_Text youWinText; // Reference to the UI Text element to display "YouWin!"

    // Method to increment player 1's score
    public void IncrementPlayer1Score()
    {
        player1Score++;
        leftScoreText.text = player1Score.ToString(); // Update the UI Text for Player 1's score
        CheckWinCondition();
    }

    // Method to increment player 2's score
    public void IncrementPlayer2Score()
    {
        player2Score++;
        rightScoreText.text = player2Score.ToString(); // Update the UI Text for Player 2's score
        CheckWinCondition();
    }

    // Method to check if either player has reached the winning score
    private void CheckWinCondition()
    {
        if (player1Score >= winningScore || player2Score >= winningScore)
        {
            // Game over, display "You Win!" UI
            youWinText.gameObject.SetActive(true);
            // You can add more actions here such as stopping the game, disabling player input, etc.
        }
    }
}
