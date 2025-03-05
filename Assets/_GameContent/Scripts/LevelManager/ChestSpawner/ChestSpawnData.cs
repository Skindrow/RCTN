using UnityEngine;
[CreateAssetMenu(fileName = "new ChestSpawnData")]
public class ChestSpawnData : ScriptableObject
{
    public ChestSpawn[] ChestSpawns;
}
[System.Serializable]
public class ChestSpawn
{
    public int MinCoins;
    public int MaxCoins;
    public int TimeBeforeSwitch;
    public int SpawnTick;
    public ResourcesChanger ChestPrefab;
}
