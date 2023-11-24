using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] short instancesPerPrefab;
    [SerializeField] public float SpawnDelay;
    [SerializeField] EnemySpawner enemySpawner;
    private List<GameObject> enemiesPool = new List<GameObject>();
    private float nextTimeToSpawn = 0;
    public bool IsEmpty { get => enemiesPool.All(enemy => enemy.IsDestroyed()); }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        nextTimeToSpawn = Time.time + enemySpawner.WaveDelay;
        for (int i = 0; i < instancesPerPrefab; i++)
        {
            enemiesPool.Add(Instantiate(enemyPrefabs, transform.position, transform.rotation));
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && enemiesPool.Any(enemy => !enemy.IsDestroyed()) && Time.time >= nextTimeToSpawn)
        {
            nextTimeToSpawn = Time.time + SpawnDelay;
            GameObject enemyToSpawn = enemiesPool.FirstOrDefault(enemy => !enemy.IsDestroyed() && !enemy.activeInHierarchy);
            if (enemyToSpawn != null)
            {
                enemyToSpawn.transform.SetPositionAndRotation(transform.position, transform.rotation);
                enemyToSpawn.SetActive(true);
            }
        }
    }

}
