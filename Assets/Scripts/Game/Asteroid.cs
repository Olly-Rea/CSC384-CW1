using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPooledObject {

    // SerializeField for the asteroid game object
    [SerializeField] GameObject thisAsteroid;
    // Public "health" variable
    public int health = 100;

    // Method to be called whenever the Object is spawned
    public void OnObjectSpawn() {
        
        Debug.Log("Spawned!");

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
    private void Despawn() {
        // Set the 'active' attribute of the GameObject to false 
        thisAsteroid.SetActive(false);
    }

}
