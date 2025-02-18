using UnityEngine;

[CreateAssetMenu(fileName = "new Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] public Sprite levelSprite;
    [SerializeField] public int keyCost;
    [SerializeField] public SquadSpawnData[] EnemySquadsData;
    [SerializeField] public SquadSpawnData[] LoopEnemySquadsData;
    [SerializeField] public SquadSpawnData PlayerSquadData;
}
