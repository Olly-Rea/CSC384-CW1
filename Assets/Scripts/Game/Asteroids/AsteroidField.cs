using System.Collections;
using UnityEngine;

public class AsteroidField : MonoBehaviour {

    // Public static values for number of currently active asteroids and max number of asteroids to have
    public static int activeAsteroids;
    public static int maxActive;

    // SerializeField for the maximum asteroids (from Unity Editor input)
    [SerializeField] int maxActiveInput;
    // SerializeField for the player GameObject
    [SerializeField] GameObject player;
    
    // Max distance from player before despawning Asteroid
    private const int MAX_DISTANCE = 40;

    // Instantiate the object pooler at Start()
    ObjectPooler objectPooler;

    void Start() {
        objectPooler = ObjectPooler.Instance;
        // Initialise the asteroid counter attributes
        activeAsteroids = 0;
        maxActive = maxActiveInput;
        // Run the check to see how far each asteroid is from the player
        StartCoroutine(CheckDistanceFromPlayer("LargeAsteroid"));
        StartCoroutine(CheckDistanceFromPlayer("SmallAsteroid"));
        // Call on the function to increase the difficulty over time
        StartCoroutine(IncreaseDifficulty());
    }

    private void Update() {
        // Check to ensure that if there are less than 20 active asteroids
        if (activeAsteroids < maxActive) {
            // Spawn a new Asteroid
            SpawnAsteroid();
            activeAsteroids++;
        }
    }

    // Method to spawn an Asteroid (dependent on player location)
    private void SpawnAsteroid() {
        // Get the players current position
        Vector2 playerPosition = player.transform.position;

        // Set the x and y ordinates of the asteroid to spawn
        int distanceX, distanceY;
        // Ensure one of the axis is the bufferZone distance away from the player
        if (Random.value > 0.5f) {
            distanceX = Random.Range(GameController.bufferZone, (int)(MAX_DISTANCE*0.8));
            distanceY = Random.Range(0, (int)(MAX_DISTANCE*0.8));
        } else {
            distanceX = Random.Range(0, (int)(MAX_DISTANCE*0.8));
            distanceY = Random.Range(GameController.bufferZone, (int)(MAX_DISTANCE*0.8));
        }

        // Create the spawnPosition to use
        Vector2 spawnPosition = new Vector2(
            playerPosition.x + ((Random.value > 0.5f) ? distanceX : -distanceX), 
            playerPosition.y + ((Random.value > 0.5f) ? distanceY : -distanceY)
        );
        // Spawn the asteroid
        GameObject asteroid = objectPooler.spawnFromPool("LargeAsteroid", spawnPosition, Quaternion.identity);
        // Set the asteroid speed
        asteroid.GetComponent<Asteroid>().speed = GameController.asteroidSpeed;
    }

    // Method to loop through all active asteroids and check distance from the player
    IEnumerator CheckDistanceFromPlayer(string pool) {
        // wait half a second before calling the check
        yield return new WaitForSeconds(0.5f);
        foreach(GameObject asteroid in objectPooler.getPool(pool)) {
            // Check that the asteroid is active first
            if (asteroid.activeSelf) {
                // Get the distance of the asteroid from the player
                float distance = Vector2.Distance(player.transform.position, asteroid.transform.position);
                // Check the if the distance is greater than the buffer zone AND the maximum distance
                if (distance > GameController.bufferZone * 2 && distance > MAX_DISTANCE) {
                    // Despawn the asteroid
                    asteroid.GetComponent<Asteroid>().Despawn();
                    // Decrement the number of active asteroids
                    activeAsteroids--;
                }
            }
        }
        StartCoroutine(CheckDistanceFromPlayer(pool));
    }

    // Method to increase the speed and explosion force of asteroids over time
    IEnumerator IncreaseDifficulty() {
        // Wait 1 minute between each increment
        yield return new WaitForSeconds(60f);
        GameController.asteroidSpeed += 2;
        GameController.explosiveForce += 1;
        // And repeat
        StartCoroutine(IncreaseDifficulty());
    }

}
