using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Spawner Configs")]
    public ESpawnerType SpawnerType;
    [SerializeField] private int _initialPoolSize;
    [SerializeField] private int _maxPoolSize;

    [Header("For Rotation Only")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    //[Header("For Point To Point Only")]
    //public Transform[] MovePoints;

    [Header("Target Prefabs")]
    [SerializeField] private List<Target> _targetsList;

    private List<Target> _targetsInPool = new();

    private bool _canSpawn;
    private float _currentSpawnTime;
    private float _time;

    private void Awake()
    {
        for (int i = 0; i < _maxPoolSize - 1; i++)
        {
            GenerateTargets(i);
        }

        _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        //_canSpawn = SpawnerType == ESpawnerType.Rotate ?
        //    !transform.Cast<Transform>().Any(child => child.gameObject.activeInHierarchy) :
        //    transform.Cast<Transform>().Where(child => child.gameObject.activeSelf).Count() < 3;

        _canSpawn = !transform.Cast<Transform>().Any(child => child.gameObject.activeInHierarchy);

        if (_canSpawn && SpawnerType == ESpawnerType.Rotate)
        {
            _time += Time.deltaTime;

            if (_time >= _currentSpawnTime)
            {
                SpawnTargetRotate();
            }
        }
        //else if (_canSpawn && SpawnerType == ESpawnerType.PointToPoint)
        //{
        //    SpawnTargetPointToPoint();
        //}
    }

    private void GenerateTargets(int index)
    {
        Target target = Instantiate(_targetsList[index], transform);
        target.gameObject.SetActive(false);
        _targetsInPool.Add(target);
        target.Spawner = this;
    }

    private void SpawnTargetRotate()
    {
        _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);

        int index = Random.Range(0, _targetsList.Count);
        _targetsInPool[index].OnSpawn(SpawnerType);

        _time = 0;
    }

    //private void SpawnTargetPointToPoint()
    //{
    //    int index = Random.Range(0, _targetsList.Count);
    //    _targetsInPool[index].OnSpawn(SpawnerType);
    //}
}

public enum ESpawnerType
{
    Rotate,
    PointToPoint
}
