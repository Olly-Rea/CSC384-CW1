using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogueController : MonoBehaviour {

    // Serialized field to store the DialogueBox animator
    [SerializeField] Animator animator;

    // UnityEvent to broadcast dialogue events
    public static UnityEvent dialogueEvent;

    // Serialized field to store the sentence inputs (specified min and max Unity TextArea lines)
    [TextArea(3, 10)][SerializeField] string[] inputs;
    // Serialized field to hold the nextButton
    [SerializeField] GameObject nextButton;
    // Public static boolean
    public static bool waitForEvent;

    // Serialized field and private variable to hold TextMeshPro component
    [SerializeField] GameObject textObject;
    private TextMeshProUGUI dialogueText;

    // Private variable to hold the queue of dialogue sentences
    private Queue<string> sentences;

    // Create a reference for this DialogueController
    public static DialogueController Instance;
    private void Awake() {
        Instance = this;
    }

    // Method to initialise the Dialogue attributes
    void Start() {
        // Get the TextMeshPro component from the dialogue box
        dialogueText = textObject.GetComponent<TextMeshProUGUI>();
        // Populate the queue with the new sentences
        sentences = new Queue<string>();
        foreach(string sentence in inputs) {
            sentences.Enqueue(sentence);
        }
        // Initialise the UnityEvent handling
        waitForEvent = false;
        dialogueEvent = new UnityEvent();
        // Start the Dialogue
        StartDialogue();
    }

    // Method to start Dialogue
    public void StartDialogue() {
        // Show the dialogue box popup
        animator.SetBool("IsOpen", true);
        // Display the first sentence
        DisplayNextSentence();
    }

    // IEnumerator to give the dialogue a "typing" appearence
    IEnumerator AnimateSentence(string sentence) {
        // Hide the next button
        nextButton.SetActive(false);
        // Reset the dialogue text
        dialogueText.text = "";
        // Small wait before displaying next sentence (unscaled time)    
        yield return new WaitForSecondsRealtime(0.6f);
        foreach(char letter in sentence.ToCharArray()) {
            // Add the next character to the text
            dialogueText.text += letter;
            // Wait between each letter (unscaled time)
            yield return new WaitForSecondsRealtime(0.02f);
        }
        // Broadcast that this dialogue sentence has been output
        dialogueEvent.Invoke();
        // Show the next button again
        if (!waitForEvent) nextButton.SetActive(true);
    }

    // Method to return the next sentence
    public void DisplayNextSentence() {
        // Check if the dialogue has ended
        if (sentences.Count == 0) {
            CloseDialogue();
            return;
        }
        // Otherwise display the next sentence in the dialogue text
        StartCoroutine(AnimateSentence(sentences.Dequeue()));
    }

    // Method to add a new sentence to the dialogue
    public void addSentence(string input) {
        sentences.Enqueue(input);
    }

    // Method to close the dialogue
    void CloseDialogue() {
        // Set the animation to "close" the dialogue popup
        animator.SetBool("IsOpen", false);
    }

}
