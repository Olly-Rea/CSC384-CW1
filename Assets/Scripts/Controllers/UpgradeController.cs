using System.Collections.Generic;
using System;
using UnityEngine;
// Use the Upgradeable enum in upgradeController (without requiring namespace)
using static Upgradeable;

public class UpgradeController : MonoBehaviour {
    
    // Dictionaries to hold the current cost and level of each upgrade
    private Dictionary<Upgradeable, int> costs, levels;
    // Dictionary to hold the modifier values
    private Dictionary<Upgradeable, float> modifiers;

    // Input list and Upgradeable-Key Dictionary to hold the menu level bars
    [SerializeField] private List<LevelBar> levelBarsInput;
    private Dictionary<Upgradeable, LevelBar> levelBars;

    [SerializeField] private GameObject upgradeMenu;

    // Value to hold the score instance in cache
    private Score score;

    // Create a reference for this UpgradeController
    public static UpgradeController Instance;
    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        // Initialise the cached score instance
        score = Score.Instance;

        // Initialise the dictionaries
        costs = new Dictionary<Upgradeable, int>();
        levels = new Dictionary<Upgradeable, int>();
        modifiers = new Dictionary<Upgradeable, float>();
        levelBars = new Dictionary<Upgradeable, LevelBar>();

        // Fill the level bars from the input SerializeField array
        levelBars[MaxSpeed] = levelBarsInput[0];
        levelBars[TurningSpeed] = levelBarsInput[1];
        levelBars[FireRate] = levelBarsInput[2];
        levelBars[LaserDamage] = levelBarsInput[3];

        // Check if game vaues to be loaded from a save file
        if (GameController.saveData != null) {
            // Get the SaveData instance and load the saved data values
            SaveData savedData = GameController.saveData;
            LoadValues(savedData.costs, savedData.levels, savedData.modifiers);
        } else {
            // Otherwise fill all dictionaries with usual starting vals
            costs[MaxSpeed] = costs[TurningSpeed] = costs[FireRate] = costs[LaserDamage] = GameController.baseCost;
            levels[MaxSpeed] = levels[TurningSpeed] = levels[FireRate] = levels[LaserDamage] = 0;
            modifiers[MaxSpeed] = 1.05f;
            modifiers[TurningSpeed] = 1.1f;
            modifiers[FireRate] = 0.8f;
            modifiers[LaserDamage] = 1.2f;
        }

    }

    // Method to show the upgrade menu
    public void showMenu() {
        // Pause the game
        PauseController.Pause();
        // Show the menu
        upgradeMenu.SetActive(true);
        // Run the method to check the prices
        CheckPrices();
    }

    // Method to handle purchasing of modifiers
    public bool PurchaseUpgrade(Upgradeable key) {
        // Check the player has a high enough score
        if (score.GetScore() >= costs[key]) {
            // Decrement the score
            score.Decrement(costs[key]);
            
            // Increment the upgrade level
            levels[key]++;
            // Make the upgrade
            GameController.playerUpgrades[key] *= modifiers[key];

            // Increment the score for the next level (+50%, rounded to nearest 10)
            costs[key] = (int) System.Math.Round(costs[key] / 10d) * 15;
            // Check thge prices again
            CheckPrices();

            // return "succesful purchase" (true)
            return true;
        // Otherwise return "no purchase" (false)
        } else {
            return false;
        }
    }

    // Method to return the current level of a levelBar (for "onload" of save file)
    public int CheckLevel(Upgradeable upgrade) {
        return levels[upgrade];
    }

    // Method to check the player score against the current cost of each upgrade
    public void CheckPrices() {
        // Loop through each level bar
        foreach (KeyValuePair<Upgradeable, int> upgrade in costs) {
            // Check if the LevelBar is "Maxed out" (and make so if true)
            if (levels[upgrade.Key] >= 6) {
                levelBars[upgrade.Key].setMaxed();            
            } else {
                // Enable/Disable Incrementer button & update cost of Levelbars
                if (score.GetScore() < upgrade.Value) {
                    levelBars[upgrade.Key].Disable();
                } else {
                    levelBars[upgrade.Key].Enable();
                }
                levelBars[upgrade.Key].SetCost(costs[upgrade.Key]);
            }
        }
    }

    // Method to get all the costs as an array (for serialisable saving)
    public int[] GetCostsAsArray() {
        int[] tempArr = new int[4];
        int i = 0;
        foreach(Upgradeable upgrade in Enum.GetValues(typeof(Upgradeable))) {
            tempArr[i++] = costs[upgrade];
        }
        return tempArr;
    }
    // Method to get all the levels as an array (for serialisable saving)
    public int[] GetLevelsAsArray() {
        int[] tempArr = new int[4];
        int i = 0;
        foreach(Upgradeable upgrade in Enum.GetValues(typeof(Upgradeable))) {  
            tempArr[i++] = levels[upgrade];
        }
        return tempArr;
    }
    // Method to get all the modifiers as an array (for serialisable saving)
    public float[] GetModifiersAsArray() {
        float[] tempArr = new float[4];
        int i = 0;
        foreach(Upgradeable upgrade in Enum.GetValues(typeof(Upgradeable))) {  
            tempArr[i++] = modifiers[upgrade];
        }
        return tempArr;
    }

    // Method to load values from SaveData to the upgrade
    public void LoadValues(int[] _costs, int[] _levels, float[] _modifiers) {
        // Load values into the costs dictionary
        costs[MaxSpeed] = _costs[0];
        costs[TurningSpeed] = _costs[1];
        costs[FireRate] = _costs[2];
        costs[LaserDamage] = _costs[3];
        // Load values into the levels dictionary
        levels[MaxSpeed] = _levels[0];
        levels[TurningSpeed] = _levels[1];
        levels[FireRate] = _levels[2];
        levels[LaserDamage] = _levels[3];
        // Load values into the modifiers dictionary
        modifiers[MaxSpeed] = _modifiers[0];
        modifiers[TurningSpeed] = _modifiers[1];
        modifiers[FireRate] = _modifiers[2];
        modifiers[LaserDamage] = _modifiers[3];
    }

}

// Public enums for the player "ship" modifiers
public enum Upgradeable {
    MaxSpeed,
    TurningSpeed,
    FireRate,
    LaserDamage
}