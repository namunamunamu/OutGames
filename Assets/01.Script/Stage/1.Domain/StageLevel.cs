using UnityEngine;


public enum ELevels{

    Easy,
    Normal,
    Hard,
    VeryHard,
    Insane,
    
    
    Count
    
}


public class StageLevel
{
    public StageLevelSO StageLevelSO;
    public ELevels StageLevelType;
    private float _stageStartTime;
    private float _stageEndTime;

    
    public float StageStartTime => _stageStartTime;
    public float StageEndTime => _stageEndTime;
    
    private float _difficultyMultiplyer;
    private float _basicSpawnTime;
    private float _basicHPAmount;

    public float SpawnTime;
    public float HPAmount;
    public int SpawnAmount;

    public StageLevel(StageLevelDTO stageLevelDTO)
    {
        StageLevelType = stageLevelDTO.StageLevelType;
        SpawnTime = stageLevelDTO.SpawnTime;
        HPAmount = stageLevelDTO.HPAmount; 
        SpawnAmount = stageLevelDTO.SpawnAmount;
        
    }
    
    public StageLevel(StageLevelSO stageLevelSO)
    {
        StageLevelSO = stageLevelSO;
        _stageStartTime = stageLevelSO.StartTime;
        _stageEndTime = stageLevelSO.EndTime;
        _difficultyMultiplyer = stageLevelSO.DifficultyMultiplier;
        _basicSpawnTime = stageLevelSO.BasicSpawnTime;
        _basicHPAmount = stageLevelSO.BasicHPAmount;
        StageLevelType = stageLevelSO.StageLevelType;
        
        SpawnTime = GetSpawnTime(_stageEndTime);
        SpawnAmount = GetSpawnAmount(_stageEndTime);
        HPAmount = GetHPAmount(_stageEndTime);

    }
    

    public float GetSpawnTime(float threshholdTime)
    {
        return CalcSpawnInterval(threshholdTime);
    }

    public float GetHPAmount(float threshholdTime)
    {
        return CalcMonsterHP(threshholdTime);
    }

    public int GetSpawnAmount(float threshholdTime)
    {
        return CalcSpawnCount(threshholdTime);
    }
    
    float CalcDifficulty(float timeSec) {
        return Mathf.Pow(_difficultyMultiplyer, timeSec / 225f);
    }

    float CalcSpawnInterval(float timeSec) {
        float d = CalcDifficulty(timeSec);
        return 10f / Mathf.Log(d + 1, 2f);
    }

    int CalcSpawnCount(float timeSec) {
        float d = CalcDifficulty(timeSec);
        return Mathf.RoundToInt(Mathf.Log(d + 1, 2f));
    }

    float CalcMonsterHP(float timeSec) {
        float d = CalcDifficulty(timeSec);
        return 100f * d;
    }

    

}
