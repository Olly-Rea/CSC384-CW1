using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Cannon level
    public int cannonLvl;
    // Cannon "clip" level
    public int clipLvl;
    // Thrust level
    public int thrustLvl;
    // Turning level
    public int turnLvl;
    // Shield level
    public int shieldLevel;
    // Player health
    public int playerHealth;

    // Method triggered when a laser hits an asteroid
    void OnTriggerEnter2D(Collider2D hitObject) {

        
        Debug.Log("Player hurt!");

        playerHealth -= 1;
        
    }
    

}
