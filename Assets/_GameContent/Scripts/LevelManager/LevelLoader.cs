using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SquadSpawner squadSpawner;

    private void Awake()
    {
        LevelData currentLevel = LevelDisplay.currentLevelData;
        squadSpawner.SetSquads(currentLevel.EnemySquadsData, currentLevel.LoopEnemySquadsData,
            currentLevel.PlayerSquadData);
    }
}
