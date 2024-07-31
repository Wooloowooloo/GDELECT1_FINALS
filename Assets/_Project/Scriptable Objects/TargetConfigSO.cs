using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configs/Target Config", fileName = "New Target Config")]
public class TargetConfigSO : ScriptableObject
{
    [SerializeField] private ETargetShootType _targetShootType;
    [SerializeField] private int _baseScoreValue;
    [SerializeField] private AudioClip _spawnAudio;
    [SerializeField] private AudioClip _despawnAudio;

    public ETargetShootType TargetShootType { get => _targetShootType; private set => _targetShootType = value; }
    public int BaseScoreValue { get => _baseScoreValue; private set => _baseScoreValue = value;}
    public AudioClip SpawnAudio { get => _spawnAudio; private set => _spawnAudio = value; }
    public AudioClip DespawnAudio { get => _despawnAudio; private set => _despawnAudio = value;}
}

public enum ETargetShootType
{
    DoShoot,
    DoNotShoot
}
