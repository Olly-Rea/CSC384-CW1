using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    // Cannon attributes
    public int clipSize;
    
    // Public value to indicate the player is dead
    public static bool isDead;

    // Serialize field to take a reference to the HealthBar script
    [SerializeField] HealthBar healthBar;
    // Serialized Field for the particle effect system
    [SerializeField] ParticleSystem destroyEffect;

    // Private variable to hold the player ship and player rigidbody
    private GameObject playerShip;
    private Rigidbody2D playerRigBod;

    // UnityEvent to broadcast player death
    public static UnityEvent playerDeath;
    // Serialized field to get the deathMenu gameObject (if not in tutorial)
    [SerializeField] GameObject deathMenu;

    // Method called on script initialisation
    void Start() {
        // Initialise values for the player
        clipSize = 200;

        // Set the "playerShip" and "playerRigBod"
        playerShip = transform.GetChild(0).gameObject;
        playerRigBod = transform.GetComponent<Rigidbody2D>();

        // Initialise the playerDeath event
        playerDeath = new UnityEvent();
        // Add the Player death method to the playerDeath event
        playerDeath.AddListener(DeathListener);
    }

    // Method triggered when a laser hits an asteroid
    private void OnCollisionEnter2D(Collision2D collision) {
        // Check the healthBar existence (tutorial or not)
        if (healthBar != null) {
            // Create a "damage" value to hold the damage dealt by the collision
            int collisionDamage = (int) System.Math.Round(collision.relativeVelocity.magnitude * GameController.damageFactor, 1);
            
            // Get teh current shield level and player health from the HealthBar
            int shieldLevel = (int)healthBar.GetShield();
            int playerHealth = (int)healthBar.GetHealth();

            // Check if the player still has a shield
            if (shieldLevel > 0) {
                // if so, deal shield damage
                healthBar.SetShield(shieldLevel-=collisionDamage);
            } else {
                // if not, deal health damage
                healthBar.SetHealth(playerHealth-=collisionDamage);
            }
            // Invoke the "playerDeath" Event if playerHealth hits 0...
            if (playerHealth <= 0) playerDeath.Invoke();
        }
    }
    
    // Method to pass the coroutine into the event listener
    private void DeathListener() {
        StartCoroutine(Death());
    }

    // Method to handle player "death"
    private IEnumerator Death() {
        // Double the drag of the player to stop them
        playerRigBod.drag = 1.4f;
        // Disable further movement and firing
        Movement.canMove = Cannons.canFire = false;

        GameController.playerAlive = false;

        // Play the "on destroy" effect
        destroyEffect.Play();
        // Deactivate/Hide the playerShip
        playerShip.SetActive(false);

        // Wait a short period
        yield return new WaitForSeconds(1f);
        // Display the ENDGAME menu
        deathMenu.SetActive(true);
    }

}
