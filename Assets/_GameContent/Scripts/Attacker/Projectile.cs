using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform rotatedTransform;
    [SerializeField] private ContactDamage damager;
    [SerializeField] private float speed;
    [SerializeField] private float destroyAfter;


    public void SetProjectile(Vector2 forceVector, int damage, int targetLayer)
    {
        rb.AddForce(forceVector.normalized * speed);
        damager.SetLayer(targetLayer);
        damager.SetDamage(damage);
        if (rotatedTransform != null)
        {
            Vector3 Rotated = new Vector3(transform.position.x + forceVector.x,
                transform.position.y + forceVector.y, transform.position.z);
            Rotation(Rotated, rotatedTransform);
        }
        Destroy(gameObject, destroyAfter);

    }

    private void Rotation(Vector3 target, Transform rotated)
    {
        float offset = 0;
        Vector2 thisPos = rotated.position;
        target.x = target.x - thisPos.x;
        target.y = target.y - thisPos.y;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        rotated.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }
}
