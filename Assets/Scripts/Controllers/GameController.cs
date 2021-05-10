using UnityEngine;

public class GameController : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {

        // Enable movement and cannons
        Movement.canMove = Cannons.canFire = true;

    }


}
