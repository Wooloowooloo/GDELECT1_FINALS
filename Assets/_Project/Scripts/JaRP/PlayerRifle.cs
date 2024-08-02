using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerRifle : MonoBehaviour
{
    [Header("Gun Config")]
    [SerializeField] private int _maxAmmo;
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private float _rifleRange;
    [SerializeField] private LayerMask _targetLayer;

    private AudioManager _audioManager;
    private XRGrabInteractable _interactable;
    private Vector3 _resetPosition;
    private Quaternion _resetRotation;
    private int _currentAmmo;

    public int MaxAmmo { get => _maxAmmo; private set => _maxAmmo = value; }
    public int CurrentAmmo { get => _currentAmmo; private set => _currentAmmo = Mathf.Clamp(value, 0, MaxAmmo); }

    private void Awake()
    {
        _audioManager = AudioManager.Instance;
        _interactable = GetComponent<XRGrabInteractable>();
        _resetPosition = transform.position;
        _resetRotation = transform.rotation;
    }

    private void OnEnable()
    {
        _interactable.activated.AddListener(TriggerHapticFeedback);
    }

    private void OnDisable()
    {
        _interactable.activated.RemoveListener(TriggerHapticFeedback);
    }

    private void Start()
    {
        Reload();
    }

    private void Update()
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo, 0, _maxAmmo);
    }

    public void TriggerHapticFeedback(ActivateEventArgs arg)
    {
        if (arg.interactorObject is XRBaseControllerInteractor interactor )
        {
            interactor.SendHapticImpulse(0.5f, 0.3f);
        }
    }

    public void Shoot()
    {
        if (_currentAmmo > 0)
        {
            _audioManager.PlaySFX(ESFXType.RifleShot);
            _currentAmmo--;

            bool hasHit = Physics.Raycast(_firingPoint.position, _firingPoint.forward, out RaycastHit hit, Mathf.Infinity, _targetLayer);

            if (hasHit)
            {
                if (hit.transform.TryGetComponent<Target>(out var target))
                {
                    StartCoroutine(target.OnHit());
                }
            }
        }
        else
        {
            _audioManager.PlaySFX(ESFXType.RifleEmptyShot);
        }
    }

    public void Reload()
    {
        _audioManager.PlaySFX(ESFXType.RifleReload);
        _currentAmmo = _maxAmmo;
    }

    public void ResetRifleLocation()
    {
        transform.SetPositionAndRotation(_resetPosition, _resetRotation);
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(_firingPoint.position, _firingPoint.forward * _rifleRange, Color.green);
    }
}