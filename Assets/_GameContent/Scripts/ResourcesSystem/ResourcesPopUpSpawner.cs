using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPopUpSpawner : MonoBehaviour
{
    [SerializeField] private ResourcesPopUp popUp;
    [SerializeField] private ResourcesChanger changer;
    [SerializeField] private Transform popUpPoint;
    private void Start()
    {
        changer.OnChange += SpawnPopUp;
    }
    private void OnDestroy()
    {
        changer.OnChange -= SpawnPopUp;
    }
    private void SpawnPopUp(int delta)
    {

        ResourcesPopUp popUpGo = Instantiate(popUp, popUpPoint.position, Quaternion.identity);
        popUpGo.SetPopUp(delta);
    }
    public void SpawnManualy(int delta, Transform point)
    {
        ResourcesPopUp popUpGo = Instantiate(popUp, point.position, Quaternion.identity);
        popUpGo.SetPopUp(delta);
    }
    public void SpawnManualy(int delta, Vector3 pos)
    {
        ResourcesPopUp popUpGo = Instantiate(popUp, pos, Quaternion.identity);
        popUpGo.SetPopUp(delta);
    }
}
