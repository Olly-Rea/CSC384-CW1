using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Asteroid : MonoBehaviour, IPooledObject {

    // public variable for the asteroid game object and its starting speed
    public GameObject thisObject;
    protected GameObject thisAsteroid;

    // Serialized Field for the particle effect system
    [SerializeField] ParticleSystem destroyEffect;

    // Values for the asteroid speed
    public int speed;
    // Public "health" and score incrementer variables
    public int health;
    public int scoreIncrementer;

    // Private value to store the asteroids rigidbody
    protected Rigidbody2D asteroidRigBod;
    // Object pooler to be instantiated at Start()
    protected ObjectPooler objectPooler;

    // Method to spawn an asteroid from he objectpooler
    public abstract void OnObjectSpawn();

    // Method to have the Asteroid take damage
    public abstract void Damage(int damage);

    // Method to despawn the asteroid
    public void Despawn() {
        StartCoroutine(DestroyAsteroid());
        // Decrease the active asteroids counter in the AsteroidField
        AsteroidField.activeAsteroids--;
    }

    // Method to perform the visual stuff of "destroying" an asteroid
    private IEnumerator DestroyAsteroid() {
        // Get the asteroid component of the gameobject (if null)
        if (thisAsteroid == null) thisAsteroid = thisObject.transform.GetChild(0).gameObject;

        // Play the "poof" effect
        destroyEffect.Play();
        // Briefly disable the asteroid component
        thisAsteroid.SetActive(false);

        // Wait a second
        yield return new WaitForSeconds(1f);

        // Set the 'active' attribute of the GameObject to false 
        thisObject.SetActive(false);
        // re-enable the rigidbody and revert the asteroid colour back
        thisAsteroid.SetActive(true);
    }

}
