using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Camera camera;
    public float mouseSensivity = 100f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        camera = Camera.main;
        //Mouse'u kitledik
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (InventorySystem.Instance.isOpen || CraftingSystem.Instance.isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
        
        }


        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0f);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
