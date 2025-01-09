using UnityEngine;

public class CameraSquadFollow : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Squad squad;
    private const float Z_POS = -10;
    void FixedUpdate()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, squad.CenterOfSquad(), cameraSpeed);
        newPos.z = Z_POS;
        transform.position = newPos;
    }
}
