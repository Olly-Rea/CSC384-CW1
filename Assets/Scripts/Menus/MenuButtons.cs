using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
    
    // MAIN MENU METHODS

    // Method to load the GameScene
    public void Play() {
        SceneManager.LoadScene(2);
    }
    // Method to load the TutorialScene
    public void Tutorial() {
        SceneManager.LoadScene(1);
    }
    // Method to exit the application
    public void ExitApp() {
        Application.Quit();
    }
    
    // PAUSE MENU METHODS

    // Method to resume the GameScene
    public void Resume() {
        Debug.Log("Resume!");
    }
    // Method to exit the game back to the menu
    public void ExitGame() {
        SceneManager.LoadScene(0);
    }

}
