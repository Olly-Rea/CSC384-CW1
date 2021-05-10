using System.Collections.Generic;
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

        // Initialise the costs dictionary
        costs = new Dictionary<Upgradeable, int>();
        costs[MaxSpeed] = costs[TurningSpeed] = costs[FireRate] = costs[LaserDamage] = GameData.baseCost;
        // Initialise the levels dictionary
        levels = new Dictionary<Upgradeable, int>();
        levels[MaxSpeed] = levels[TurningSpeed] = levels[FireRate] = levels[LaserDamage] = 1;
        // Initialise the modifiers dictionary
        modifiers = new Dictionary<Upgradeable, float>();
        modifiers[MaxSpeed] = modifiers[LaserDamage] = 1.2f;
        modifiers[TurningSpeed] = 1.05f;
        modifiers[FireRate] = 0.8f;

        // Initialise the level bars dictionary
        levelBars = new Dictionary<Upgradeable, LevelBar>();
        levelBars[MaxSpeed] = levelBarsInput[0];
        levelBars[TurningSpeed] = levelBarsInput[1];
        levelBars[FireRate] = levelBarsInput[2];
        levelBars[LaserDamage] = levelBarsInput[3];
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
            GameData.playerUpgrades[key] *= modifiers[key];

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

    // Method to check the player score against the current cost of 
    public void CheckPrices() {
        // Loop through each level bar
        foreach (KeyValuePair<Upgradeable, int> upgrade in costs) {
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

// Public enums for the player "ship" modifiers
public enum Upgradeable {
    LaserDamage,
    FireRate,
    MaxSpeed,
    TurningSpeed
}