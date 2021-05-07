using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPooledObject {

    // SerializeField for the asteroid game object and its starting speed
    [SerializeField] GameObject thisAsteroid;
    // Values for the asteroid speed and the force with which it explodes (Public for difficulty settings)
    public int speed;
    public int explosiveForce;
    // Public "health" variable
    public int health = 100;
    // Private value to store the asteroids rigidbody
    private Rigidbody2D asteroidRigBod;

    // Instantiate the object pooler at Start()
    ObjectPooler objectPooler;
    private void Start() {
        objectPooler = ObjectPooler.Instance;
    }

    // Method to be called whenever the Object is spawned
    public void OnObjectSpawn() {
        // Get the asteroids rigidbody
        asteroidRigBod = thisAsteroid.GetComponent<Rigidbody2D>();
        // Set a random angular velocity to the asteroid
        asteroidRigBod.angularVelocity = Random.Range(-10, 10);
        // Initialise the speed value
        speed = AsteroidField.maxSpeed;
        // Give the asteroid a velocity
        asteroidRigBod.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    // Method to have the Asteroid take damage
    public void Damage(int damage) {
        // Check if the laser has destroyed the Asteroid
        if ((health -= damage) <= 0) {
            // Split the asteroid into smaller pieces
            for (int i = 0; i < 3; i++) {
                GameObject asteroid = objectPooler.spawnFromPool("SmallAsteroid", asteroidRigBod.transform.position, Quaternion.identity);
                // Set the speed of the smaller asteroids
                asteroid.GetComponent<Asteroid>().speed = explosiveForce;
            }
            // Increment the player score
            Score.Instance.Increment(10);
            // Despawn this asteroid
            Despawn();
            // Reset the Asteroid health (for when it is respawned)
            health = 100;
        }
    }

    // Method to despawn the asteroid
    public void Despawn() {
        // Set the 'active' attribute of the GameObject to false 
        thisAsteroid.SetActive(false);
        AsteroidField.activeAsteroids--;
    }

}
