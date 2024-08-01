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

    [Header("Stinger Transitions")]
    [SerializeField] private AudioSource _startRound;
    [SerializeField] private AudioSource _endRound;
    [SerializeField] private AudioSource _playAgain;

    private AudioSource _currentMusic;

    public void PlayMusic(EMusicType musicToPlay)
    {
        if (_currentMusic != null)
        {
            StartCoroutine(Fade(_currentMusic, 2f, 0f));
        }

        if (musicToPlay == EMusicType.Gameplay)
        {
            _currentMusic = _gameplayMusic;
        }
        else
        {
            _currentMusic = _nonGameplayMusic;
        }

        _currentMusic.volume = 1f;
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
            case ESFXType.StartRound:
                _startRound.Play();
                break;
            case ESFXType.EndRound:
                _endRound.Play();
                break;
            case ESFXType.PlayAgain:
                _playAgain.Play();
                break;
        }
    }

    private IEnumerator Fade(AudioSource audioToFade, float fadeDuration, float targetVolume)
    {
        float fadeTime = 0f;
        float startingVolume = audioToFade.volume;

        while (fadeTime > fadeDuration)
        {
            fadeTime += Time.deltaTime;
            audioToFade.volume = Mathf.Lerp(startingVolume, targetVolume, fadeTime / fadeDuration);
            yield return null;
        }
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
    StartRound,
    EndRound,
    PlayAgain
}
