using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageCooldown = 0.5f;
    
    private float lastDamageTime;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && CanTakeDamage())
        {
            health.TakeDamage(damage);
            lastDamageTime = Time.time;
            Debug.Log("Took damage by " + damage + " - Health left: " + health.CurrentHealth);
        }
    }
    
    private bool CanTakeDamage()
    {
        return Time.time >= lastDamageTime + damageCooldown;
    }
}