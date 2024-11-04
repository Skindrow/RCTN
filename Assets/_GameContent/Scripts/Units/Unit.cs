using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTakeDamage;
    [SerializeField] private UnityEvent OnAttack;
    public float maxHealth = 100f;
    private float currentHealth;

    public string factionName;

    public float attackRange = 2f;
    public float attackDetectRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    public float attractionForce = 0.02f; // ���� ������������ ��� �����

    private Unit targetEnemy;
    private bool isAttacking = false;
    private Rigidbody2D rb;
    private float baseSpeed;
    private float currentSpeed;

    private Squad squad; // ������ �� �����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void SetSquad(Squad squad)
    {
        this.squad = squad;
    }

    public void SetBaseSpeed(float speed)
    {
        baseSpeed = speed;
    }

    public void MoveToPosition(Vector2 targetPosition, float maxSpeed)
    {
        if (rb != null)
        {
            Vector2 direction = (targetPosition - rb.position).normalized;
            float distance = Vector2.Distance(targetPosition, rb.position);
            currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, distance / 10f); // ������� ��������� ��������

            if (targetEnemy != null)
            {
                Vector2 attackDirection = (targetEnemy.transform.position - transform.position).normalized;
                rb.AddForce(attackDirection * attractionForce);
            }
            rb.AddForce(direction * currentSpeed);
        }
    }

    public void Stop()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            Vector2 direction = (targetEnemy.transform.position - transform.position).normalized;
            rb.linearVelocity += direction * attractionForce;
            Attack(targetEnemy);
        }
        else
        {
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackDetectRange);
        foreach (var hitCollider in hitColliders)
        {
            Unit enemy = hitCollider.GetComponent<Unit>();
            if (enemy != null && enemy.factionName != factionName)
            {
                targetEnemy = enemy;
                Attack(enemy);
                break;
            }
        }
    }

    private void Attack(Unit enemy)
    {
        if (Vector2.Distance(enemy.transform.position, transform.position) > attackDetectRange)
        {
            targetEnemy = null;
        }
        else if (Vector2.Distance(enemy.transform.position, transform.position) < attackRange && (Time.time - lastAttackTime) > attackCooldown)
        {
            print("Attack");
            OnAttack?.Invoke();
            enemy.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnTakeDamage?.Invoke();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (squad != null)
        {
            squad.RemoveUnit(this);
        }
        Destroy(gameObject);
    }
}