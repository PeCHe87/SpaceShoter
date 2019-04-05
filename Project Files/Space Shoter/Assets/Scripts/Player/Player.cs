using UnityEngine;

public class Player : CharacterEntity
{
    [SerializeField] private ScriptablePlayer _playerData = null;

    private void Awake()
    {
        Health = GetComponent<HealthController>();
        Health.SetMaxHealth(_playerData.MaxHealth);
    }
}
