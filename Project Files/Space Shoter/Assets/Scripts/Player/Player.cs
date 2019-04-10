using UnityEngine;

public class Player : CharacterEntity
{
    [SerializeField] private ScriptablePlayer _playerData = null;

    private Weapon _weapon = null;
    private PlayerMovementByPhysics _movementComponent = null;

    private void Awake()
    {
        Health = GetComponent<HealthController>();
        Health.SetMaxHealth(_playerData.MaxHealth);
        Health.OnDead += Dead;

        _weapon = GetComponent<Weapon>();

        _movementComponent = GetComponent<PlayerMovementByPhysics>();

        WeaponSelectionSystem.OnWeaponSelection += WeaponHasChanged;
    }

    private void OnDestroy()
    {
        WeaponSelectionSystem.OnWeaponSelection -= WeaponHasChanged;
        Health.OnDead -= Dead;
    }

    private void WeaponHasChanged(ScriptableWeapon weaponData)
    {
        _weapon.SetWeaponData(weaponData);
    }

    private void Update()
    {
        if (Health.IsDead)
            return;

        if (_weapon != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _weapon.Fire();
            }
        }
    }

    private void Dead()
    {
        // Deactivates every component related with player
        if (_movementComponent != null)
            _movementComponent.Disabled();
    }
}