using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private PhysicMover mover;
    [SerializeField] private HealthBehaviour health;
    [SerializeField] private float moveForce;
    public void MoveTo(Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - new Vector2(transform.position.x, transform.position.y);
        mover.MoveTo(direction, moveForce);
    }
}