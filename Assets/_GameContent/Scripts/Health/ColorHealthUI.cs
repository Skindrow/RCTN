using UnityEngine;

public class ColorHealthUI : MonoBehaviour
{
    [SerializeField] private HealthBehaviour health;
    [SerializeField] private SpriteRenderer srMainSprite;
    [SerializeField] private SpriteRenderer sr;

    private void Start()
    {
        health.OnHealthChange += UpdateUI;
        sr.sprite = srMainSprite.sprite;
    }
    private void OnDestroy()
    {
        health.OnHealthChange -= UpdateUI;
    }
    private void UpdateUI(int currentHealth)
    {
        float persentage = (float)((float)currentHealth / (float)health.MaxHealth);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1.0f - persentage));
    }
}
