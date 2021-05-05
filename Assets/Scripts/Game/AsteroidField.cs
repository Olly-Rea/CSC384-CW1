using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {

    public int activeAsteroids = 0;

    // Instantiate the object pooler at Start()
    ObjectPooler objectPooler;
    private void Start() {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update() {
        // Check to ensure 20 asteroids are in the Game at all times
        if (activeAsteroids < 20) {



        }
    }

}
