using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    // MAIN MENU METHODS

    public void showDifficultyMenu() {

    }

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
            setDifficulty(0.2, 14, 200, 2, 6, 20); // Easy
        } else if (difficulty == 1) {
            setDifficulty(0.4, 12, 300, 4, 12, 28); // Medium
        } else {
            setDifficulty(0.6, 10, 400, 8, 18, 28); // Hard
        }
        // Load the Game Scene
        SceneManager.LoadScene(2);
    }

    // Method to set the game difficulty
    private void setDifficulty(double damageFactor, int laserDamage, int baseCost, int asteroidSpeed, int explosiveForce, int bufferZone) {
        // Player value(s)
        Player.damageFactor = damageFactor;
        Cannons.laserDamage = laserDamage;
        InventoryController.baseCost = baseCost;
        // Asteroid value(s)
        AsteroidField.maxSpeed = asteroidSpeed;
        AsteroidField.explosiveForce = explosiveForce;
        AsteroidField.bufferZone = bufferZone;        
    }

    // Method to load the TutorialScene
    public void Tutorial() {
        setDifficulty(0.2, 14, 200, 2, 6, 20); // Use "Easy" presets
        SceneManager.LoadScene(1);
    }
    // Method to exit the application
    public void ExitApp() {
        Application.Quit();
    }
    
}
