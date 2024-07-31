using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITarget
{
    [HideInInspector] public TargetSpawner Spawner;

    public ETargetMoveType TargetMoveType;

    [SerializeField] private TargetConfigSO _targetConfig;
    [SerializeField] private string _rotateSpawnHash;
    [SerializeField] private string _rotateDespawnHash;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _movementSpeed;

    private AudioSource _audioSource;
    private Animator _animator;
    private Collider _spawnArea;
    private Bounds _spawnBounds;
    private Vector3 _spawnCenter;
    private Vector3 _spawnPoint;
    private Vector3 _goToPoint;
    private float _currentLifetime;
    private float _assignedLifetime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _spawnArea = transform.root.GetComponent<Collider>();
        _spawnBounds = _spawnArea.bounds;
        _spawnCenter = _spawnArea.bounds.center;
    }

    private void OnEnable()
    {
        _assignedLifetime = Random.Range(_minLifeTime, _maxLifeTime);
    }

    private void OnDisable()
    {
        _currentLifetime = 0;
    }

    private void Update()
    {
        if (TargetMoveType == ETargetMoveType.Stationary)
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
        else if(TargetMoveType == ETargetMoveType.Mobile)
        {
            if (isActiveAndEnabled)
            {
                transform.position = Vector3.MoveTowards(transform.position, _goToPoint, _movementSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, _goToPoint) <= 0.1f)
            {
                StartCoroutine(NaturalDespawn());
            }
        }
    }

    public void OnSpawn()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
        int typeDeterminant = Random.Range(0, 100);
        Transform spawnerLocation = GetComponentInParent<Transform>();
        TargetMoveType = typeDeterminant > 49 ? ETargetMoveType.Stationary : ETargetMoveType.Mobile;
        //_audioSource.PlayOneShot(_targetConfig.SpawnAudio);

        if (TargetMoveType == ETargetMoveType.Stationary)
        {
            _spawnPoint = new()
            {
                x = spawnerLocation.position.x + Random.Range(_spawnCenter.x - _spawnBounds.extents.x, _spawnCenter.x + _spawnBounds.extents.x),
                y = spawnerLocation.position.y,
                z = spawnerLocation.position.z
            };
        }
        else if (TargetMoveType == ETargetMoveType.Mobile)
        {
            int pointDeterminant = Random.Range(0, 2);
            _spawnPoint = new()
            {
                x = pointDeterminant == 0 ?
                    spawnerLocation.position.x + _spawnCenter.x - _spawnBounds.extents.x :
                    spawnerLocation.position.x + _spawnCenter.x + _spawnBounds.extents.x,
                y = spawnerLocation.position.y,
                z = spawnerLocation.position.z - 0.2f
            };
            _goToPoint = new()
            {
                x = pointDeterminant != 0 ?
                    spawnerLocation.position.x + Random.Range(_spawnCenter.x, _spawnCenter.x - _spawnBounds.extents.x) :
                    spawnerLocation.position.x + Random.Range(_spawnCenter.x, _spawnCenter.x + _spawnBounds.extents.x),
                y = spawnerLocation.position.y,
                z = spawnerLocation.position.z - 0.2f
            };
        }

        transform.position = _spawnPoint;
        gameObject.SetActive(true);
        _animator.Play(_rotateSpawnHash);
    }

    public IEnumerator OnHit()
    {
        GameStateManager manager = FindFirstObjectByType<GameStateManager>();
        manager.UpdateScore(_targetConfig.BaseScoreValue);
        //_audioSource.PlayOneShot(_targetConfig.DespawnAudio);

        if (_targetConfig.TargetShootType == ETargetShootType.DoShoot)
        {
            Debug.Log("Yes, plus points");
        }
        else if (_targetConfig.TargetShootType == ETargetShootType.DoNotShoot)
        {
            Debug.LogWarning("No, minus points");
        }

        _animator.Play(_rotateDespawnHash);

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
    }

    public IEnumerator NaturalDespawn()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        //_audioSource.PlayOneShot(_targetConfig.DespawnAudio);
        _animator.Play(_rotateDespawnHash);

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
    }
}

public enum ETargetMoveType
{
    Stationary,
    Mobile
}
