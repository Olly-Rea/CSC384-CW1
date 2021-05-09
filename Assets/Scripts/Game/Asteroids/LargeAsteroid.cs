using System.Collections;
using UnityEngine;

public class LargeAsteroid : Asteroid {

    // Variable specific to a large asteroid indicating the force with which it explodes (Public for difficulty settings)
    public static int explosiveForce;

    // Instantiate the object pooler at Start()
    private void Start() {
        // Get the cameraShake component from the Main Camera
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        // Setup asteroid atributes
        objectPooler = ObjectPooler.Instance;
        health = 100;
    }

    // Method to be called whenever the Object is spawned
    override public void OnObjectSpawn() {
        // Get the asteroids rigidbody (if null)
        if (asteroidRigBod == null) asteroidRigBod = transform.GetComponent<Rigidbody2D>();
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
            // Shake the player camera
            StartCoroutine(cameraShake.shakeCamera(0.08f, 0.08f));
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
            // Set a position for the asteroid
            Vector2 position = new Vector2(
                asteroidRigBod.transform.position.x + Random.Range(-1f, 1f),
                asteroidRigBod.transform.position.y + Random.Range(-1f, 1f)
            );
            GameObject asteroid = objectPooler.spawnFromPool("SmallAsteroid", position, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
