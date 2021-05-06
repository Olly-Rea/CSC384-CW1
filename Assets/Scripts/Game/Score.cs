using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    
    // SerializeField for the scoreText game object
    [SerializeField] GameObject scoreObj;
    // private variable to hold the score and score text
    private int score = 0;
    private TextMeshProUGUI scoreText;
    
    // Create a reference for the Score class
    public static Score Instance;
    private void Awake() {
        Instance = this;
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
    }

    // Method to increment the player score (and display to UI)
    public void Increment(int amount) {
        // Increment the score and display it in the UI
        scoreText.text = "SCORE: " + $"{(score += amount):00000}";
    }

    // Method to return the current score value
    public int getScore() {
        return score;
    }

}
