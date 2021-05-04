using UnityEngine;
// Custom imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Cannons : MonoBehaviour {
    // SerializeFields for the ship cannons
    [SerializeField] Transform leftCannon;
    [SerializeField] Transform rightCannon;
    // SerializeField for the laser object and the fireRate
    [SerializeField] GameObject laserPrefab;
    // limit the range of the fire rate input
    [Range(0.1f,1.0f)][SerializeField] float fireRate = 0.3f;
    // Private value for the side the laser last fired from, and for the currentInput
    private bool side = true, 
        toFire = false,
        canFire = true;

    // Update is called once per frame
    private IEnumerator FireCannons() {
        // Disable further firing
        canFire = false;
        // Fire on the currently active side
        if (side) {
            Instantiate(laserPrefab, leftCannon.position, leftCannon.rotation);
        } else {
            Instantiate(laserPrefab, rightCannon.position, rightCannon.rotation);
        }
        // Flip the 'side'
        side = !side;

        // Wait for the fireRate limiter
        yield return new WaitForSeconds(fireRate);
        // Enable firing
        canFire = true;
    }

    private void Update() {
        // Only fire if toFire online
        if (toFire && canFire) StartCoroutine(FireCannons());
    }

    // Method to read in the boolean input from Unity's 'InputAction' fire command
    public void onFire(InputAction.CallbackContext context) {
        // Get a boolean value from the input context
        toFire = (context.ReadValue<float>() != 0) ? true : false;
    }

}
