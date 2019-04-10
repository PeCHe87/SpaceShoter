using System;
using UnityEngine;

public class Player : CharacterEntity
{
    [SerializeField] private ScriptablePlayer _playerData = null;

    private Weapon _weapon = null;
    private PlayerMovementByPhysics _movementComponent = null;
    private DamageByCollisionComponent _damageByCollisionComponent = null;

    private void Awake()
    {
        GameEventsManager.OnFuelEmpty += FuelEmpty;

        // Setups the health and subscribes to OnDead event
        Health = GetComponent<HealthController>();
        Health.SetMaxHealth(_playerData.MaxHealth);
        Health.OnDead += Dead;

        // Sets damage by collision if it has that component
        _damageByCollisionComponent = GetComponent<DamageByCollisionComponent>();
        if (_damageByCollisionComponent != null)
            _damageByCollisionComponent.DamageByCollision = _playerData.DamageByCollision;

        // Gets Weapon component
        _weapon = GetComponent<Weapon>();

        // Gets MovementComponent
        _movementComponent = GetComponent<PlayerMovementByPhysics>();

        // Subscribes to Weapon Selection system event of selection
        WeaponSelectionSystem.OnWeaponSelection += WeaponHasChanged;
    }

    private void OnDestroy()
    {
        GameEventsManager.OnFuelEmpty -= FuelEmpty;
        WeaponSelectionSystem.OnWeaponSelection -= WeaponHasChanged;
        Health.OnDead -= Dead;
    }

    private void FuelEmpty()
    {
        Debug.Log("<color=red>Fuel empty</color>");
        Dead();
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