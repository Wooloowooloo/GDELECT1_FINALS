using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITarget
{
    [HideInInspector] public TargetSpawner Spawner;

    [SerializeField] private TargetConfigSO _targetConfig;
    [SerializeField] private string _rotateSpawnHash;
    [SerializeField] private string _rotateDespawnHash;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;
    //[SerializeField] private float _movementSpeed;

    private Animator _animator;
    private float _currentLifetime;
    private float _assignedLifetime;
    //private Transform _currentSpawnPoint;
    //private Transform _goToDespawnPoint;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //if (Spawner.SpawnerType == ESpawnerType.Rotate)
        //{
        //    _assignedLifetime = Random.Range(_minLifeTime, _maxLifeTime);
        //}
        //else
        //{
        //    if (Random.Range(1, 101) > 50)
        //    {
        //        _currentSpawnPoint = Spawner.MovePoints[0];
        //        _goToDespawnPoint = Spawner.MovePoints[1];
        //    }
        //    else
        //    {
        //        _currentSpawnPoint = Spawner.MovePoints[1];
        //        _goToDespawnPoint = Spawner.MovePoints[0];
        //    }
        //}

        _assignedLifetime = Random.Range(_minLifeTime, _maxLifeTime);
    }

    private void OnDisable()
    {
        _currentLifetime = 0;
        //_currentSpawnPoint = null;
        //_goToDespawnPoint = null;
    }

    private void Update()
    {
        if (Spawner.SpawnerType == ESpawnerType.Rotate)
        {
            if (isActiveAndEnabled)
            {
                _currentLifetime += Time.deltaTime;
            }

            if (_currentLifetime >= _assignedLifetime)
            {
                StartCoroutine(NaturalDespawn());
            }
        }

        //if (Spawner.SpawnerType == ESpawnerType.PointToPoint)
        //{
        //    if (isActiveAndEnabled)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, _goToDespawnPoint.position, _movementSpeed * Time.deltaTime);
        //    }

        //    if (Vector3.Distance(transform.position, _goToDespawnPoint.position) < 0.1f)
        //    {
        //        StartCoroutine(NaturalDespawn());
        //    }
        //}
    }

    public void OnSpawn(ESpawnerType spawnerType)
    {
        switch (spawnerType)
        {
            case ESpawnerType.Rotate:
                gameObject.SetActive(true);
                _animator.Play(_rotateSpawnHash);
                break;
            case ESpawnerType.PointToPoint:
                //gameObject.SetActive(true);
                //transform.position = _currentSpawnPoint.position;
                //_animator.Play(_rotateSpawnHash);
                break;
        }
    }

    public IEnumerator OnHit()
    {
        _animator.Play(_rotateDespawnHash);
        //add score to player score

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
    }

    public IEnumerator NaturalDespawn()
    {
        Collider collider = GetComponentInChildren<Collider>();
        collider.enabled = false;
        _animator.Play(_rotateDespawnHash);

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
    }
}
