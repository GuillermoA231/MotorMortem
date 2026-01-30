using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private TMP_Text healthText;
    
    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealthDisplay;
    }
    
    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealthDisplay;
    }
    
    private void UpdateHealthDisplay(int health)
    {
        healthText.text = $"Health: {health}";
    }
}
