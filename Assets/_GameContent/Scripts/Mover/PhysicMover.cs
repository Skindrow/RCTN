using TMPro;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PhysicMover : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void MoveTo(Vector2 direction, float force)
    {
        rb.AddForce(direction.normalized * force);
    }
    public void MoveToPosition(Vector2 targetPosition, float force)
    {

        Vector2 direction = targetPosition - new Vector2(transform.position.x, transform.position.y);
        rb.AddForce(direction.normalized * force);
    }
}
