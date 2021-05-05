using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    // Value to determine if the game is currently in a paused state
    public static bool GamePaused = false;
    [SerializeField] GameObject pauseMenu;

    // PAUSE MENU METHODS

    // Method to read in the input from the InputAction package
    public void onPause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (GamePaused = !GamePaused) {
                // Pause the game and show the menu
                toggleMenu(0f);
            } else {
                // Resume the game and hide the menu
                toggleMenu(1f);            
            }
        }
    }

    // Method to toggle the pause Menu (and pause the game)
    private void toggleMenu(float speed) {
        Time.timeScale = speed;
        pauseMenu.SetActive(GamePaused);
    }

    // Method to resume the GameScene
    public void Resume() {
        GamePaused = false;
        toggleMenu(1f);
    }
    // Method to exit the game back to the menu
    public void ExitGame() {
        GamePaused = false;
        Time.timeScale = 1; // Ensure time is running as normal
        SceneManager.LoadScene(0);
    }

}
