using System;
using System.Collections.Generic;
using Redcode.Pools;
using Unity.FPS.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpanwer : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    private float _currentTime;
    private float REWPAWN_TIME = 3f;
    private int MAX_COUNT = 10;

    private void Start()
    {
        StageManager.instance.OnStageLevelChanged += SetParameters;
    }

    public void SetParameters()
    {
        int spawnAmout = StageManager.instance.GetCurrentStageDTO().CurrentStageLevel.SpawnAmount;
        float spawnTime = StageManager.instance.GetCurrentStageDTO().CurrentStageLevel.SpawnTime;

        REWPAWN_TIME = spawnTime;
        MAX_COUNT = spawnAmout;
        
        Debug.Log($"스테이지 정보에 변화가 생겼습ㄴ디ㅏ :: 리스폰타임 : {REWPAWN_TIME}, 스폰갯수 {MAX_COUNT}");
    }
    
    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= REWPAWN_TIME)
        {
            _currentTime = 0f;

            int enemyCount = FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
            if (enemyCount >= MAX_COUNT)
            {
                return;
            }

            var randomIndex = Random.Range(0, SpawnPoints.Count);
            Instantiate(EnemyPrefab, SpawnPoints[randomIndex].position, Quaternion.identity);
            // EnemyController enemy = PoolManager.Instance.enemyMobilePool.Get();
            // enemy.transform.position = SpawnPoints[randomIndex].position;
        }
    }

}