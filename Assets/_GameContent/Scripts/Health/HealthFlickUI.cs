using System.Collections;
using UnityEngine;

public class HealthFlickUI : MonoBehaviour
{
    [SerializeField] private HealthBehaviour health;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color flickColor;


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
        float persentage = (float)((float)currentHealth / (float)health.MaxHealth);
        sr.color = new Color(persentage, sr.color.g, sr.color.b);
    }
}
