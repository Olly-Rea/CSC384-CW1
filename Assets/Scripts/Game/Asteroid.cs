using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    // Public "health" variable
    public int health = 100;

    // Method to have the Asteroid take damage
    public void Damage(int damage) {
        // Check if the laser has destroyed the Asteroid
        if ((health -= damage) <= 0) {

            // Split the asteroid into smaller pieces

        }
    }


}
