using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {

    // Public static value for the number of active asteroids
    public static int activeAsteroids = 0;
    // SerializeFields for the player GameObject and the asteroid prefabs to use
    [SerializeField] GameObject player;
    [SerializeField] int maxActive = 32;

    // Buffer zone radius to use when spawning in new Asteroids (could decrease over time as game gets harder?)
    private int bufferZone = 8;
    // Max distance from player before despawning Asteroid
    private const int MAX_DISTANCE = 1000;

    // Instantiate the object pooler at Start()
    ObjectPooler objectPooler;
    private void Start() {
        objectPooler = ObjectPooler.Instance;
        activeAsteroids = 0;
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
        Vector2 spawnPosition = player.transform.position;

        // Set the x ordinate of the asteroid to spawn
        int distanceX = Random.Range(bufferZone, bufferZone * 8);
        spawnPosition.x += (Random.Range(1, 10) >= 5) ? distanceX : -distanceX;
        // Set the y ordinate of the asteroid to spawn
        int distanceY = Random.Range(bufferZone, bufferZone * 8);
        spawnPosition.y += (Random.Range(1, 10) >= 5) ? distanceY : -distanceY;

        // Spawn the asteroid
        objectPooler.spawnFromPool("LargeAsteroid", spawnPosition, Quaternion.identity);
    }

}
