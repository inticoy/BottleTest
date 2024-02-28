using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragJump : MonoBehaviour
{
    public Vector3 jumpForce;
    public float rotationSpeed;

    private Vector3 dragStartPosition;
    private bool isDragging;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                dragStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Calculate drag direction
                Vector3 dragDirection = (touch.position - (Vector2)dragStartPosition).normalized;

                // Apply rotation and jump force
                rb.AddTorque(Vector3.forward * dragDirection.x * rotationSpeed, ForceMode.Impulse);
                rb.AddForce(jumpForce * dragDirection.y, ForceMode.Impulse);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }

        if (isDragging)
        {
            // Update rotation based on touch drag
            Vector3 touchDelta = Input.touches[0].position - (Vector2)dragStartPosition;
            rb.AddTorque(Vector3.forward * touchDelta.x * rotationSpeed * Time.deltaTime, ForceMode.Force);
        }
    }
}
