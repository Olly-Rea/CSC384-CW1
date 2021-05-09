using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    // Value to determine if the game is currently in a paused state
    public static bool GamePaused = false;
    [SerializeField] GameObject pauseMenu;

    // PAUSE MENU METHODS

    // Method to read in the input from the InputAction package
    public void onPause(InputAction.CallbackContext context) {
        if (context.performed) {
            ToggleMenu();
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
