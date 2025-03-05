using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private UnityEvent onGetDamage;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private float invoulTime;
    [SerializeField] private bool isDestroyedOnDeath = true;
    [SerializeField] private int index;
    [SerializeField] private StatData healthStat;
    private bool isInvoul = false;
    private bool isDead = false;
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        private set
        {

        }
    }
    public int Index => index;
    public static List<HealthBehaviour> allHealthBehaviour = new List<HealthBehaviour>();
    public int CurrentHealth { get; private set; }
    private void Start()
    {
        if (healthStat != null)
        {
            float multiplayer = healthStat.GetStat();
            maxHealth = (int)(maxHealth * multiplayer);
        }


        CurrentHealth = maxHealth;
    }
    private void OnEnable()
    {
        allHealthBehaviour.Add(this);
    }
    private void OnDisable()
    {
        allHealthBehaviour.Remove(this);
    }
    public delegate void OnHealthChangeEvent(int currentHealth);
    public OnHealthChangeEvent OnHealthChange;
    public OnHealthChangeEvent OnHealthEnd;

    public void SetIndex(int index)
    {
        this.index = index;
    }
    public void GetDamage(int count)
    {
        if (!isInvoul && !isDead)
        {
            CurrentHealth -= count;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnHealthEnd?.Invoke(0);
                Death();
            }
            onGetDamage?.Invoke();
            OnHealthChange?.Invoke(CurrentHealth);
            StartCoroutine(InvoulTime());
        }
    }
    public void Heal(int count)
    {
        CurrentHealth += count;
        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
        OnHealthChange?.Invoke(CurrentHealth);
    }
    private IEnumerator InvoulTime()
    {
        isInvoul = true;
        yield return new WaitForSeconds(invoulTime);
        isInvoul = false;
    }
    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        CurrentHealth += amount;
    }
    private void Death()
    {
        isDead = true;
        onDeath?.Invoke();
        if (isDestroyedOnDeath)
            Destroy(gameObject);
    }
}
