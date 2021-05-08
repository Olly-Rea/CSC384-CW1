using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAsteroid : Asteroid {

    // Variable specific to a large asteroid indicating the force with which it explodes (Public for difficulty settings)
    public int explosiveForce;

    // Instantiate the object pooler at Start()
    private void Start() {
        objectPooler = ObjectPooler.Instance;
        health = 100;
    }

    // Method to be called whenever the Object is spawned
    override public void OnObjectSpawn() {
        // Get the asteroids rigidbody
        asteroidRigBod = thisObject.GetComponent<Rigidbody2D>();
        // Set a random angular velocity to the asteroid
        asteroidRigBod.angularVelocity = Random.Range(-10, 10);
        // Initialise the speed value
        speed = AsteroidField.maxSpeed;
        // Give the asteroid a velocity
        asteroidRigBod.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    // Method to have the Asteroid take damage
    override public void Damage(int damage) {
        // Check if the laser has destroyed the Asteroid
        if ((health -= damage) <= 0) {
            // Despawn this asteroid
            Despawn();
            // Split the asteroid into smaller pieces
            StartCoroutine(SpawnSmallAsteroids());
            // Increment the player score
            Score.Instance.Increment(scoreIncrementer);
            // Reset the Asteroid health (for when it is respawned)
            health = 100;
        }
    }

    // Method to wait before spawning follow-up small asteroids
    private IEnumerator SpawnSmallAsteroids() {
        // Wait a small period of time
        yield return new WaitForSeconds(0.1f);
        // Loop through and spawn new asteroids
        for (int i = 0; i < 3; i++) {
            GameObject asteroid = objectPooler.spawnFromPool("SmallAsteroid", asteroidRigBod.transform.position, Quaternion.identity);
            // Set the speed of the smaller asteroids
            asteroid.GetComponent<SmallAsteroid>().speed = explosiveForce;
        }
    }

}
