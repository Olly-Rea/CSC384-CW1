using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrackPlayer : MonoBehaviour {

    // Public static bool to inform if the current task has been completed
    public static bool taskComplete;

    // The player gameobject to use
    [SerializeField] GameObject player;

    // Value to hold the players last position (for distance tracking)
    private Vector2 lastPos;
    // Private variables to track the players tutorial progress
    private CurrentTask task;
    private float distance;
    private int shotsFired;

    private void Start() {
        // Initialise the task and starting values of the player tracking
        task = CurrentTask.Movement;
        taskComplete = false;

        lastPos = player.transform.position;
        shotsFired = 0;

        // Run the check to see how far the player has travelled
        StartCoroutine(TrackPlayerDistance());
    }

    private void Update() {
        // Check the current task
        if(task == CurrentTask.Movement) {
            // Increment and check the players distance travelled
            if (distance > 50) {
                // Indicate that the task has been completed
                taskComplete = true;
                // Stop tracking player distance
                StopCoroutine(TrackPlayerDistance());
                // Set the next task
                task = CurrentTask.Shooting;
            }
        } else if(task == CurrentTask.Shooting) {
            // Check the number of shots fired
            if (shotsFired > 5) {
                // Indicate that the task has been completed
                taskComplete = true;
                // Set the next task
                task = CurrentTask.TrialRun;
            }
        } else if(task == CurrentTask.TrialRun) {
            // Check the number of asteroids destroyed
            if (Score.Instance.GetScore() >= 280) {
                // Indicate that the task has been completed
                taskComplete = true;
                // Add end dialogue to the DialogueController
                DialogueController.Instance.addSentence("Great job pilot! You're ready for action!\nTo end the tutorial, press NEXT");
                DialogueController.Instance.StartDialogue();
                // Set the next task
                task = CurrentTask.End;
            }
        }
    }

    // Method to track the distance the player has travelled
    IEnumerator TrackPlayerDistance() {
        // Increment the distance the player has traveled
        distance += Vector2.Distance(player.transform.position, lastPos);
        lastPos = player.transform.position;
        // wait a small period before calling the check again
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(TrackPlayerDistance());
    }

    // Method to read in the boolean input from Unity's 'InputAction' fire command
    public void onFire(InputAction.CallbackContext context) {
        // Increment the number of shots fired
        if (Cannons.canFire) shotsFired++;
    }

}

public enum CurrentTask {
    Movement,
    Shooting,
    TrialRun,
    End
}