using System.Collections;
using UnityEngine;
using TMPro;

public class DamageIndicator : MonoBehaviour, IPooledObject {
    
    private GameObject textObject;

    // private variable to hold the score and score text
    private TextMeshProUGUI damageText;
    private Animator textAnimator;

    // Get the rigidbody component from the game object on creation
    private void Awake() {
        // Get the "textObject" child from the canvas
        textObject = transform.GetChild(0).gameObject;
        // Get the TextMeshPro Text element and the object animator
        damageText = textObject.GetComponent<TextMeshProUGUI>();
        textAnimator = textObject.GetComponent<Animator>();
    }

    // // Move the indicator up while active/shown
    // private void Update() {
    //     if (transform.gameObject.activeSelf){
    //         transform.position = new Vector2(transform.position.x, transform.position.y + 1);
    //     }
    // }

    // Method to be called whenever the Object is spawned
    public void OnObjectSpawn() {
        // Fade out the damageIndicator after a short period
        StartCoroutine(Despawn());
    }

    // Method to set the damage indicator text and position
    public void setDamage(int damage) {
        damageText.text = $"{(damage)}";
    }

    // Method to fade out the text after a delay
    private IEnumerator Despawn() {
        // Fade in the damageIndicator
        textAnimator.SetBool("Shown", true);
        yield return new WaitForSeconds(0.5f);
        textAnimator.SetBool("Shown", false);
        // Wait for animation to play out and then "Despawn the indicator
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.SetActive(false);
    }

}
