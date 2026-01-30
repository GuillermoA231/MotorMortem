using System.Collections;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable, IHealthProvider, IDeatheable
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    
    public event System.Action<int> OnHealthChanged;
    public event System.Action OnDeath;
    
    private void Start()
    {
        ResetHealth();
    }
    
    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        
        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth);
        
        if (currentHealth <= 0)
            OnDeath?.Invoke();
    }
    
    public void Heal(int amount)
    {
        if (amount <= 0) return;
        
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth);
    }
    
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }
}