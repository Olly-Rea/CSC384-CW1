using UnityEngine;

public class Rotate : MonoBehaviour {
    // SerializeField variables
    [SerializeField] private float degPerSec;
    // Rotate the object at 20 degrees a second
    void Update() {
        transform.Rotate(0, 0, degPerSec * Time.deltaTime);
    }
}
