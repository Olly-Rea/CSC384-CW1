using UnityEngine;
using UnityEngine.SceneManagement;
// using the Upgradeable enums
using static Upgradeable;

public class MainMenu : MonoBehaviour {
    
    // MAIN MENU METHODS

    // Method to load the GameScene
    public void Play(int difficulty) {
        // Set the diffciulty with:
        //  - player-asteroid collision damage factor
        //  - initial laser-damage starting value
        //  - base cost of items
        //  - speed of asteroids
        //  - explosive force of large asteroids
        //  - Spawn "buffer zone" around player
        if (difficulty == 0) {
            setDifficulty(18, 100, 0.2, 2, 6, 22); // Easy
        } else if (difficulty == 1) {
            setDifficulty(16, 200, 0.4, 4, 10, 28); // Medium
        } else {
            setDifficulty(14, 300, 0.6, 6, 14, 28); // Hard
        }
        // Load the Game Scene
        SceneManager.LoadScene(2);
    }

    // Method to set the game difficulty
    private void setDifficulty(int laserDamage, int baseCost, double damageFactor, int asteroidSpeed, int explosiveForce, int bufferZone) {
        // Set base "Upgradeable" properties
        GameData.baseCost = baseCost;
        GameData.playerUpgrades[MaxSpeed] = 14;
        GameData.playerUpgrades[TurningSpeed] = 0.4f;
        GameData.playerUpgrades[FireRate] = 0.5f;
        GameData.playerUpgrades[LaserDamage] = laserDamage;
        // Asteroid value(s)
        GameData.damageFactor = damageFactor;
        GameData.asteroidSpeed = asteroidSpeed;
        GameData.explosiveForce = explosiveForce;
        GameData.bufferZone = bufferZone;
    }

    // Method to load the TutorialScene
    public void Tutorial() {
        setDifficulty(28, 200, 0, 2, 6, 20); // Use "Easy" presets
        SceneManager.LoadScene(1);
    }
    // Method to exit the application
    public void ExitApp() {
        Application.Quit();
    }
    
}
