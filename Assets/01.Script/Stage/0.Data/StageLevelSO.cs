using UnityEngine;

[CreateAssetMenu(fileName = "StageLevelSO", menuName = "StageLevel")]
public class StageLevelSO : ScriptableObject
{
    public ELevels StageLevelType;
    public float DifficultyMultiplier;
    public float BasicSpawnTime;
    public int BasicSpawnAmount;
    public float BasicHPAmount;
    public float ThresholdTime;
}
