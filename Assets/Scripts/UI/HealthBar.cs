// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {

    // Two slider values for the health & shield bars
    [SerializeField] private Slider healthBar, shieldBar;
    [SerializeField] private TextMeshProUGUI healthText, shieldText;

    // Maximum values for the player health and shield
    private int maxHealth, maxShield;

    // Methods to Set the health and shield values
    public void SetHealth(int health) {
        if (health < 0) health = 0;
        healthText.text = $"{(healthBar.value = health)}/{(maxHealth)}";
    }
    public void SetShield(int level) {
        if (level < 0) level = 0;
        shieldText.text = $"{(shieldBar.value = level)}/{(maxShield)}";
    }

    // Methods to Set the maxHealth and MaxShield values
    public void SetMaxHealth(int health) {
        maxHealth = health;
    }
    public void SetMaxShield(int level) {
        maxShield = level;
    }

    public float GetHealth() {
        return healthBar.value;
    }
    public float GetShield() {
        return shieldBar.value;
    }

}
