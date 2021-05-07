using UnityEngine;
// Custom imports
using System;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    // Public static value to determine if the player can move
    public static bool canMove = true;

    [SerializeField] int maxSpeed = 20;
    [SerializeField] double turnSpeed = 0.7;

    private Rigidbody2D rigidBody;
    private Vector2 currentInput;
    private Vector2 velocity;
    // Speed modifier (Constant)
    private const double SPEED_FACTOR = 0.08;

    // Method called when class initialized
    private void Awake() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
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
        rigidBody.rotation -= (float)(input.x * turnSpeed);
        // Get the current vector magnitude
        double magnitude = Math.Sqrt((velocity.x * velocity.x) + (velocity.y * velocity.y));

        // Check if the input is up/forwards (or up/forwards and turning)
        if((Math.Abs(Math.Round(input.x, 1)) == 0.7 && Math.Round(input.y, 1) == 0.7) || input.y == 1) {
            // Show the ship exhaust 
            this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            // Change the player's velocity vectors (only if vector magnitude < maxSpeed)
            if (magnitude < maxSpeed){
                velocity.x -= (float)(input.y * Math.Sin(rigidBody.rotation/57.2958) * SPEED_FACTOR); // SOH(CAH)TOA (Convert to radians)
                velocity.y += (float)(input.y * Math.Cos(rigidBody.rotation/57.2958) * SPEED_FACTOR); // (SOH)CAHTOA (Convert to radians)
            }
            // Set the rigidbody's new velocity
            rigidBody.velocity = velocity;
        } else {
            // Hide the ship exhaust
            this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update() {
        // Only move if playerMovement is allowed
        if (canMove) Move(currentInput);
    }

    // Method to read in the input from Unity's 'InputAction' package
    public void onMove(InputAction.CallbackContext context) {
        currentInput = context.ReadValue<Vector2>();
        // Update();
    }
    
}
