using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Rigidbody2D rb;
    private float baseSpeed;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, distance / 10f); // Плавное изменение скорости
            rb.velocity = direction * currentSpeed;
        }
    }

    public void Stop()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
