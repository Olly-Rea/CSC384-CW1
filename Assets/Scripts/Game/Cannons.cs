using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
// using the Upgradeable enums
using static Upgradeable;

public class Cannons : MonoBehaviour {

    // Public static value to determine if the player can fire the cannons
    public static bool canFire = true;

    // SerializeFields for the ship cannons
    [SerializeField] Transform leftCannon;
    [SerializeField] Transform rightCannon;
    // SerializeField for the laser object and the fireRate
    [SerializeField] GameObject laserPrefab;
    // Private values for the side the laser last fired from, and for the currentInput
    private bool side = true, toFire = false;

    // Instantiate the object pooler at Start()
    ObjectPooler objectPooler;
    void Start() {
        objectPooler = ObjectPooler.Instance;
    }

    // Enumerator to slow the fire rate of the cannons
    private IEnumerator FireCannons() {
        // Fire on the NEXT active side
        if (side = !side) {
            objectPooler.spawnFromPool("Laser", leftCannon.position, leftCannon.rotation);
        } else {
            objectPooler.spawnFromPool("Laser", rightCannon.position, rightCannon.rotation);
        }
        // Wait for the fireRate limiter
        yield return new WaitForSeconds(GameData.playerUpgrades[FireRate]);
        // Enable firing
        canFire = true;
    }

    private void Update() {
        // Only fire if toFire and canFire are true
        if (toFire && canFire) {
            // Disable further firing
            canFire = false;
            StartCoroutine(FireCannons());
        }
    }

    // Method to read in the boolean input from Unity's 'InputAction' fire command
    public void onFire(InputAction.CallbackContext context) {
        // Get a boolean value from the input context
        toFire = (context.ReadValueAsButton() && !PauseController.GamePaused) ? true : false;
    }

}
