using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SquadSpawner : MonoBehaviour
{
    [SerializeField] private Squad squadPrefab;
    [SerializeField] private int maxSquads;
    [SerializeField] private float spawnTick;
    [SerializeField] private float spawnCheckTick;
    [SerializeField] private SquadSpawnData[] spawnData;
    [SerializeField] private Vector3[] spawnPoints;
    private List<Squad> squads = new List<Squad>();
    private int currentSpawnDataIndex = 0;
    private void Start()
    {
        StartCoroutine(SquadSpawn());
        StartCoroutine(SquadDataSwitch());
    }
    private IEnumerator SquadDataSwitch()
    {
        for (int i = 0; i < spawnData.Length; i++)
        {
            yield return new WaitForSeconds(spawnData[currentSpawnDataIndex].secondsBeforeSwitch);
            currentSpawnDataIndex++;
        }
    }
    private IEnumerator SquadSpawn()
    {
        while (true)
        {
            if (squads.Count <= maxSquads)
            {
                Squad squadGO = Instantiate(squadPrefab, SpawnZone(), Quaternion.identity);
                SpawnUnits(squadGO);
                squadGO.OnSquadDeath += DeleteSquad;
                squads.Add(squadGO);
                yield return new WaitForSeconds(spawnTick);
            }
            else
            {
                yield return new WaitForSeconds(spawnCheckTick);
            }
        }
    }
    private void DeleteSquad(Squad squad)
    {
        if (squads.Contains(squad))
        {
            squads.Remove(squad);
            squad.OnSquadDeath -= DeleteSquad;
            Destroy(squad.gameObject);
        }
    }
    private SquadSpawnData GetSpawnData()
    {
        return spawnData[currentSpawnDataIndex];
    }
    private void SpawnUnits(Squad squad)
    {
        SquadSpawnData currentSpawnData = GetSpawnData();
        List<Unit> unitPrefs = SquadSpawnData.GetUnits(currentSpawnData);
        for (int i = 0; i < unitPrefs.Count; i++)
        {
            Unit unit = Instantiate(unitPrefs[i], squad.transform);
            unit.transform.localPosition = Vector2.zero + UnitRndSpawnPos();
            squad.AddUnit(unit);
        }
    }
    private float rndPosDelta = 0.1f;
    private Vector2 UnitRndSpawnPos()
    {
        float x = Random.Range(-rndPosDelta, rndPosDelta);
        float y = Random.Range(-rndPosDelta, rndPosDelta);
        Vector2 pos = new Vector2(x, y);
        return pos;
    }
    private Vector3 SpawnZone()
    {
        int rnd = Random.Range(0, spawnPoints.Length);
        return spawnPoints[rnd];
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Gizmos.DrawSphere(spawnPoints[i], 0.3f);
        }
    }
}
