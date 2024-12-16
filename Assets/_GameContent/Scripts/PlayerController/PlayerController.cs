using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Squad squad;
    private Vector2 targetPosition;

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (Input.GetMouseButton(0))
        {
            // �������� ������� ������� ���� � ������� ������������
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z; // ������������� ������� ������ ������ ������
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            squad.SquadMove(targetPosition);
        }
    }
}
