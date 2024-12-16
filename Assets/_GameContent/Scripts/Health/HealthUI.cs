using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthBehaviour health;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        health.OnHealthChange += UpdateUI;
    }
    private void OnDestroy()
    {
        health.OnHealthChange -= UpdateUI;
    }
    private void UpdateUI(int currentHealth)
    {
        healthBar.fillAmount = (float)((float)currentHealth / (float)health.MaxHealth);
    }
}
