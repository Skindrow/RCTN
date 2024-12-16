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
}
