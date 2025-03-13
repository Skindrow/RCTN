using System.Collections;
using UnityEngine;

public class BackgroundMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] backs;
    [SerializeField] private Color[] cameraColors;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 cameraFollowDelta;
    [SerializeField] private float cameraSpeed;

    private float startCameraZ;
    private void Start()
    {
        startCameraZ = mainCamera.transform.position.z;
        int rnd = Random.Range(0, backs.Length);
        Instantiate(backs[rnd]);
        mainCamera.backgroundColor = cameraColors[rnd];
        StartCoroutine(CameraMoveCoroutine());
    }
    private IEnumerator CameraMoveCoroutine()
    {
        while (true)
        {
            float x = Random.Range(-cameraFollowDelta.x, cameraFollowDelta.x);
            float y = Random.Range(-cameraFollowDelta.y, cameraFollowDelta.y);
            Vector2 targetPos = new Vector2(x, y);
            while (Vector2.Distance(mainCamera.transform.position, targetPos) > 0.1f)
            {
                Vector3 newPos = Vector3.MoveTowards(mainCamera.transform.position, targetPos, cameraSpeed);
                newPos.z = startCameraZ;
                mainCamera.transform.position = newPos;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
