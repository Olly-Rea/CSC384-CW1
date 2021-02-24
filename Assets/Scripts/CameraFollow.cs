using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    // Public 'target' object
    public Transform target;
    // // Private variables
    // private float smoothSpeed = 0.125f;
    private float offset = -1;

    // Method to be run after Update()
    private void LateUpdate() {
        transform.position = new Vector3(target.position.x, target.position.y, offset);
        // Vector3.SmoothDamp(transform.position, target.position + offset);
    }

}
