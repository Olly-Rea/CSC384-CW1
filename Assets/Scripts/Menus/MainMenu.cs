using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    // MAIN MENU METHODS

    // Method to load the GameScene
    public void Play(int difficulty) {

        // Clear any saveData from the GameController
        GameController.saveData = null;
        // Ensure the player is treated as Alive
        GameController.playerAlive = true;

        // Set the game diffciulty
        if (difficulty == 0) {
            GameController.setDifficulty(
                100, // baseCost
                14, // MaxSpeed
                0.4f, // TurningSpeed
                0.5f, // FireRate
                18, // LaserDamage
                0.2, // Asteroid damageFactor
                2, // asteroidSpeed
                6, // explosiveForce
                22, // bufferZone
                1f // scoreModifier
            );
        } else if (difficulty == 1) {
            GameController.setDifficulty(
                150, // baseCost
                14, // MaxSpeed
                0.4f, // TurningSpeed
                0.5f, // FireRate
                16, // LaserDamage
                0.4, // Asteroid damageFactor
                4, // asteroidSpeed
                10, // explosiveForce
                28, // bufferZone
                1.5f // scoreModifier
            );
        } else {
            GameController.setDifficulty(
                200, // baseCost
                14, // MaxSpeed
                0.36f, // TurningSpeed
                0.46f, // FireRate
                14, // LaserDamage
                0.6, // Asteroid damageFactor
                6, // asteroidSpeed
                14, // explosiveForce
                28, // bufferZone
                2f // scoreModifier
            );
        }

        // Load the Game Scene
        SceneManager.LoadScene(2);
    }

    // Method to load the TutorialScene
    public void Tutorial() {
        // Clear any saveData from the GameController
        GameController.saveData = null;

        // Set the Tutorial Difficulty (Using values from "Easy")
        GameController.setDifficulty( 
            0, // baseCost
            14, // MaxSpeed
            0.6f, // TurningSpeed
            0.4f, // FireRate
            28, // LaserDamage
            0, // Asteroid damageFactor
            2, // asteroidSpeed
            6, // explosiveForce
            20, // bufferZone
            1f // scoreModifier
        );

        // Load the tutorial Scene
        SceneManager.LoadScene(1);
    }
    
    // Method to exit the application
    public void ExitApp() {
        Application.Quit();
    }
    
}
