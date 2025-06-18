using UnityEngine;

[CreateAssetMenu(fileName = "Regacy_StageLevelSO", menuName = "Scriptable Objects/Regacy_StageLevelSO")]
public class Regacy_StageLevelSO : ScriptableObject
{
    [Header("이름")]
    [SerializeField] private string _name; 
    public string Name => _name;


    [Header("레벨 범위")]
    [SerializeField] private int _startLevel;
    public int StartLevel => _startLevel;

    [SerializeField] private int _endLevel;
    public int EndLevel => _endLevel;


    [Header("시간")]
    [SerializeField] private float _duration;
    public float Duration => _duration;


    [Header("능력치 배율")]
    [SerializeField] private float _healthFactor;
    public float HealthFactor => _healthFactor;

    [SerializeField] private float _damageFactor;
    public float DamageFactor => _damageFactor;
    

    [Header("스폰 설정")]
    [SerializeField] private float _spawnInterval;
    public float SpawnInterval => _spawnInterval;

    [SerializeField] private float _spawnRate;
    public float SpawnRate => _spawnRate;
}
