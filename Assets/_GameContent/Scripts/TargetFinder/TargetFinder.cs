using UnityEngine;

public static class TargetFinder
{

    public static HealthBehaviour GetTargetInRadius(int index, Transform ownTransform, float radius)
    {

        HealthBehaviour selectedTarget = null;
        float lastDistance = float.MaxValue;
        for (int i = 0; i < HealthBehaviour.allHealthBehaviour.Count; i++)
        {
            if (HealthBehaviour.allHealthBehaviour[i].Index != index)
                continue;
            float distance = Vector2.Distance(HealthBehaviour.allHealthBehaviour[i].transform.position, ownTransform.position);
            if (distance < lastDistance && distance < radius)
            {
                lastDistance = distance;
                selectedTarget = HealthBehaviour.allHealthBehaviour[i];
            }
        }
        return selectedTarget;
    }
    public static HealthBehaviour GetNearestTarget(int index , Transform ownTransform)
    {
        HealthBehaviour selectedTarget = null;
        float lastDistance = float.MaxValue;
        for (int i = 0; i < HealthBehaviour.allHealthBehaviour.Count; i++)
        {
            if (HealthBehaviour.allHealthBehaviour[i].Index != index)
                continue;
            float distance = Vector2.Distance(HealthBehaviour.allHealthBehaviour[i].transform.position, ownTransform.position);
            if (distance < lastDistance)
            {
                lastDistance = distance;
                selectedTarget = HealthBehaviour.allHealthBehaviour[i];
            }
        }
        return selectedTarget;
    }
    public static Vector3 GetRndPointInRadius(Transform ownTransform, float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius;

        randomPoint += ownTransform.position;

        return randomPoint;
    }
    public static Vector2 GetRndPointOutsideRadius(Transform ownTransform, float radius)
    {
        // Генерируем случайную точку на поверхности сферы с радиусом, большим на 1
        Vector3 randomPoint = Random.onUnitSphere * (radius + 1);

        // Смещаем точку относительно позиции объекта
        randomPoint += ownTransform.position;

        return randomPoint;
    }
}
