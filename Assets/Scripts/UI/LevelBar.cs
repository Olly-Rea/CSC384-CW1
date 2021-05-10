using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBar : MonoBehaviour {
    
    // Serialize field for the upgrade type this bar is for
    [SerializeField] Upgradeable upgradeType;
    // Serialize field for the color to fill the bar with
    [SerializeField] Color fillColor;
    // Serialize field to hold the starting value of the level bar
    [SerializeField] int startingValue;

    // Public value to hold the textmeshPro "cost" value
    public TextMeshProUGUI costText;

    // Private variable to hold the value and max value of the bar
    private int value, maxValue;

    // Private variable to hold each of the bar segments
    private GameObject[] bars;
    private GameObject incrementer;

    // Private cached instance of the UpgradeController
    private UpgradeController upgradeController;

    // Initialise the LevelBars values
    void Awake() {
        // Assign the UpgradeController
        upgradeController = UpgradeController.Instance;
        // Set the starting values of the level bar
        value = startingValue;
        maxValue = transform.childCount;
        // Initialise the bars array
        bars = new GameObject[maxValue];
        for(int i = 0; i < maxValue; i++) {
            bars[i] = transform.GetChild(i).gameObject;
        }
        // Get the incrementer button
        incrementer = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject;
        // Get the costText  text element
        costText = transform.parent.GetChild(transform.parent.childCount-1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Method to display a "level up" (increment the level bar)
    public void LevelUp() {
        if (upgradeController.PurchaseUpgrade(upgradeType)) {
            bars[value++].GetComponent<Image>().color = fillColor;
        }
    }

    // Methods to "Enable/Disable" the LevelBar
    public void Enable() {
        incrementer.GetComponent<Button>().interactable = true;
        incrementer.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        // Set text color to green
        costText.color = new Color(0.5f, 0.66f, 0.5f, 1f);
    }
    public void Disable() {
        incrementer.GetComponent<Button>().interactable = false;
        incrementer.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        // Set text color to red
        costText.color = new Color(0.8f, 0.5f, 0.5f, 1f);
    }

    // Method to set the cost text of the LevelBar
    public void SetCost(int newCost) {
        costText.text = "COST: " + $"{(newCost):000}";
    }

}
