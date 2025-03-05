using UnityEngine;

public class DeadUnit : MonoBehaviour
{
    [SerializeField] private Unit unit;
    public bool IsCanRevive = false;

    public Unit ReviveUnit()
    {
        Unit revivedUnit = Instantiate(unit, transform.position + ReviveDeviation(0.3f), Quaternion.identity);
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

    private Vector3 ReviveDeviation(float deviation)
    {
        float x = Random.Range(-deviation, deviation);
        float y = Random.Range(-deviation, deviation);
        return new Vector3(x, y, 0);
    }
}
