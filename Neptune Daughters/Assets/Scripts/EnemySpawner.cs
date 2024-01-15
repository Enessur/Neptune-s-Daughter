using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private GameObject enemyInstance;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if (enemyInstance == null || !enemyInstance.activeSelf)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        enemyInstance = Instantiate(enemy, transform.position, transform.rotation);
        enemyInstance.transform.parent = transform;
    }
}