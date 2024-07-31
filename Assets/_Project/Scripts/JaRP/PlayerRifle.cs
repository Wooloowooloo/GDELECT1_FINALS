using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerRifle : MonoBehaviour
{
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _raycastRange;
    [SerializeField] private Transform _firingPoint;

    private int _currentAmmo;

    public int MaxAmmo { get => _maxAmmo; private set => _maxAmmo = value; }
    public int CurrentAmmo { get => _currentAmmo; private set => _currentAmmo = Mathf.Clamp(value, 0, MaxAmmo); }

    private void Start()
    {
        Reload();
    }

    private void Update()
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo, 0, _maxAmmo);
    }

    public void Shoot()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo--;

            bool hasHit = Physics.Raycast(_firingPoint.position, _firingPoint.forward, out RaycastHit hit, _raycastRange);

            if (hasHit)
            {
                if (hit.collider is ITarget target)
                {
                    target.OnHit();
                    Debug.Log("target hit!");
                }
                else
                {
                    Debug.Log("ya missed");
                }
            }
        }
        else
        {
            Debug.Log("no more bullets");
        }
    }

    public void Reload()
    {
        _currentAmmo = _maxAmmo;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(_firingPoint.position, _firingPoint.forward * _raycastRange, Color.green);
    }
}