using UnityEngine;

public class MoveBG : MonoBehaviour {
    // SerializeField variables
    [SerializeField] Transform target;
    [SerializeField] double incrementer;
    // Update is called once per frame
    void Update() {
        // Divide the incrementer value by 10 (otherwise too fast)
        float incX = target.position.x + (float)(0.1 * incrementer);
        float incY = target.position.y + (float)(0.1 * incrementer);
        // Make the transformation
        transform.position = new Vector3(incX, incY, 0);
    }
}
