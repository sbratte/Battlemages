using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;
    private float mouseX;
    private float xRotation;
    private float mouseY;
    private float turnSpeed = 200f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to the game windown
    }

    void LateUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        // Rotate camera up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f); // clamp rotation so cant look behind player

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Rotate left and right
        player.Rotate(Vector3.up * mouseX);
    }
}