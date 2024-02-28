using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce;
    public float rotationTorque;
    public float uprightThreshold = 100f; // 회전한 후에 원통이 똑바로 서있는지 확인할 임계값

    public bool isGrounded;
    Rigidbody rb;

    Quaternion initialRotation; // 초기 회전 상태 저장

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1f, 0.0f);
        initialRotation = rb.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            // Apply force for jumping
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);

            // Apply torque for rotation only along the z-axis
            rb.AddTorque(Vector3.left * rotationTorque, ForceMode.Impulse);
        }

        // Check if the cylinder is within the upright threshold
        if (!isGrounded && Quaternion.Angle(rb.rotation, initialRotation) < uprightThreshold)
        {
            // If within the threshold, reset rotation to initial rotation
            rb.rotation = Quaternion.Lerp(rb.rotation, initialRotation, Time.deltaTime * 2f);
        }
    }
}
