using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour {

    public void showMenu() {
        // Pause the game
        PauseController.Pause();
        // Show the menu
        transform.gameObject.SetActive(true);
    }

}
