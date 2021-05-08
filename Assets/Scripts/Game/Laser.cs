using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour, IPooledObject {
    // SerializeFields for the laser speed, game object and damage inflicted
    [SerializeField] float speed;
    [SerializeField] GameObject thisLaser;
    [SerializeField] int damage;
    // Private values for the laser RigidBody2D and velocity
    private Rigidbody2D laserRigBod;
    private Vector2 velocity;

    // Get the rigidbody component from the game object on creation
    private void Awake() {
        laserRigBod = thisLaser.GetComponent<Rigidbody2D>();
    }

    // Method to be called whenever the Object is spawned
    public void OnObjectSpawn() {
        // Set the rigidBody rotation
        laserRigBod.rotation = this.transform.rotation.eulerAngles.z;
        // Calculate the velocity vector for the laser
        velocity.x = -(float)(speed * Math.Sin(laserRigBod.rotation / 57.2958)); // SOH(CAH)TOA (Convert to radians)
        velocity.y = (float)(speed * Math.Cos(laserRigBod.rotation / 57.2958)); // (SOH)CAHTOA (Convert to radians)
        // Set the velocity of the laser
        laserRigBod.velocity = velocity;
        // Despawn the laser again (after 1(ish) second)
        StartCoroutine(Despawn(1.0f));
    }

    // private void Update() {
    //     // If the laser exits the view window
    //     if (transform.right.x > Screen.width*2 || transform.right.y > Screen.height*2)
    //         Destroy(this.gameObject, 1);
    // }

    // Method triggered when a laser hits an asteroid
    void OnTriggerEnter2D(Collider2D hitObject) {
        // Get the asteroid parent object
        GameObject asteroidObject = hitObject.transform.parent.gameObject;
        // Get the asteroid that has been hit
        Asteroid asteroid = (asteroidObject.GetComponent<LargeAsteroid>() != null) 
            ? (Asteroid) asteroidObject.GetComponent<LargeAsteroid>() 
            : (Asteroid) asteroidObject.GetComponent<SmallAsteroid>();

        // If the asteroid is not null
        if (asteroid != null) {
            // Inflict damage on the asteroid
            asteroid.Damage(damage);

            // DEBUG
            Debug.Log("Name: " + asteroid.name + ", Health: " + asteroid.health);

        }
        // Despawn the laser
        StartCoroutine(Despawn(0f));
    }

    // Method to despawn the laser after 3 seconds
    private IEnumerator Despawn(float time) {
        // Wait for the fireRate limiter
        yield return new WaitForSeconds(time);
        // Set the 'active' attribute of the GameObject to false 
        thisLaser.SetActive(false);
        // Reset the velocity of the laserRigBod
        laserRigBod.velocity = Vector2.zero;
    }

}
