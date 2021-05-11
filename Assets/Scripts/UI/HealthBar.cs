using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {

    // Two slider values for the health & shield bars
    [SerializeField] private Slider healthBar, shieldBar;
    [SerializeField] private TextMeshProUGUI healthText, shieldText;

    // Maximum values for the player health and shield
    private int maxHealth, maxShield;

    // Initialise an instance of the Health bar at start
    public static HealthBar Instance;
    void Start() {
        Instance = this;
        // Initialise the shield/health values
        maxShield = 100;
        maxHealth = 40;

        if (GameController.saveData != null) {
            SetShield(GameController.saveData.playerShield);
            SetHealth(GameController.saveData.playerHealth);
        }

        // Start the coroutine to replenish the shield
        StartCoroutine(ReplenishShield());
    }

    // Methods to Set the health and shield values
    public void SetHealth(float health) {
        if (health < 0) health = 0;
        healthText.text = $"{(healthBar.value = health)}/{(maxHealth)}";
    }
    public void SetShield(float level) {
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

    // Methods to get the current values of the health and shield bars
    public float GetHealth() {
        return healthBar.value;
    }
    public float GetShield() {
        return shieldBar.value;
    }

    // Method to replenish a users shield every 2 seconds
    IEnumerator ReplenishShield() {
        float shieldVal = GetShield();
        if (shieldVal < maxShield) SetShield(shieldVal+=1f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ReplenishShield());
    }

}
