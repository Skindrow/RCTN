using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "new Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] public Sprite levelSprite;
    [SerializeField] public int keyCost;
    [SerializeField] public SquadSpawnData[] EnemySquadsData;
    [SerializeField] public SquadSpawnData[] LoopEnemySquadsData;
    [SerializeField] public SquadSpawnData PlayerSquadData;
    [SerializeField] public ChestSpawnData ChestSpawnData;
    [SerializeField] public Color CameraColor;
    [SerializeField] public GameObject Map;
    [SerializeField] public LocalizedString levelName;
}
