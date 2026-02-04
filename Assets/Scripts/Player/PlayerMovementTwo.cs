using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTwo : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public Rigidbody rb;

    [Header("Horizontal Bounds")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;

    [Header("Turning")]
    [SerializeField] private float maxTurnSpeed = 5f;
    [SerializeField] private float turnAcceleration = 8f;
    [SerializeField] private float turnDeceleration = 10f;

    // Horizontal velocity in world units per second (positive = right, negative = left)
    private float currentHorizontalVelocity = 0f;

    private void Update()
    {
        HandleMouseMovement();
    }

    private void FixedUpdate()
    {
        // Forward movement based on fixed delta time
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;

        // Desired horizontal displacement this physics step (before clamping)
        float horizontalDisplacement = currentHorizontalVelocity * Time.fixedDeltaTime;

        // Compute new unclamped X position
        float unclampedNewX = rb.position.x + horizontalDisplacement;

        // Clamp to bounds
        float clampedNewX = Mathf.Clamp(unclampedNewX, minX, maxX);

        // Build horizontal move using the clamped delta (this prevents passing through the ground)
        Vector3 horizontalMove = transform.right * (clampedNewX - rb.position.x);

        rb.MovePosition(rb.position + forwardMove + horizontalMove);

        // If we hit a boundary, zero out horizontal velocity to prevent constant pushing into it
        if (clampedNewX == minX || clampedNewX == maxX)
        {
            // Stop horizontal movement when at the edge
            currentHorizontalVelocity = 0f;
        }
    }

    private void HandleMouseMovement()
    {
        if (Mouse.current == null)
            return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        float normalizedX = mouseScreenPos.x / Screen.width;
        float targetX = Mathf.Lerp(minX, maxX, normalizedX);

        MoveTowardsTarget(targetX);
    }

    private void MoveTowardsTarget(float targetX)
    {
        float currentX = transform.position.x;
        float desiredDirection = Mathf.Sign(targetX - currentX);
        float distanceToTarget = Mathf.Abs(targetX - currentX);

        float acceleration = distanceToTarget > 0.01f ? turnAcceleration : turnDeceleration;
        float targetVelocity = distanceToTarget > 0.01f ? desiredDirection * maxTurnSpeed : 0f;

        // Smooth acceleration using Update() delta time
        currentHorizontalVelocity = Mathf.MoveTowards(
            currentHorizontalVelocity,
            targetVelocity,
            acceleration * Time.deltaTime
        );
    }
}
