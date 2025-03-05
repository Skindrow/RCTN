using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SquadSpawner squadSpawner;
    [SerializeField] private ChestSpawner chestSpawner;

    private void Awake()
    {
        LevelData currentLevel = LevelDisplay.currentLevelData;
        squadSpawner.SetSquads(currentLevel.EnemySquadsData, currentLevel.LoopEnemySquadsData,
            currentLevel.PlayerSquadData);
        chestSpawner.SetChestSpawnData(currentLevel.ChestSpawnData);
    }
}
