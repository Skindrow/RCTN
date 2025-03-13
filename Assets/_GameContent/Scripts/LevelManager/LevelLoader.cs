using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SquadSpawner squadSpawner;
    [SerializeField] private ChestSpawner chestSpawner;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        LevelData currentLevel = LevelDisplay.currentLevelData;
        squadSpawner.SetSquads(currentLevel.EnemySquadsData, currentLevel.LoopEnemySquadsData,
            currentLevel.PlayerSquadData);
        chestSpawner.SetChestSpawnData(currentLevel.ChestSpawnData);
        mainCamera.backgroundColor = currentLevel.CameraColor;
        if (currentLevel.Map != null)
        {
            Instantiate(currentLevel.Map);
        }
    }
}
