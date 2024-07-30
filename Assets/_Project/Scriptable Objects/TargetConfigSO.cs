using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configs/Target Config", fileName = "New Target Config")]
public class TargetConfigSO : ScriptableObject
{
    [SerializeField] private ETargetShootType _targetShootType;
    [SerializeField] private int _baseScoreValue;

    public ETargetShootType TargetShootType { get => _targetShootType; private set => _targetShootType = value; }
    public int BaseScoreValue { get => _baseScoreValue; private set => _baseScoreValue = value;}
}

public enum ETargetShootType
{
    DoShoot,
    DoNotShoot
}
