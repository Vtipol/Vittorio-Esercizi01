using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    public Transform playerBody; 
    public float mouseSensitivity = 100f; 
    private Vector2 rotateInput; 

    private float xRotation = 0f; 

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Rotate.performed += OnRotatePerformed; 
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Rotate.performed -= OnRotatePerformed; 
        playerInputActions.Disable();
    }

    private void OnRotatePerformed(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float mouseX = rotateInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = rotateInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
