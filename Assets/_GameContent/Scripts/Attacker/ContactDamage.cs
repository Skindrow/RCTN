using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private UnityEvent onDamageDeal;
    [SerializeField] private int damage;
    [SerializeField] private bool isDestroyed = false;
    private int targetIndex;
    public void SetDamage(int damage)
    {
        if (damage < 0)
            damage = 0;
        this.damage = damage;
    }
    public void SetLayer(int damage)
    {
        targetIndex = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthBehaviour healthBehaviour = collision.GetComponent<HealthBehaviour>();
        if (healthBehaviour?.Index == targetIndex)
        {
            healthBehaviour.GetDamage(damage);
            onDamageDeal?.Invoke();
            if (isDestroyed)
                Destroy(gameObject);
        }
    }
}
