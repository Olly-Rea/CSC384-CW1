using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPooledObject {

    // SerializeField for the asteroid game object and its starting speed
    [SerializeField] GameObject thisAsteroid;
    [SerializeField] int speed;

    // Public "health" variable
    public int health = 100;

    // Method to be called whenever the Object is spawned
    public void OnObjectSpawn() {
        // Get the asteroids rigidbody
        Rigidbody2D asteroidRigBod = thisAsteroid.GetComponent<Rigidbody2D>();
        // Set a random angular velocity to the asteroid
        asteroidRigBod.angularVelocity = Random.Range(-10, 10);
        // Give the asteroid a velocity
        asteroidRigBod.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    // Method to have the Asteroid take damage
    public void Damage(int damage) {
        // Check if the laser has destroyed the Asteroid
        if ((health -= damage) <= 0) {

            // Split the asteroid into smaller pieces

            


            // Increment the player score
            Score.Instance.Increment(10);

            // Despawn this asteroid
            Despawn();
            // Reset the Asteroid health
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
