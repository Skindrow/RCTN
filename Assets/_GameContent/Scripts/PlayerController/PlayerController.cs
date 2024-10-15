using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Squad squad;
    private Vector2 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        // Проверяем, зажата ли левая кнопка мыши
        if (Input.GetMouseButton(0))
        {
            // Получаем позицию курсора мыши в мировом пространстве
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z; // Устанавливаем глубину равной высоте камеры
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            isMoving = true; // Отряд начинает движение
        }
        else
        {
            isMoving = false; // Если кнопка мыши отжата, останавливаем движение
        }

        // Если отряд движется
        if (isMoving)
        {
            squad.MoveSquadToPosition(targetPosition);
        }
        else
        {
            // Останавливаем движение отряда, если кнопка мыши отжата
            squad.StopSquadMovement();
        }
    }
}
