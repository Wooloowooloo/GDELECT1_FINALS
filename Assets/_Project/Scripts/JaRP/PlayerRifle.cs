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

    [Header("Gun Audio")]
    [SerializeField] private AudioClip _rifleShotSFX;
    [SerializeField] private AudioClip _rifleEmptyShotSFX;
    [SerializeField] private AudioClip _rifleReloadSFX;

    private XRGrabInteractable _interactable;
    private AudioSource _rifleAudio;
    private int _currentAmmo;

    public int MaxAmmo { get => _maxAmmo; private set => _maxAmmo = value; }
    public int CurrentAmmo { get => _currentAmmo; private set => _currentAmmo = Mathf.Clamp(value, 0, MaxAmmo); }

    private void Awake()
    {
        _interactable = GetComponent<XRGrabInteractable>();
        _rifleAudio = GetComponent<AudioSource>();
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
            //_rifleAudio.PlayOneShot(_rifleFireSFX);
            //_currentAmmo--;

            bool hasHit = Physics.Raycast(_firingPoint.position, _firingPoint.forward, out RaycastHit hit, Mathf.Infinity, _targetLayer);

            if (hasHit)
            {
                //if (hit.transform.gameObject.TryGetComponent<Target>(out var target))
                //{
                //    StartCoroutine(target.OnHit());
                //    Debug.Log($"target {hit.transform.name} hit!");
                //}
                //else
                //{
                //    Debug.Log($"ya missed, {_currentAmmo} left");
                //}
                Target target = hit.transform.GetComponent<Target>();
                StartCoroutine(target.OnHit());

                Debug.Log($"target {hit.transform.name} hit!");
            }
            else
            {
                Debug.Log($"ya missed, ya hit {hit.transform.name} instead. {_currentAmmo} left");
            }
        }
        else
        {
            //_rifleAudio.PlayOneShot(_rifleEmptyShotSFX);
            Debug.Log("no more bullets");
        }
    }

    public void Reload()
    {
        //_rifleAudio.PlayOneShot(_rifleReloadSFX);
        _currentAmmo = _maxAmmo;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(_firingPoint.position, _firingPoint.forward * _rifleRange, Color.green);
    }
}