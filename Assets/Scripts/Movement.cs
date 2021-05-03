using UnityEngine;
// Custom imports
using System;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    [SerializeField] private float runspeed;
    private Rigidbody2D rigidBody;
    private Vector2 currentInput;
    private Vector2 velocity;

    // Speed modifiers (Constants)
    private int MAX_SPEED = 24;
    private double SPEED_FACTOR = 0.08;

    // Method called when class initialized
    private void Awake() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Method to move the player model
    private void Move(Vector2 input) {
        // Get current rigidbody velocity
        velocity = rigidBody.velocity;
        
        // Set the rigidbody's new rotation
        rigidBody.rotation -= (float)(input.x * 0.7 * runspeed);
        // Get the current vector magnitude
        double magnitude = Math.Sqrt((velocity.x * velocity.x) + (velocity.y * velocity.y));

        // Show the ship exhaust (if input.y == 1 (up/forwards) OR the rounded absolute value of input.x == 0.7 (combo of up/forward and left/right))
        if (input.y == 1 || Math.Abs(Math.Round(input.x, 1)) == 0.7) {
            this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
        }
        // Change the player's velocity vectors (only if vector magnitude < MAX_SPEED AND input != -1 (down/backwards))
        if(input.y != -1) {
            // Update the movement vector
            if (magnitude < MAX_SPEED){
                velocity.x -= (float)(input.y * Math.Sin(rigidBody.rotation/57.2958) * SPEED_FACTOR) * runspeed; // SOH(CAH)TOA (Convert to radians)
                velocity.y += (float)(input.y * Math.Cos(rigidBody.rotation/57.2958) * SPEED_FACTOR) * runspeed; // (SOH)CAHTOA (Convert to radians)
            }
            // Set the rigidbody's new velocity
            rigidBody.velocity = velocity;
        }
        // Hide the ship exhaust
        if(input.y != 1 && Math.Abs(Math.Round(input.x, 1)) != 0.7){
            this.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update() {
        Move(currentInput);
    }

    // Method to read in the input from Unity's 'InputAction' package
    public void onMove(InputAction.CallbackContext context) {
        currentInput = context.ReadValue<Vector2>();
        Update();
    }
    
}
