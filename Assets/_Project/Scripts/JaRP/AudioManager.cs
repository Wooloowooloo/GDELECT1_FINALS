using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-15)]
public class AudioManager : PersistentSingleton<AudioManager>
{
    [Header("Game State BGMs")]
    [SerializeField] private AudioSource _nonGameplayMusic;
    [SerializeField] private AudioSource _gameplayMusic;

    [Header("Rifle SFX")]
    [SerializeField] private AudioSource _rifleShot;
    [SerializeField] private AudioSource _rifleEmptyShot;
    [SerializeField] private AudioSource _rifleReload;

    [Header("Target and Spawner SFX")]
    [SerializeField] private AudioSource _spawnerSpawn;
    [SerializeField] private AudioSource _spawnerDespawn;
    [SerializeField] private AudioSource _targetHit;

    [Header("Button SFX")]
    [SerializeField] private AudioSource _button;

    private AudioSource _currentMusic, _nextMusic;

    public void PlayMusic(EMusicType musicToPlay)
    {
        if (_currentMusic != null)
        {
            StartCoroutine(Fade(_currentMusic, 0.25f, 0.2f));          
        }

        if (musicToPlay == EMusicType.Gameplay)
        {
            _currentMusic = _gameplayMusic;
            _nextMusic = _nonGameplayMusic;
        }
        else
        {
            _currentMusic = _nonGameplayMusic;
            _nextMusic = _gameplayMusic;
        }

        _currentMusic.volume = 0.2f;
        _currentMusic.Play();
    }

    public void PlaySFX(ESFXType sfxToPlay)
    {
        switch (sfxToPlay)
        {
            case ESFXType.RifleShot:
                _rifleShot.Play();
                break;
            case ESFXType.RifleEmptyShot:
                _rifleEmptyShot.Play();
                break;
            case ESFXType.RifleReload:
                _rifleReload.Play();
                break;
            case ESFXType.SpawnerSpawn:
                _spawnerSpawn.Play();
                break;
            case ESFXType.SpawnerDespawn:
                _spawnerDespawn.Play();
                break;
            case ESFXType.TargetHit:
                _targetHit.Play();
                break;
        }
    }

    public void PlayButtonSFX()
    {
        _button.Play();
    }

    private IEnumerator Fade(AudioSource audioToFade, float fadeDuration, float targetVolume)
    {
        /*
        float fadeTime = 0f;
        float startingVolume = audioToFade.volume;

        while (fadeTime <= fadeDuration)
        {
            fadeTime += Time.deltaTime;
            audioToFade.volume = Mathf.Lerp(startingVolume, targetVolume, fadeTime / fadeDuration);
            yield return null;
        }*/
        float fadeTime = 0f;
        _nextMusic.Play();

        while (fadeTime <= fadeDuration)
        {
            _nextMusic.volume = Mathf.Lerp(0, targetVolume, fadeTime / fadeDuration);
            _currentMusic.volume = Mathf.Lerp(targetVolume, 0, fadeTime / fadeDuration);
            fadeDuration += Time.deltaTime;
            yield return null;
        }

        _currentMusic.Stop();
    }
}

public enum EMusicType
{
    NonGameplay,
    Gameplay,
}

public enum ESFXType
{
    RifleShot,
    RifleEmptyShot,
    RifleReload,
    SpawnerSpawn,
    SpawnerDespawn,
    TargetHit,
    Button
}
