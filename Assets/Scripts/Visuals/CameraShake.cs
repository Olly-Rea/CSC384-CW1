using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    
    public IEnumerator shakeCamera(float magnitude, float duration) {
        // Temp variable to store the current magnitude (to allow fade in / out)
        float currentMagnitude = 0;
        // Loop while the timeElapsed is less than 2 seconds
        float timeElapsed = 0.0f;
        while (timeElapsed < duration) {
            // Increment/Decrement currentMagnitude based on timeElapsed
            currentMagnitude += (timeElapsed <= 1.0f && currentMagnitude < magnitude) ? 0.01f : -0.01f;
            // Get new random x and y values
            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;
            // Move the camera
            transform.localPosition = new Vector2(x, y);
            // Increment the time elapsed variable
            timeElapsed += Time.deltaTime;
            // Wait a short period
            yield return new WaitForSeconds(0.02f);
        }
        // Revert the camera back to it's central starting position
        transform.localPosition = new Vector2(0,0);
    }

}
