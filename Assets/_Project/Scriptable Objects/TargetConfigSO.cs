using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configs/Target Config", fileName = "New Target Config")]
public class TargetConfigSO : ScriptableObject
{
    [SerializeField] private ETargetType _targetType;
    [SerializeField] private float _baseScoreValue;

    public ETargetType TargetType { get => _targetType; private set => _targetType = value; }
    public float BaseScoreValue { get => _baseScoreValue; private set => _baseScoreValue = value;}
}

public enum ETargetType
{
    DoShoot,
    DoNotShoot
}
