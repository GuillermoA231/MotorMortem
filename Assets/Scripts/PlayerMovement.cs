using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float smoothTime = 12f;
    
    [Header("Motorcycle-like Movement")]
    [SerializeField] private float maxTurnSpeed = 5f;
    [SerializeField] private float turnAcceleration = 8f;
    [SerializeField] private float turnDeceleration = 10f;
    
    private Vector3 targetPosition;
    private Vector3 velocity;
    private float currentHorizontalVelocity = 0f;
    private float previousTargetX;
    
    public void SetMinX(float value) => minX = value;
    public void SetMaxX(float value) => maxX = value;
    public void SetSmoothTime(float value) => smoothTime = value;
    
    private void Start()
    {
        previousTargetX = transform.position.x;
    }
    
    private void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        
        float normalizedX = mouseScreenPos.x / Screen.width;
        
        float targetX = Mathf.Lerp(minX, maxX, normalizedX);
        
        float desiredDirection = Mathf.Sign(targetX - transform.position.x);
        float distanceToTarget = Mathf.Abs(targetX - transform.position.x);
        
        if (distanceToTarget > 0.01f)
        {
            currentHorizontalVelocity = Mathf.MoveTowards(
                currentHorizontalVelocity,
                desiredDirection * maxTurnSpeed,
                turnAcceleration * Time.deltaTime
            );
        }
        else
        {
            currentHorizontalVelocity = Mathf.MoveTowards(
                currentHorizontalVelocity,
                0f,
                turnDeceleration * Time.deltaTime
            );
        }
        
        float newX = transform.position.x + (currentHorizontalVelocity * Time.deltaTime);
        newX = Mathf.Clamp(newX, minX, maxX);
        
        if (desiredDirection > 0)
            newX = Mathf.Min(newX, targetX);
        else
            newX = Mathf.Max(newX, targetX);
        
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        
        previousTargetX = targetX;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && canGetHurt)
        {
            // TODO
            StartCoroutine(DamageDelay());
        }
    }
    
    private bool canGetHurt = true;
    
    private IEnumerator DamageDelay()
    {
        canGetHurt = false;
        yield return new WaitForSeconds(0.5f);
        canGetHurt = true;
    }
}