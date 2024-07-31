using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Spawner Configs")]
    [SerializeField] private int _initialPoolSize;
    [SerializeField] private int _maxPoolSize;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;


    [Header("Target Prefabs")]
    [SerializeField] private List<Target> _targetsList;

    private List<Target> _targetsInPool = new();
    protected Collider SpawnArea;

    private bool _canSpawn;
    private float _currentSpawnTime;
    private float _time;

    private void Awake()
    {
        for (int i = 0; i < _maxPoolSize - 1; i++)
        {
            GenerateTargets(i % _targetsList.Count);
        }

        _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void Update()
    {
        _canSpawn = transform.Cast<Transform>().Where(child => child.gameObject.activeInHierarchy).Count() < 4;

        if (_canSpawn)
        {
            _time += Time.deltaTime;

            if (_time >= _currentSpawnTime)
            {
                SpawnTarget();
            }
        }
    }

    private void GenerateTargets(int index)
    {
        Target target = Instantiate(_targetsList[index], transform);
        target.gameObject.SetActive(false);
        _targetsInPool.Add(target);
        target.Spawner = this;
    }

    private void SpawnTarget()
    {
        int index;
        int checker;
        bool isAlreadySpawned;

        do
        {
            index = Random.Range(0, _targetsInPool.Count);
            checker = Random.Range(0, _targetsInPool.Count);
            isAlreadySpawned = _targetsInPool[index].gameObject.activeInHierarchy;
        } while (index == checker || isAlreadySpawned);

        _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        _targetsInPool[index].OnSpawn();

        _time = 0;
    }
}
