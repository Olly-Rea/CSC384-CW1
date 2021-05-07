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
        //  - Spawn "buffer zone" around player
        if (difficulty == 0) {
            setDifficulty(200, 2, 12); // Easy
        } else if (difficulty == 1) {
            setDifficulty(300, 4, 18); // Medium
        } else {
            setDifficulty(400, 8, 24); // Hard
        }
        // Load the Game Scene
        SceneManager.LoadScene(2);
    }

    // Method to set the game difficulty
    private void setDifficulty(int baseCost, int asteroidSpeed, int bufferZone) {
        AsteroidField.maxSpeed = asteroidSpeed;
        AsteroidField.bufferZone = bufferZone;        
        InventoryController.baseCost = baseCost;
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
