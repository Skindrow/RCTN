using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private float detectTick = 0.1f;
    [SerializeField] private float detectRadius = 2f;
    private int fractionIndex;

    public void SetIndex(int index)
    {
        fractionIndex = index;
        StartCoroutine(TargetFindFlow());
    }

    private IEnumerator TargetFindFlow()
    {
        while (true)
        {
            GetClosestUnit();
            yield return new WaitForSeconds(detectTick);
        }
    }
    public delegate void UnitDetectEvent(HealthBehaviour unitHealth);
    public UnitDetectEvent OnUnitDetect;
    public UnitDetectEvent OnNoUnitDetect;
    public void GetClosestUnit()
    {
        HealthBehaviour unitHealth = TargetFinder.GetTargetInRadius(fractionIndex, transform, detectRadius);
        if (unitHealth != null)
        {
            print("detect");
            OnUnitDetect?.Invoke(unitHealth);
        }
        else
        {
            print("nodetect");
            OnNoUnitDetect?.Invoke(unitHealth);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}