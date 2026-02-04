using UnityEngine;


public class PlayerMovementTwo :MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    float horizontalInput;
    float horizontalMultiplier = 2f;
    float forwardInput;

    private void Update()
    {
        Vector3 forwardMove  = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove);
    }
}
