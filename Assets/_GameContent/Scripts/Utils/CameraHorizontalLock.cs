using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontalLock : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float minimalHorizontalSize;

    [SerializeField] private float standartSize = 5.0f;
    private void Update()
    {
        mainCamera.orthographicSize = standartSize;
        float horizontalSize = mainCamera.orthographicSize * mainCamera.aspect;
        if (horizontalSize < minimalHorizontalSize)
        {
            mainCamera.orthographicSize = minimalHorizontalSize / mainCamera.aspect;
        }
    }

}
