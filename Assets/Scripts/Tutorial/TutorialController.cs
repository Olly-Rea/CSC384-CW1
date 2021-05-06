using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    // 1. Practice flying (Disable weapons and asteroids)
    // 2. Practice shooting (enable weapons)
    // 3. Practice run (enable some asteroids)

    // Queue to hold each stage of the Tutorial
    private Queue<GameObject> stages;

    // Serialized field to hold the nextButton
    [SerializeField] GameObject nextButton;

    // Start is called before the first frame update
    void Start() {
        // Initalise the stages Queue
        stages = new Queue<GameObject>();

        // Disable movement and cannons
        Movement.canMove = Cannons.canFire = false;

        // // Start the Dialogue
        // DialogueController.StartDialogue();
    }

}
