using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    // Public 'target' object
    [SerializeField] Transform target;

    // Method to be run after Update()
    private void LateUpdate() {
        transform.position = new Vector3(target.position.x, target.position.y, -1);
    }

}
