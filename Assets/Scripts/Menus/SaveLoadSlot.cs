using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveLoadSlot : MonoBehaviour {

    // Serialized field to state if the slot allows saving
    [SerializeField] bool allowSave;
    // Serialized field for the slot number
    [SerializeField] int slotNum;

    // Value to hold the slot path
    string slotPath;

    // Variables to hold the Slot button and TextMeshPro components
    Button thisButton;
    TextMeshProUGUI thisText;
    
    // Start is called before the first frame update
    void Start() {
        // Get the button component from the slot
        thisButton = GetComponent<Button>();
        // Get the TextMeshPro element for the Empty/Full slot indicator
        thisText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        // Set the slotPath
        slotPath = SaveLoadController.GetPath(slotNum);

        // If a file exists in this slot
        if (File.Exists(slotPath)) {
            // Make the button interactable
            thisButton.interactable = true;
            GetComponent<CanvasGroup>().alpha = 1f;
            thisText.text = SaveLoadController.getSaveCreatedDate(slotNum);
            // Add the loading onClick functionlity to the SaveSlot
            thisButton.onClick.AddListener(LoadFromSlot);
        // Otherwise check to see if the button allows saving
        } else {
            if(allowSave) {
                // Add the saving onClick functionlity to the SaveSlot
                thisButton.onClick.AddListener(SaveToSlot);
            } else {
                // Make the button un-interactable
                thisButton.interactable = false;
                GetComponent<CanvasGroup>().alpha = 0.7f;
            }
        }

    }

    // Methods to Save/Load to slot
    void LoadFromSlot() {
        GameController.LoadGame(slotNum);
    }
    void SaveToSlot() {
        // Check if a file already exists
        if (File.Exists(slotPath)) {
            // Prompt overwrite

            Debug.Log("Overwrite??");

        } else {
            GameController.SaveGame(slotNum);
        }
    }

    // Method to clear the save data from this slot
    void ClearSlot() {

    }

}
