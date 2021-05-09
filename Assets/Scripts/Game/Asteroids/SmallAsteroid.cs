using UnityEngine;

public class SmallAsteroid : Asteroid {

    // Instantiate the object pooler at Start()
    private void Start() {
        // Get the cameraShake component from the Main Camera
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        // Setup asteroid atributes
        objectPooler = ObjectPooler.Instance;
        health = 60;
    }

    // Method to be called whenever the Object is spawned
    override public void OnObjectSpawn() {
        // Get the asteroids rigidbody (if null)
        if (asteroidRigBod == null) asteroidRigBod = transform.GetComponent<Rigidbody2D>();
        // Set a random angular velocity to the asteroid
        asteroidRigBod.angularVelocity = Random.Range(-10, 10);
        // Initialise the speed value
        speed = AsteroidField.explosiveForce;
        // Give the asteroid a velocity
        asteroidRigBod.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    // Method to have the Asteroid take damage
    override public void Damage(int damage) {
        // Check if the laser has destroyed the Asteroid
        if ((health -= damage) <= 0) {
            // Shake the player camera
            StartCoroutine(cameraShake.shakeCamera(0.04f, 0.04f));
            // Increment the player score
            Score.Instance.Increment(scoreIncrementer);
            // Despawn this asteroid
            Despawn();
            // Reset the Asteroid health (for when it is respawned)
            health = 100;
        }
    }

}
