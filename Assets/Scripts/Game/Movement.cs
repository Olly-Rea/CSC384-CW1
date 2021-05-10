using UnityEngine;
using System;
using UnityEngine.InputSystem;
// using the Upgradeable enums
using static Upgradeable;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    // Public static value to determine if the player can move
    public static bool canMove = true;

    // private values to hold the player rigidbody and the player ship game object
    private Rigidbody2D rigidBody;
    private GameObject playerShip;

    // private values to hold the players input and velocity
    private Vector2 currentInput;
    private Vector2 velocity;

    // Speed modifier (Constant)
    private const double SPEED_FACTOR = 0.08;

    // Method called when class initialized
    void Awake() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerShip = transform.GetChild(0).gameObject;
    }
    
    // Method to move the player model
    private void Move(Vector2 input) {
        // Get current rigidbody velocity
        velocity = rigidBody.velocity;
        
        // Stop 'ragdoll' rotation if the player calls on the rotation controls
        if (Math.Abs(input.x) == 1 || Math.Abs(Math.Round(input.x, 1)) == 0.7) {
            rigidBody.angularVelocity = 0;
        }

        // Set the rigidbody's new rotation
        rigidBody.rotation -= (float)(input.x * GameData.playerUpgrades[TurningSpeed]);
        // Get the current vector magnitude
        double magnitude = Math.Sqrt((velocity.x * velocity.x) + (velocity.y * velocity.y));

        // Check if the input is up/forwards (or up/forwards and turning)
        if((Math.Abs(Math.Round(input.x, 1)) == 0.7 && Math.Round(input.y, 1) == 0.7) || input.y == 1) {
            // Show the ship exhaust 
            playerShip.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            // Change the player's velocity vectors (only if vector magnitude < maxSpeed)
            if (magnitude < GameData.playerUpgrades[MaxSpeed]){
                velocity.x -= (float)(input.y * Math.Sin(rigidBody.rotation/57.2958) * SPEED_FACTOR); // SOH(CAH)TOA (Convert to radians)
                velocity.y += (float)(input.y * Math.Cos(rigidBody.rotation/57.2958) * SPEED_FACTOR); // (SOH)CAHTOA (Convert to radians)
            }
            // Set the rigidbody's new velocity
            rigidBody.velocity = velocity;
        } else {
            // Hide the ship exhaust
            playerShip.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update() {
        // Only move if playerMovement is allowed
        if (canMove && !PauseController.GamePaused) Move(currentInput);
    }

    // Method to read in the input from Unity's 'InputAction' package
    public void onMove(InputAction.CallbackContext context) {
        currentInput = context.ReadValue<Vector2>();
        // Update();
    }
    
}
