using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private ResourceData coinsData;
    private ChestSpawnData chestSpawnData;
    private int currentChestData = 0;
    public void SetChestSpawnData(ChestSpawnData data)
    {
        chestSpawnData = data;
        StartCoroutine(ChestSpawnDataSwitch());
        StartCoroutine(ChestSpawn());
    }
    private IEnumerator ChestSpawnDataSwitch()
    {
        for (int i = 0; i < chestSpawnData.ChestSpawns.Length; i++)
        {
            yield return new WaitForSeconds(chestSpawnData.ChestSpawns[currentChestData].TimeBeforeSwitch);
            currentChestData++;
        }
    }
    private IEnumerator ChestSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(chestSpawnData.ChestSpawns[currentChestData].SpawnTick);
            ResourcesChanger chestGO = Instantiate(chestSpawnData.ChestSpawns[currentChestData].ChestPrefab, SpawnZone(), Quaternion.identity);
            int amount = Random.Range(chestSpawnData.ChestSpawns[currentChestData].MinCoins, chestSpawnData.ChestSpawns[currentChestData].MaxCoins);
            chestGO.SetResources(new Resource(coinsData, amount));
        }
    }
    private List<Vector3> spawnPointsList = new List<Vector3>();

    private Vector3 SpawnZone()
    {
        if (spawnPointsList.Count <= 0)
        {
            spawnPointsList.AddRange(spawnPoints);
        }
        int rnd = Random.Range(0, spawnPointsList.Count);
        Vector3 pos = spawnPointsList[rnd];
        spawnPointsList.RemoveAt(rnd);
        return pos;
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Gizmos.DrawSphere(spawnPoints[i], 0.3f);
        }
    }
}
