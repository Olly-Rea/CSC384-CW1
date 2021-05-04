using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour {
    // Laser speed and RigidBody2D
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D laserRigBod;

    private Vector2 velocity;

    // Instantiate the Laser
    void Start() {
        // Calculate the velocity vector for the laser
        velocity.x = -(float)(speed * Math.Sin(laserRigBod.rotation / 57.2958)); // SOH(CAH)TOA (Convert to radians)
        velocity.y = (float)(speed * Math.Cos(laserRigBod.rotation / 57.2958)); // (SOH)CAHTOA (Convert to radians)
        // Set the velocity of the laser
        laserRigBod.velocity = velocity;
    }

    private void Update() {
        // If the laser exits the view window
        if (transform.right.x > Screen.width*2 || transform.right.y > Screen.height*2)
            Destroy(this.gameObject, 1);
    }

    // Method triggered when a laser hits an asteroid
    void OnTriggerEnter2D(Collider2D asteroid) {
        
        // DEBUG
        Debug.Log(asteroid.name);

        // Destroy this laser
        Destroy(this.gameObject, 0.1f);

        // Enemy enemy = asteroid.GetComponent<Enemy>();
    }

}
