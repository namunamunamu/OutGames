using UnityEngine;
using Redcode.Pools;
using Unity.FPS.AI;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public EnemyController EnemyMobilePrefab;
    public int EnemeyPoolSize = 10;

    public Pool<EnemyController> enemyMobilePool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        
        enemyMobilePool = Pool.Create(EnemyMobilePrefab, EnemeyPoolSize, transform);
    }
}
