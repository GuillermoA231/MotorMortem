using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Minx and MaxX Clamps the horizontal movement of the player to avoid clipping out of the ground 
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    
    [Header("Motorbike-like movement")] //Helps with game feel and smoother movement
    [SerializeField] private float maxTurnSpeed = 5f;
    [SerializeField] private float turnAcceleration = 8f;
    [SerializeField] private float turnDeceleration = 10f;
    
    private float currentHorizontalVelocity = 0f;
    private void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        float normalizedX = mouseScreenPos.x / Screen.width;
        float targetX = Mathf.Lerp(minX, maxX, normalizedX);
        
        MoveTowardsTarget(targetX);
    }
    
    private void MoveTowardsTarget(float targetX)
    {
        float desiredDirection = Mathf.Sign(targetX - transform.position.x);
        float distanceToTarget = Mathf.Abs(targetX - transform.position.x);
        
        float acceleration = distanceToTarget > 0.01f ? turnAcceleration : turnDeceleration;
        float targetVelocity = distanceToTarget > 0.01f ? desiredDirection * maxTurnSpeed : 0f;
        
        currentHorizontalVelocity = Mathf.MoveTowards(
            currentHorizontalVelocity,
            targetVelocity,
            acceleration * Time.deltaTime
        );
        
        float newX = transform.position.x + (currentHorizontalVelocity * Time.deltaTime);
        newX = Mathf.Clamp(newX, minX, maxX);
        
        if (desiredDirection > 0)
            newX = Mathf.Min(newX, targetX);
        else
            newX = Mathf.Max(newX, targetX);
        
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}

