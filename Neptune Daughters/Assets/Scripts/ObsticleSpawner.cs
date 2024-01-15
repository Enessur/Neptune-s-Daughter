using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private GameObject enemyInstance;
    private bool isSpawning = false;

    private void Start()
    {
    }

    private void Update()
    {
        if ((enemyInstance == null || !enemyInstance.activeSelf) && !isSpawning)
        {
            isSpawning = true;
            Invoke("SpawnEnemy", Random.Range(1f, 5f));
        }
    }

    private void SpawnEnemy()
    {
        enemyInstance = Instantiate(enemy, transform.position, transform.rotation);
        isSpawning = false;
    }


}
