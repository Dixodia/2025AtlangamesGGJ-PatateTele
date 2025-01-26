using UnityEngine;

public class PointerChanger : MonoBehaviour
{
    public Texture2D defaultCursor;    // Default cursor texture
    public Texture2D hoverCursor;      // Hover cursor texture
    public LayerMask clickableLayer;   // Layer for clickable objects
    public Camera cam;


    private void Awake()
    {
        
    }
    void Update()
    {
        ChangeCursorOnHover();
    }

    void ChangeCursorOnHover()
    {
        // Perform a raycast from the camera's position to where the mouse is pointing in the 3D world
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
        {
            // If we hit a clickable object, change the cursor
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
            Debug.Log("clickable");
        }
        else
        {
            // If no object is under the mouse, reset to the default cursor
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
            Debug.Log("unclickable");
        }
    }
}

