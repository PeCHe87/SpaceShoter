using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Game Elements/Enemy", order = 3)]
[System.Serializable]
public class ScriptableEnemy : ScriptableObject
{
    public float MaxHealth;
}