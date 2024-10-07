using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Vector2 moveInput;
    private Rigidbody rb;

    public float moveSpeed = 5f;
    public float jumpForce = 7f; 
    private bool isGrounded = true; 

    public GameObject bulletPrefab; 
    public Transform bulletSpawnPoint; 
    public float bulletSpeed = 10f; 

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Move.performed += OnMovePerformed;
        playerInputActions.Player.Move.canceled += OnMoveCanceled;
        playerInputActions.Player.Jump.performed += OnJumpPerformed; 
        playerInputActions.Player.Shoot.performed += OnShootPerformed; 
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Move.performed -= OnMovePerformed;
        playerInputActions.Player.Move.canceled -= OnMoveCanceled;
        playerInputActions.Player.Jump.performed -= OnJumpPerformed;
        playerInputActions.Player.Shoot.performed -= OnShootPerformed; 
        playerInputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }
        }
    }
}
