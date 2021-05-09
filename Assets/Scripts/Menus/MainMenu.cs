using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    // MAIN MENU METHODS

    public void showDifficultyMenu() {

    }

    // Method to load the GameScene
    public void Play(int difficulty) {
        // Set the diffciulty with:
        //  - base cost of items
        //  - speed of asteroids
        //  - explosive force of large asteroids
        //  - Spawn "buffer zone" around player
        if (difficulty == 0) {
            setDifficulty(200, 2, 6, 18); // Easy
        } else if (difficulty == 1) {
            setDifficulty(300, 4, 12, 24); // Medium
        } else {
            setDifficulty(400, 8, 18, 24); // Hard
        }
        // Load the Game Scene
        SceneManager.LoadScene(2);
    }

    // Method to set the game difficulty
    private void setDifficulty(int baseCost, int asteroidSpeed, int explosiveForce, int bufferZone) {
        InventoryController.baseCost = baseCost;
        AsteroidField.maxSpeed = asteroidSpeed;
        AsteroidField.explosiveForce = explosiveForce;
        AsteroidField.bufferZone = bufferZone;        
    }

    // Method to load the TutorialScene
    public void Tutorial() {
        SceneManager.LoadScene(1);
    }
    // Method to exit the application
    public void ExitApp() {
        Application.Quit();
    }
    
}
