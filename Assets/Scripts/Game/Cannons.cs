using UnityEngine;
// Custom imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Cannons : MonoBehaviour {

    // Public static value to determine if the player can fire the cannons
    public static bool canFire = true;

    // SerializeFields for the ship cannons
    [SerializeField] Transform leftCannon;
    [SerializeField] Transform rightCannon;
    // SerializeField for the laser object and the fireRate
    [SerializeField] GameObject laserPrefab;
    // limit the range of the fire rate input
    [Range(0.1f,1.0f)][SerializeField] float fireRate = 0.3f;
    // Private values for the side the laser last fired from, and for the currentInput
    private bool side = true, 
        toFire = false;

    // Instantiate the object pooler at Start()
    ObjectPooler objectPooler;
    private void Start() {
        objectPooler = ObjectPooler.Instance;
    }

    // Enumerator to slow the fire rate of the cannons
    private IEnumerator FireCannons() {
        // Disable further firing
        canFire = false;
        // Fire on the NEXT active side
        if (side = !side) {
            objectPooler.spawnFromPool("Laser", leftCannon.position, leftCannon.rotation);
        } else {
            objectPooler.spawnFromPool("Laser", rightCannon.position, rightCannon.rotation);
        }
        // Wait for the fireRate limiter
        yield return new WaitForSeconds(fireRate);
        // Enable firing
        canFire = true;
    }

    private void Update() {
        // Only fire if toFire and canFire are true
        if (toFire && canFire) StartCoroutine(FireCannons());
    }

    // Method to read in the boolean input from Unity's 'InputAction' fire command
    public void onFire(InputAction.CallbackContext context) {
        // Allow firing if game is not paused
        if (!PauseController.GamePaused) {
            // Get a boolean value from the input context
            toFire = (context.ReadValueAsButton()) ? true : false;
        } else {
            toFire = false;
        }
    }

}
