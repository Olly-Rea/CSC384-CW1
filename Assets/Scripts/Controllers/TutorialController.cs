using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

    // Public static field to tell if a task is complete
    public static bool taskComplete = false;

    // Serialized field to hold the nextButton
    [SerializeField] GameObject nextButtonObject;
    // Private variable to hold the Button component
    private Button nextButton;

    // Private int for counting dialogue
    private int dialogueCounter;

    // Start is called before the first frame update
    void Start() {
        // Get the button component from the nextButton
        nextButton = nextButtonObject.GetComponent<Button>();
        // Disable movement and cannons
        Movement.canMove = Cannons.canFire = false;
        // Set dialogue counger to 0
        dialogueCounter = 0;
    }

    private void Update() {
        // Check to see if the current task has been completed
        if (TrackPlayer.taskComplete) {
            if (dialogueCounter < 4) {
                nextButtonObject.SetActive(true);
                TrackPlayer.taskComplete = false;
            // Check for tutorial end
            } else {
                TrackPlayer.taskComplete = false;
                nextButton.onClick.AddListener(EndTutorial);
            }
        }
    }

    // Method to perform actions based on the dialogue counter
    public void incrementDialogueCounter() {
        // Increment dialogue counter
        dialogueCounter++;

        // Check if dialogue has reached section to test movement
        if (dialogueCounter == 1)  {
            // Hide the next button until the movement section has been completed
            DialogueController.waitForEvent = true;
            // Add an event listener to call on the MoveTutorial when the dialogue has finished
            DialogueController.dialogueEvent.AddListener(MoveTutorial);
        }
        // Check if dialogue has reached section to test shooting
        if (dialogueCounter == 2) {
            // Add an event listener to call on the ShootTutorial when the dialogue has finished
            DialogueController.dialogueEvent.AddListener(ShootTutorial);
        }
        if (dialogueCounter == 3) {
            // Show the next button for the remainder of the dialogue
            DialogueController.waitForEvent = false;
        }
        // Check if dialogue has reached section to do a trial run
        if (dialogueCounter == 4) {
            // Add an event listener to call on the TrialRun when the dialogue has finished
            DialogueController.dialogueEvent.AddListener(TrialRun);
        }

    }

    // 1. Practice flying (Disable weapons and asteroids)
    public void MoveTutorial() {
        // Remove this listener from the dialogue controller
        DialogueController.dialogueEvent.RemoveListener(MoveTutorial);
        // Allow movement...
        Movement.canMove = true;
        // ...and un-pause the game
        PauseController.Resume();
    }

    // 2. Practice shooting (enable weapons)
    public void ShootTutorial() {
        // Remove this listener from the dialogue controller
        DialogueController.dialogueEvent.RemoveListener(ShootTutorial);
        // Allow shooting
        Cannons.canFire = true;
    }

    // 3. Practice run (enable some asteroids)
    public void TrialRun() {
        // Remove this listener from the dialogue controller
        DialogueController.dialogueEvent.RemoveListener(TrialRun);
        // Add 4 asteroids to the scene
        AsteroidField.maxActive = 4;
    }

    // Method to be called at the end of the tutorial
    public void EndTutorial() {
        // Return to the main menu
        SceneManager.LoadScene(0);
    }

}
