using UnityEngine;

public class Parallax : MonoBehaviour {

    // Script variable
    private float width, height, startposX, startposY;
    public GameObject cam;
    // Script parameter
    public float parallaxEffect;

    // Start is called before the first frame update
    private void Awake() {
        startposX = transform.position.x;
        startposY = transform.position.y;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    private void Update() {
        // Get the current camera position
        float tempX = cam.transform.position.x * (1 - parallaxEffect);
        float tempY = cam.transform.position.y * (1 - parallaxEffect);

        // Move this 'slice' (layer) of background in the x and y directions
        float distX = cam.transform.position.x * parallaxEffect;
        float distY = cam.transform.position.y * parallaxEffect;
        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        // Make repeating background (x axis)
        if(tempX > startposX + width) startposX += width;
        else if(tempX < startposX - width) startposX -= width;
        // Make repeating background (y axis)
        if(tempY > startposY + height) startposY += height;
        else if(tempY < startposY - height) startposY -= height;
    }
}
