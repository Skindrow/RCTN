using UnityEngine;

public class DeadUnit : MonoBehaviour
{
    [SerializeField] private Unit unit;
    public bool IsCanRevive = false;

    public Unit ReviveUnit()
    {
        Unit revivedUnit = Instantiate(unit, transform.position, Quaternion.identity);
        return revivedUnit;
    }
    public Unit ReviveUnit(int fraction)
    {
        Unit revivedUnit = Instantiate(unit, transform.position, Quaternion.identity);
        return revivedUnit;
    }
    public void BodyDestroy()
    {
        Destroy(gameObject);
    }
}
