using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    // Value to determine if the game is currently in a paused state
    public static bool GamePaused = false;
    // Serialized fields to hold the different pause-enabled menus
    [SerializeField] GameObject pauseMenu, upgradeMenu, deathMenu, saveloadMenu;

    // PAUSE MENU METHODS

    // Method to read in the input from the InputAction package
    public void onPause(InputAction.CallbackContext context) {
        if (context.performed) {
            // Check that the player isn't dead
            if (GameController.playerAlive) {
                // Check if the upgrade menu is active
                if (upgradeMenu.activeSelf) {
                    upgradeMenu.SetActive(false);
                    Resume();
                } else if (saveloadMenu.activeSelf) {
                    saveloadMenu.SetActive(false);
                    Resume();
                } else {
                    // Otherwise toggle the pause menu
                    ToggleMenu();
                }
            }
        }
    }

    // Method to toggle the pause Menu (and pause the game)
    public void ToggleMenu() {
        if (pauseMenu.activeSelf) {
            pauseMenu.SetActive(false);
            Resume();
        } else {
            pauseMenu.SetActive(true);
            Pause();
        }
    }

    // Methods to pause and resume the game
    public static void Pause() {
        GamePaused = true;
        Time.timeScale = 0f;
    }
    public static void Resume() {
        Time.timeScale = 1f;
        GamePaused = false;
    }

    // Method to exit the game back to the menu
    public void ExitGame() {
        Resume(); // Ensure time is running as normal
        SceneManager.LoadScene(0);
    }

}
