using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    
    // SerializeField for the scoreText game object
    [SerializeField] GameObject scoreObj;
    // private variable to hold the score and score text
    private int score = 0, scoreDisplay = 0;
    private TextMeshProUGUI scoreText;
    
    // Create a reference for the Score class
    public static Score Instance;
    private void Awake() {
        Instance = this;
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
    }

    // Method to increment the player score (and display to UI)
    public void Increment(int amount) {
        // Increment the "next" score (to have the update function bring score up to)
        score += amount;
        StartCoroutine(UpdateScore());
    }   

    // Method to increment the player score (and display to UI)
    public void Decrement(int amount) {
        // Decrement the score (checking to ensure it's not less than 0)...
        if ((score -= amount) < 0) score = 0;
        StartCoroutine(UpdateScore());
    }

    // Method to return the current score value
    public int GetScore() {
        return score;
    }

    // Method to give "incrementing" effect to text
    private IEnumerator UpdateScore() {
        // If the scoreDisplay is less than the lastest score, increment the score
        while(scoreDisplay < score) {
            scoreText.text = "SCORE: " + $"{(scoreDisplay += 1):00000}";
            yield return new WaitForSeconds(0.01f);
        }
        // If the scoreDisplay is greater than the lastest score, decrement the score
        while (scoreDisplay > score) {
            scoreText.text = "SCORE: " + $"{(scoreDisplay -= 1):00000}";
            yield return new WaitForSeconds(0.01f);
        }
    }

}
