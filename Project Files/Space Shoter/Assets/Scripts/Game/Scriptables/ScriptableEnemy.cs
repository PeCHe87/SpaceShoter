using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Game Elements/Enemy", order = 3)]
[System.Serializable]
public class ScriptableEnemy : ScriptableObject
{
    public float MaxHealth;
    public float DeadDelay;
    public float DetectionRadius;
    public LayerMask DetectionMask;
    public float SpeedRotation;
    public float DelayToStartFiring;
}