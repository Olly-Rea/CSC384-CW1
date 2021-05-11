using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using the Upgradeable enums
using static Upgradeable;

public class GameController : MonoBehaviour {

    // Game values
    public static bool playerAlive;
    public static Difficulty difficultyLevel;
    public static float scoreModifier;

    // Dictionary to hold PlayerUpgrade values
    public static int baseCost;
    public static Dictionary<Upgradeable, float> playerUpgrades = new Dictionary<Upgradeable, float>();

    // Asteroid values
    public static double damageFactor;
    public static int asteroidSpeed;
    public static int explosiveForce;
    public static int bufferZone;

    public static SaveData saveData;

    // Enable movement and cannons on game start
    void Start() {
        Movement.canMove = Cannons.canFire = true;
    }

    // Method to set the game difficulty
    public static void setDifficulty(int _baseCost, float _maxSpeed, float _turningSpeed, float _fireRate, int _laserDamage, 
        double _damageFactor, int _asteroidSpeed, int _explosiveForce, int _bufferZone, float _scoreModifier) {
        // Set base "Upgradeable" properties
        baseCost = _baseCost;
        playerUpgrades[MaxSpeed] = _maxSpeed;
        playerUpgrades[TurningSpeed] = _turningSpeed;
        playerUpgrades[FireRate] = _fireRate;
        playerUpgrades[LaserDamage] = _laserDamage;
        // Asteroid values
        damageFactor = _damageFactor;
        asteroidSpeed = _asteroidSpeed;
        explosiveForce = _explosiveForce;
        bufferZone = _bufferZone;
        // Game value
        scoreModifier = _scoreModifier;
    }

    // Method to call on the Save/Load Controller SaveGame Method
    public static void SaveGame(int slot) {
        // Initialise the SaveData class with all of the GameController data
        saveData = new SaveData(
            // GameController stuff
            difficultyLevel,
            baseCost,
            playerUpgrades,
            damageFactor,
            asteroidSpeed,
            explosiveForce,
            bufferZone,
            scoreModifier,
            // UpgradeController stuff
            UpgradeController.Instance.GetCostsAsArray(),
            UpgradeController.Instance.GetLevelsAsArray(),
            UpgradeController.Instance.GetModifiersAsArray(),
            // Player health stuff
            HealthBar.Instance.GetShield(),
            HealthBar.Instance.GetHealth()
        );
        // Call on the save method
        SaveLoadController.SaveGame(slot, saveData);
    }

    // Method to call on the Save/Load Controller LoadGame Method
    public static void LoadGame(int slot) {
        // Load the SaveData from file
        saveData = SaveLoadController.LoadGame(slot);

        setDifficulty(
            // "Upgradeable" properties
            saveData.baseCost,
            saveData.playerUpgrades[MaxSpeed],
            saveData.playerUpgrades[TurningSpeed],
            saveData.playerUpgrades[FireRate],
            (int)saveData.playerUpgrades[LaserDamage],
            // Asteroid properties
            saveData.damageFactor,
            saveData.asteroidSpeed,
            saveData.explosiveForce,
            saveData.bufferZone,
            // Game Values
            saveData.scoreModifier
        );

        // Ensure the player is treated as alive on load
        playerAlive = true;

        // Load the Game Scene
        SceneManager.LoadScene(2);
    }

}

// "Difficulty level" enums
public enum Difficulty {
    Easy,
    Medium,
    Hard
}