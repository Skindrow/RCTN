using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Squad squad;
    private Vector2 targetPosition;

    private bool isMoving = false;

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            isMoving = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
    }
    public void HandleMovement()
    {
        if (isMoving)
            squad.SquadMove(targetPosition);
    }
}
