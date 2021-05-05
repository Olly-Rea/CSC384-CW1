using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
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
    
}
