using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveLoadSlot : MonoBehaviour {

    // Serialized field to state if the slot allows saving
    [SerializeField] bool forSave;
    // Serialized field for the slot number
    [SerializeField] int slotNum;

    // Misc SerializeFields depending on Slot UI location
    [SerializeField] GameObject confirmPrompt;
    [SerializeField] GameObject deleteBtn;

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
            thisText.text = SaveLoadController.GetSaveDateTime(slotNum);
            if(forSave) {
                thisButton.onClick.AddListener(CheckSave);
            } else {
                // Add the loading onClick functionlity to the SaveSlot
                thisButton.onClick.AddListener(LoadFromSlot);
                // Show the deleteBtn
                if (deleteBtn != null) {
                    deleteBtn.SetActive(true);
                }
            }
        // Otherwise check to see if the button allows saving
        } else {
            if(forSave) {
                // Add the saving onClick functionlity to the SaveSlot
                thisButton.onClick.AddListener(CheckSave);
            } else {
                // Make the button un-interactable
                thisButton.interactable = false;
                GetComponent<CanvasGroup>().alpha = 0.7f;
            }
        }
    }

    // Method to check a save slot
    void CheckSave() {
        // Clear any previous listeners from the confirm prompt
        confirmPrompt.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        // Check if a file already exists
        if (File.Exists(slotPath)) {
            // update and show the confirm prompt
            confirmPrompt.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Overwrite save data in Slot {(slotNum)}?";
            confirmPrompt.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(OverwriteSlot);
            confirmPrompt.SetActive(true);
        } else {
            // update and show the confirm prompt
            confirmPrompt.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Save data in Slot {(slotNum)}?";
            confirmPrompt.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(SaveToSlot);
            confirmPrompt.SetActive(true);
        }
    }

    // Methods to Save/Load to slot
    void LoadFromSlot() {
        GameController.LoadGame(slotNum);
    }
    void SaveToSlot() {
        // Save the game to this slot
        GameController.SaveGame(slotNum);
        // Update the text 
        thisText.text = SaveLoadController.GetSaveDateTime(slotNum);
    }

    // Method to show the confirm prompt (if one present)
    void OverwriteSlot() {
        ClearSlot();
        // Save the game to this slot
        GameController.SaveGame(slotNum);
        // Clear the confirm prompt
        confirmPrompt.SetActive(false);
        // Update the text 
        thisText.text = SaveLoadController.GetSaveDateTime(slotNum);
    }

    // Method to clear the save data from this slot
    public void ClearSlot() {
        File.Delete(slotPath);
        thisText.text = "(Empty)";
        // Hide the deleteBtn
        if (deleteBtn != null) {
            // Make the button un-interactable
            thisButton.interactable = false;
            GetComponent<CanvasGroup>().alpha = 0.7f;
            deleteBtn.SetActive(false);
        }
    }

}
