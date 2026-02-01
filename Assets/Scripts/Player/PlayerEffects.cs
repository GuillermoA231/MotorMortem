using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private PPManager ppManager;
    private int previousHealth;

    void OnEnable()
    {
        health.OnHealthChanged += OnHealthChanged;
        previousHealth = health.CurrentHealth;
    }

    void OnDisable()
    {
        health.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int newHealth)
    {
        if (newHealth < previousHealth)
        {
            ppManager.PlayHurtEffect();
        }

        previousHealth = newHealth;
    }
}
