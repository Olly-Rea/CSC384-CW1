using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour {
    
    // // Serialize field for the value to track
    // [SerializeField] GameData toTrack;
    [SerializeField] Color fillColor;

    // Serialize field to hold the starting value of the level bar
    [SerializeField] int startingValue;

    // Public value to hold the cost of the associated upgrade
    public int cost;

    // Private variable to hold the value and max value of the bar
    private int value, maxValue;
    // Private variable to hold each of the bar segments
    private GameObject[] bars;
    private Button incrementer;

    // Initialise the LevelBars values
    void Start() {
        // Set the starting values of the level bar
        value = startingValue;
        maxValue = transform.childCount;

        // Initialise the bars array
        bars = new GameObject[maxValue];
        // Get and add all the level bar segments
        for(int i = 0; i < maxValue; i++) {
            bars[i] = transform.GetChild(i).gameObject;
        }

        // Get the incrementer button
        incrementer = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject.GetComponent<Button>();

        // toTrack = new GameData();
    }

    // Method to "level up" the associated data (and display the change on the level bar)
    public void LevelUp() {
        if(!AtMax()) {
            bars[value++].GetComponent<Image>().color = fillColor;
        }
        if (Score.Instance.GetScore() < cost) {
            incrementer.interactable = false;
        } else {
            if(!incrementer.interactable) {
                incrementer.interactable = true;
            }
        }
    }

    // Method to return whether the level bar is "maxed out"
    public bool AtMax() {
        return value >= maxValue;
    }

}
