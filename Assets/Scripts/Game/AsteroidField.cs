using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {

    // Public static values for number of currently active asteroids and max number of asteroids to have
    public static int activeAsteroids = 0;
    public static int maxActive;
    // Unity Input field for the above
    [SerializeField] int maxActiveInput;

    // SerializeField for the player GameObject
    [SerializeField] GameObject player;
    
    // Buffer zone radius to use when spawning in new Asteroids
    private const int BUFFER_ZONE = 16;
    // Max distance from player before despawning Asteroid
    private const int MAX_DISTANCE = 40;

    // Instantiate the object pooler at Start()
    ObjectPooler objectPooler;
    private void Start() {
        objectPooler = ObjectPooler.Instance;
        // Initialise the max active asteroids attribute
        maxActive = maxActiveInput;
        // Run the check to see how far each asteroid is from the player
        StartCoroutine(checkDistanceFromPlayer());
    }

    private void Update() {
        // Check to ensure that if there are less than 20 active asteroids
        if (activeAsteroids < maxActive) {
            // Spawn a new Asteroid
            spawnAsteroid();
            activeAsteroids++;
        }
    }

    // Method to spawn an Asteroid (dependent on player location)
    private void spawnAsteroid() {
        // Get the players current position
        Vector2 playerPosition = player.transform.position;

        // Set the x and y ordinates of the asteroid to spawn
        int distanceX, distanceY;
        // Ensure one of the axis is the BUFFER_ZONE distance away from the player
        if (Random.Range(1, 10) >= 5) {
            distanceX = Random.Range(BUFFER_ZONE, (int)(MAX_DISTANCE*0.8));
            distanceY = Random.Range(0, (int)(MAX_DISTANCE*0.8));
        } else {
            distanceX = Random.Range(0, (int)(MAX_DISTANCE*0.8));
            distanceY = Random.Range(BUFFER_ZONE, (int)(MAX_DISTANCE*0.8));
        }

        // Create the spawnPosition to use
        Vector2 spawnPosition = new Vector2(
            playerPosition.x + ((Random.Range(1, 10) >= 5) ? distanceX : -distanceX), 
            playerPosition.y + ((Random.Range(1, 10) >= 5) ? distanceY : -distanceY)
        );
        // Spawn the asteroid
        objectPooler.spawnFromPool("LargeAsteroid", spawnPosition, Quaternion.identity);
    }

    // Method to loop through all active asteroids and check distance from the player
    IEnumerator checkDistanceFromPlayer() {
        foreach(GameObject asteroid in objectPooler.getPool("LargeAsteroid")) {
            // Check that the asteroid is active first
            if (asteroid.activeSelf) {
                // Get the distance of the asteroid from the player
                float distance = Vector2.Distance(player.transform.position, asteroid.transform.position);
                // Check the if the distance is greater than the buffer zone AND the maximum distance
                if (distance > BUFFER_ZONE * 2 && distance > MAX_DISTANCE) {
                    // Despawn the asteroid
                    asteroid.GetComponent<Asteroid>().Despawn();
                    // Decrement the number of active asteroids
                    activeAsteroids--;
                }
            }
        }
        // wait a second before calling the check again
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(checkDistanceFromPlayer());
    }

}
