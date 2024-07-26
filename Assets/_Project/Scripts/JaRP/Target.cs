using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IShootable
{
    [SerializeField] private TargetConfigSO _targetConfig;
    [SerializeField] private string _rotateSpawnHash;
    [SerializeField] private string _rotateDespawnHash;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnSpawn(ESpawnerType spawnerType)
    {
        gameObject.SetActive(true);

        switch (spawnerType)
        {
            case ESpawnerType.Rotate:
                _animator.Play(_rotateSpawnHash);
                break;
            case ESpawnerType.Move:
                //move
                break;
        }
    }

    public IEnumerator OnHit()
    {
        _animator.Play(_rotateDespawnHash);

        yield return new WaitForSeconds(0.25f);

        // add score value to player score
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }
}
