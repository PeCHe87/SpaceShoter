using UnityEngine;

public class Enemy : CharacterEntity
{
    [SerializeField] private ScriptableEnemy _data = null;

    private void Awake()
    {
        Health = GetComponent<HealthController>();
        Health.SetMaxHealth(_data.MaxHealth);
    }
}
