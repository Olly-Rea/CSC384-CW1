using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimator : MonoBehaviour {

    [SerializeField] Animator[] MenuAnimators;

    void Awake() {
        // Move all bar the first menu down
        for (int i = 1; i < MenuAnimators.Length; i++) {
            MenuAnimators[i].SetInteger("Transition", 1);
        }
    }

    // Methods to animate the transitions between Menus
    public void SlideDown(int menuIndex) {
        MenuAnimators[menuIndex].SetInteger("Transition", 1);
    }
    public void SlideRight(int menuIndex) {
        MenuAnimators[menuIndex].SetInteger("Transition", 2);
    }
    public void SlideUp(int menuIndex) {
        MenuAnimators[menuIndex].SetInteger("Transition", 3);
    }
    public void SlideLeft(int menuIndex) {
        MenuAnimators[menuIndex].SetInteger("Transition", 4);
    }

}
